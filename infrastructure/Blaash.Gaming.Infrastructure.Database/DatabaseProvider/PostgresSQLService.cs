using System;
using System.Collections.Generic;
using System.Data.Common;
using Dapper;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json.Linq;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Interfaces;
using System.Linq;
using ZNxt.Net.Core.Model;
using Npgsql;

namespace Blaash.Gaming.Infrastructure.Database.PostgresSQL
{
    public class PostgresSQLService : IRDBService
    {
        private string _connectionStr = "";
        private string _DBType = "NONE";

        private IHttpContextProxy _httpContextProxy;
        public PostgresSQLService(IHttpContextProxy httpContextProxy)
        {
            _httpContextProxy = httpContextProxy;

            _connectionStr = CommonUtility.GetAppConfigValue("NPGSQLConnectionString");
            if (!string.IsNullOrEmpty(_connectionStr))
            {
                _DBType = "NPGSQL";
            }

            /*  _connectionStr = CommonUtility.GetAppConfigValue("MYSQLConnectionString");
              if (string.IsNullOrEmpty(_connectionStr))
              {
                  _connectionStr = CommonUtility.GetAppConfigValue("MSSQLConnectionString");
                  if (!string.IsNullOrEmpty(_connectionStr))
                  {
                      _DBType = "MSSQL";
                  }
                  else
                  {
                      _connectionStr = CommonUtility.GetAppConfigValue("NPGSQLConnectionString");
                      if (!string.IsNullOrEmpty(_connectionStr))
                      {
                          _DBType = "NPGSQL";
                      }
                  }
              }
              else
              {
                  _DBType = "MYSQL";
              }*/
        }

        public RDBTransaction BeginTransaction()
        {
            var conn = GetConnection();
            conn.Open();
            return new RDBTransaction(conn.BeginTransaction(), conn);
        }

        public void CommitTransaction(RDBTransaction transaction)
        {
            transaction.Transaction.Commit();
            transaction.Connection.Close();
            transaction.Connection.Dispose();
        }

        public void Init(string dbType, string connectionString)
        {
            if (!string.IsNullOrEmpty(connectionString) && !string.IsNullOrEmpty(dbType))
            {
                _connectionStr = connectionString;
                _DBType = dbType;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void RollbackTransaction(RDBTransaction transaction)
        {
            transaction.Transaction.Rollback();
            transaction.Connection.Close();
            transaction.Connection.Dispose();
        }
        public DbConnection GetConnection()
        {
            switch (_DBType.ToUpper())
            {
                case "NPGSQL":
                    return new NpgsqlConnection(_connectionStr);
            }
            throw new NotSupportedException($"DB Type {_DBType} not supported");
        }

        #region get 

        public IEnumerable<T> Get<T>(string sql, object param = null) where T : class
        {
            using (var conn = GetConnection())
            {
                if (typeof(T) == typeof(JObject))
                {
                    IEnumerable<JObject> data = GetDateAsJObject(conn, sql, param);
                    return data as IEnumerable<T>;
                }
                return conn.Query<T>(sql, param);
            }
        }

        public IEnumerable<T> Get<T>(string tablename, int top, int skipped, JObject filter) where T : class
        {
            var sql = $"select * from {tablename}";

            var filters = new List<string>();
            if (filter != null)
            {
                foreach (var item in filter)
                {
                    filters.Add($"{item.Key}='{item.Value}'");
                }
            }
            AddTenantFilter<T>(filter);
            if (filters.Any())
            {
                sql = $"{sql} where { string.Join(" and ", filters) }";
            }
            sql = $"{sql} LIMIT {top} OFFSET {skipped}";

            using (var conn = GetConnection())
            {
                if (typeof(T) == typeof(JObject))
                {
                    IEnumerable<JObject> data = GetDateAsJObject(conn, sql);
                    return data as IEnumerable<T>;
                }
                return conn.Query<T>(sql);
            }
        }
        public IEnumerable<T> Get<T>(string tablename, int top, int skipped, string filter) where T : class
        {
            var sql = $"select * from {tablename}";

            var filters = new List<string>();


            if (!string.IsNullOrEmpty(filter))
            {
                sql = $"{sql} where { filter}";
            }
            sql = $"{sql} LIMIT {top} OFFSET {skipped}";

            using (var conn = GetConnection())
            {
                if (typeof(T) == typeof(JObject))
                {
                    IEnumerable<JObject> data = GetDateAsJObject(conn, sql);
                    return data as IEnumerable<T>;
                }
                return conn.Query<T>(sql);
            }
        }

        public long GetCount(string tablename, string filter)
        {
            var sql = $"select count(*) from {tablename} ";

            if (!string.IsNullOrEmpty(filter))
            {

                sql = $"{sql} where { filter}";
            }
            using (var conn = GetConnection())
            {
                return conn.QueryFirst<long>(sql);
            }
        }
        public long GetCount(string tablename, JObject filter)
        {
            var filters = new List<string>();
            if (filter != null)
            {
                foreach (var item in filter)
                {
                    filters.Add($"{item.Key}='{item.Value}'");
                }
            }

            var sql = $"select count(*) from {tablename} ";
            if (filters.Any())
            {
                sql = $"{sql} where { string.Join(" and ", filters) }";
            }
            using (var conn = GetConnection())
            {
                return conn.QueryFirst<long>(sql);
            }
        }

        public T GetFirst<T>(string sql, object param = null) where T : class
        {
            using (var conn = GetConnection())
            {
                if (typeof(T) == typeof(JObject))
                {
                    IEnumerable<JObject> data = GetDateAsJObject(conn, sql, param);
                    if (data.Any())
                    {
                        return data.First() as T;
                    }
                    else
                    {
                        return default;
                    }
                }
                else
                {
                    return conn.QueryFirst<T>(sql, param);
                }
            }
        }
        public T GetFirst<T>(string id) where T : class
        {
            using (var conn = GetConnection())
            {
                return conn.Get<T>(id);
            }
        }
        private IEnumerable<JObject> GetDateAsJObject(DbConnection conn, string sql, object param = null)
        {
            List<JObject> dataResult = new List<JObject>();
            conn.Open();
            List<string> columns = new List<string>();
            DbCommand oCmd = null;
            try
            {
                switch (_DBType)
                {
                   
                    case "NPGSQL":
                        oCmd = new NpgsqlCommand(sql, conn as NpgsqlConnection);
                        break;
                }
                using (DbDataReader oReader = oCmd.ExecuteReader())
                {
                    for (int i = 0; i < oReader.FieldCount; i++)
                    {
                        columns.Add(oReader.GetName(i));
                    }
                    while (oReader.Read())
                    {
                        JObject jdata = new JObject();
                        foreach (var col in columns)
                        {
                            jdata[col] = oReader[col].ToString();
                        }
                        dataResult.Add(jdata);
                    }

                }
            }
            finally
            {
                conn.Close();
            }

            return dataResult;
        }

        #endregion

        #region update

        public bool Update(string sql)
        {
            using (var conn = GetConnection())
            {
                return conn.Update(sql);
            }
        }

        public bool Update(string sql, object param = null, RDBTransaction transaction = null)
        {
            if (transaction != null)
            {
                transaction.Connection.Execute(sql, param);
            }
            else
            {
                using (var conn = GetConnection())
                {
                    conn.Execute(sql, param);
                }
            }
            return true;
        }

        public bool Update(string sql, RDBTransaction transaction)
        {
            return transaction.Connection.Update(sql, transaction.Transaction);
        }

        public bool Update(IEnumerable<string> sql)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {
                    foreach (var item in sql)
                    {
                        if (conn.Update(item, transaction))
                        {
                            try
                            {
                                transaction.Rollback();
                                return false;
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return true;
        }

        public bool Update<T>(T data) where T : class
        {
            using (var conn = GetConnection())
            {
                SetDefaultValues<T>(data, false);
                return conn.Update<T>(data);
            }
        }
        public bool Update<T>(T data, RDBTransaction transaction) where T : class
        {
            SetDefaultValues<T>(data, false);
            return transaction.Connection.Update<T>(data, transaction.Transaction);
        }


        #endregion

        #region insert


        public bool WriteData<T>(IEnumerable<T> data) where T : class
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {
                    foreach (var item in data)
                    {
                        SetDefaultValues<T>(item);
                        var result = conn.Insert<T>(item, transaction);
                        if (result != 0)
                        {
                            try
                            {
                                transaction.Rollback();
                                return false;
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return true;
        }
        public long WriteData<T>(T data) where T : class
        {
            using (var conn = GetConnection())
            {
                SetDefaultValues<T>(data);
                var result = conn.Insert<T>(data);
                return result;
            }
        }
        public long WriteData<T>(T data, RDBTransaction transaction) where T : class
        {
            SetDefaultValues<T>(data);
            return transaction.Connection.Insert<T>(data, transaction.Transaction);

        }
        public bool WriteData(string sql, object param = null)
        {
            using (var conn = GetConnection())
            {
                return conn.Execute(sql, param) == 1;
            }
        }
        public long WriteDataGetId(string sql, object param = null)
        {
            using (var conn = GetConnection())
            {
                return conn.Query<int>(sql, param).Single();
            }
        }
        public int WriteDataGetId(string sql, object param = null, RDBTransaction transaction = null)
        {
            return transaction.Connection.Query<int>(sql, param, transaction.Transaction).Single();
        }
        public bool WriteData(string sql, object param = null, RDBTransaction transaction = null)
        {
            if (transaction != null)
            {
                var result = transaction.Connection.Execute(sql, param, transaction.Transaction);
                return result == 1;
            }
            else
            {
                var result = transaction.Connection.Execute(sql, param);
                return result == 1;
            }

        }

        #endregion

        #region delete 
        public bool Delete(string sql)
        {
            using (var conn = GetConnection())
            {
                return conn.Delete(sql);
            }
        }

        public bool Delete(string sql, RDBTransaction transaction)
        {
            return transaction.Connection.Delete(sql, transaction.Transaction);
        }

        public bool Delete(IEnumerable<string> sql)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {
                    foreach (var item in sql)
                    {
                        if (conn.Delete(item, transaction))
                        {
                            try
                            {
                                transaction.Rollback();
                                return false;
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return true;
        }

        public bool Delete<T>(T data) where T : class
        {
            using (var conn = GetConnection())
            {
                return conn.Delete<T>(data);
            }
        }
        public bool Delete<T>(T data, RDBTransaction transaction) where T : class
        {
            return transaction.Connection.Delete<T>(data, transaction.Transaction);
        }

        DbConnection IRDBService.GetConnection()
        {
            throw new NotImplementedException();
        }
        #endregion

        private void AddTenantFilter<T>(JObject filter)
        {
            if (filter != null)
            {
                if (typeof(T).IsSubclassOf(typeof(BaseModelTenantDbo)))
                {
                    var tenantId = _httpContextProxy.GetRequestTenantId();
                    if (!string.IsNullOrEmpty(tenantId))
                    {
                        filter["tenant_id"] = long.Parse(tenantId);
                    }
                }
            }
        }
        private void SetDefaultValues<T>(T request, bool setCreated = true)
        {
            if (request != null)
            {
                if (typeof(T).IsSubclassOf(typeof(BaseModelDbo)))
                {
                    var user = _httpContextProxy.User;
                    if (user != null)
                    {
                        if (setCreated)
                        {
                            ((BaseModelDbo)((object)request)).created_by = user.user_id;
                        }
                        else
                        {
                            ((BaseModelDbo)((object)request)).updated_by = user.user_id;
                            // ((BaseModelDbo)((object)request)).updated_on = CommonUtility.GetUnixTimestamp(DateTime.UtcNow);
                        }
                    }
                }
                if (typeof(T).IsSubclassOf(typeof(BaseModelTenantDbo)))
                {
                    var tenantId = _httpContextProxy.GetRequestTenantId();
                    if (!string.IsNullOrEmpty(tenantId))
                    {
                        ((BaseModelTenantDbo)((object)request)).tenant_id = long.Parse(tenantId);
                    }
                }
            }
        }
    }
}
