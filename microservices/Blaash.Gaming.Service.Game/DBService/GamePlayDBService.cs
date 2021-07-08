using Blaash.Gaming.Infrastructre.Common.TimeZone;
using Blaash.Gaming.Service.GamePlay.DBService.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Interfaces;

namespace Blaash.Gaming.Service.GamePlay.DBService
{
    public class GamePlayDBService
    {
        private readonly IDBService _dBService;
        private readonly IRDBService _rDBService;
        private readonly ILogger _logger;
        private readonly HttpClientService _httpClientService;

        public GamePlayDBService(IDBService dBService, IRDBService rDBService, ILogger logger)
        {
            _dBService = dBService;
            _logger = logger;
            _rDBService = rDBService;
            _httpClientService = new HttpClientService();
        }

        ///Methods with NO DB Operations
        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public int GetRewardLevel()
        {
            return new Random().Next(25);
        }


        //Perform All DB Operation Here
        public List<PlayerPointsBalanceDbo> GetPlayerPointsBalance(long customerId, long tenantId, string fetchforLastX)
        {
            try
            {
                Enum.TryParse(fetchforLastX, out SummeryDateTime summeryDateTime);
                var lastXdays = 7;
                switch (summeryDateTime)
                {
                    case SummeryDateTime.Last7Days:
                        lastXdays = 7;
                        break;
                    case SummeryDateTime.LastMonth:
                        lastXdays = 30;
                        break;
                    case SummeryDateTime.Last6Month:
                        lastXdays = 180;
                        break;
                    default:
                        lastXdays = 7;
                        break;
                }

                var playerPointsBalance = _rDBService.Get<PlayerPointsBalanceDbo>($"SELECT * FROM get_date_limited_points_balance({customerId},{tenantId},{lastXdays})");

                return playerPointsBalance.ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"Getting Player Points Balance is failed; Error {ex.Message}");
                throw ex;
            }
        }


        public GameLaunchResponse CreateGamePlayData(GameLaunchRequest gameLaunchRequest)
        {
            try
            {
                var gamePlayDbo = new GamePlayDbo
                {
                    game_id = gameLaunchRequest.GameID,
                    player_id = gameLaunchRequest.PlayerID,
                    engagement_id = gameLaunchRequest.EngagementID,
                    game_session_id = Guid.NewGuid().ToString("N"), //returns 32 character string
                    game_session_start = DateTime.Now.ConverTlocalToUTC(),
                    game_session_end = null,
                    game_exit_datetime = null
                };
                var GamePlayID = _rDBService.WriteData(gamePlayDbo);

                ///Get Reward Details From Engagement Service
                var bonusRewards = _httpClientService.GetBonusRewardResponse(gamePlayDbo.engagement_id);
                var totalRewardsCount = bonusRewards.data.ToList().Count;
                // Selecting random reward based on Probability
                var reward = bonusRewards.data.ToList().ElementAtOrDefault(new Random().Next(0, totalRewardsCount - 1));

                var gameScore = GetGameScoreData(gameLaunchRequest.GameID, gameLaunchRequest.PlayerID, gameLaunchRequest.TenantID);

                var restartLevelfromBegining = bool.Parse(CommonUtility.GetAppConfigValue("restartLevelfromBegining"));
                var ApiKey = CommonUtility.GetAppConfigValue("API_KEY");

                var Token = RandomString(10) + gamePlayDbo.game_session_id + ApiKey + RandomString(10);
                return new GameLaunchResponse
                {
                    Token = Token,
                    CurrentLevel = restartLevelfromBegining && gameScore.Any() ? gameScore.First().current_level : 1,
                    CurrentScore = gameScore.Any() ? gameScore.First().current_score : 0,
                    GameSessionID = gamePlayDbo.game_session_id,
                    RewardLevel = GetRewardLevel(),
                    RewardID = reward.EngagementRewardId,
                    RewardText = reward.RewardText,
                    SocialShareMessage = " Here it is Social Share Mesage"
                };
            }
            catch (Exception ex)
            {
                _logger.Error($"Create GamePlay Record on GameLaunch is failed; Error {ex.Message}");
                throw;
            }
        }

        public void UpdateGamePlayDataOnGameOver(GameOverRequest gameExitRequest)
        {
            try
            {
                var gamePlay = GetGamePlayByGameSessionID(gameExitRequest.GameSessionID).First();
                gamePlay.final_score = gameExitRequest.CurrentScore;
                gamePlay.is_cancelled = true;
                gamePlay.game_session_end = DateTime.UtcNow;
                gamePlay.game_exit_datetime = DateTime.UtcNow;
                _ = _rDBService.Update(gamePlay);
            }
            catch (Exception ex)
            {
                _logger.Error($"Update GamePlay Record on GameOver is failed; Error {ex.Message}");
                throw;
            }
        }

        public void UpdateGamePlayLevelData(GameLevelCompleteRequest gameLevelCompleteRequest)
        {
            try
            {
                var gamePlay = GetGamePlayByGameSessionID(gameLevelCompleteRequest.GameSessionID).FirstOrDefault();
                gamePlay.final_score = gameLevelCompleteRequest.CurrentScore;

                _ = _rDBService.Update(gamePlay);
            }
            catch (Exception ex)
            {
                _logger.Error($"Updating GamePlay Level Complete Data is failed; Error: {ex.Message}");
                throw;
            }
        }

        public void UpdateGameScoreOnPurchase(PurchasedRequest request, long TenantID)
        {
            var gamePlay = GetGamePlayByGameSessionID(request.GameSessionID).First();
            var gameScore = GetGameScoreData(gamePlay.game_id, gamePlay.player_id, TenantID).First();
            if (gameScore.current_score >= request.PurchasedAmount)
            {
                gameScore.current_score -= request.PurchasedAmount;
                _ = _rDBService.Update(gameScore);
            }
            else
            {
                _logger.Info($"Amount is greater than player score. PlayerID {gamePlay.player_id}");
            }
        }


        ///Common Methods to Perform DB Operations
        public IEnumerable<GamePlayDbo> GetGamePlayByGameSessionID(string gameSessionID)
        {
            return _rDBService.Get<GamePlayDbo>(GamePlayTables.GAME_PLAY, 1, 0, new JObject
            {
                [GamePlayTableColumns.GAME_SESSION_ID] = gameSessionID
            });
        }

        public long GetEngagementIDFromGamePlayByGameSessionID(string gameSessionID)
        {
            return _rDBService.Get<GamePlayDbo>(GamePlayTables.GAME_PLAY, 1, 0, new JObject
            {
                [GamePlayTableColumns.GAME_SESSION_ID] = gameSessionID
            }).FirstOrDefault().engagement_id;
        }

        public long GetGameIDFromGamePlayByGameSessionID(string gameSessionID)
        {
            return _rDBService.Get<GamePlayDbo>(GamePlayTables.GAME_PLAY, 1, 0, new JObject
            {
                [GamePlayTableColumns.GAME_SESSION_ID] = gameSessionID
            }).FirstOrDefault().game_id;
        }

        public IEnumerable<GameScoreDbo> GetGameScoreData(long GameID, long PlayerID, long TenantID)
        {
            return _rDBService.Get<GameScoreDbo>(GamePlayTables.GAME_SCORE, 1, 0, new JObject
            {
                [GamePlayTableColumns.GAME_ID] = GameID,
                [GamePlayTableColumns.PLAYER_ID] = PlayerID,
                [GamePlayTableColumns.TENANT_ID] = TenantID,
            });
        }

    }
}
