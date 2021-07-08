namespace Blaash.Gaming.Infrastructre.Common
{

    public static class GameMvpCommonConsts
    {
        public static class CommonKeys
        {
            public const string EVENT_NAME = "event_name";
            public const string IS_PROCESSED = "is_processed";
            public const string USER_ID = "user_id";
            public const string FIRST_NAME = "first_name";
            public const string LAST_NAME = "last_name";
            public const string MIDDLE_NAME = "middle_name";
            public const string PLAYER_USER_ROLE = "mvp_player";
        }
       
        public static class Collections
        {
            public const string EVENT = "event_track";
            public const string TENANT = "tenant";
            public const string TENANT_CLIENT = "tenant_client";
            
            public const string PLAYER_SUMMARY = "player_summary";

            public const string WINNERS = "winners";


        }
        public static class EventTypes
        {
            
            public const string PLAYER_LOGIN = "player_login";
            public const string VIEW_PRODUCT = "view_product";
        }

    }
}
