using System;
using System.Collections.Generic;
using System.Text;

namespace ZNxt.Net.Core.Model
{
    public class BaseModelDbo
    {
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set; }
        public string updated_by { get; set; }

        public BaseModelDbo()
        {
            updated_on = created_on = ZNxt.Net.Core.Helpers.CommonUtility.GetUTCTime(DateTime.Now);
        }
    }

    public class BaseModelTenantDbo : BaseModelDbo
    {
        public long tenant_id { get; set; }
    }
}
