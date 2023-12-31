﻿namespace DomainLayer.Constants;

public static class Constants
{
    public static class Http
    {
        public const string API_VERSION = "v1";
        public const string CORS = "CORS";
        public const string JSON_CONTENT_TYPE = "application/json";
        public const string USER_POLICY = "User";
    }

    public static class HttpContext
    {
        public const string UID = "uid";
        public const string SID = "sid";
        public const string SCP = "http://schemas.microsoft.com/identity/claims/scope";
    }

    public static class Errors
    {
        public const string NOT_EXIST_ERROR = "not exist";
        public const string ALREADY_EXIST_ERROR = "already exist";
    }

    public static class Entities
    {
        public const string ACTIVITY = "Activity ";
        public const string ACTIVITY_TYPE = "Activity Type ";
        public const string ASSET_ATTRIBUTE = "Asset attribute ";
        public const string ASSET = "Asset ";
        public const string ASSET_TYPE = "Asset type ";
        public const string ATTRIBUTE_GROUP = "Attribute group ";
        public const string CHARACTER_ASSET = "Character asset ";
        public const string CHARACTER_ATTRIBUTE = "Character attribute ";
        public const string CHARACTER = "Character ";
        public const string CHARACTER_TYPE = "Character type ";
        public const string GAME = "Game ";
        public const string GAME_SERVER = "Game server ";
        public const string LEVEL = "Level ";
        public const string LEVEL_PROGRESS = "Level progress ";
        public const string PAYMENT = "Payment ";
        public const string TRANSACTION = "Transaction ";
        public const string USER = "User ";
        public const string WALLET_CATEGORY = "Wallet category ";
        public const string WALLET = "Wallet ";
    }


}