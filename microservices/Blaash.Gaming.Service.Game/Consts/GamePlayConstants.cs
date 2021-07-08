namespace Blaash.Gaming.Service.GamePlay
{
    public static class GamePlayConstants
    {
        public const string SERVICE_NAME = "Blaash.Gaming.Service.Game";
        public const string SERVICE_API_PREFIX = "/game";
        public static string GetServiceInfo()
        {
            return $"Name: {SERVICE_NAME}, Api Prefix: {SERVICE_API_PREFIX}";
        }
    }

    public static class GamePlayTables
    {
        public const string EXTERNAL_GAMES_SESSION = "external_games_session";
        public const string GAME = "game";
        public const string GAME_AUTHORIZATION = "game_authorization";
        public const string GAME_PLAY = "game_play";
        public const string GAME_SCORE = "game_score";
        public const string GAME_TYPE = "game_type";
        public const string GAME_PLAY_ACTIVITY = "game_play_activity";
        public const string GAME_PLAY_ACTIVITY_TYPE = "game_play_activity_type";
        public const string PLAYER_POINTS_BALANCE = "player_points_balance";
        public const string TENANT_GAMES = "tenant_games";

    }

    public static class GamePlayTableColumns
    {
        public const string TENANT_ID = "tenant_id";
        public const string PLAYER_ID = "player_id";
        public const string GAME_ID = "game_id";
        public const string GAME_SESSION_ID = "game_session_id";
    }
}
