﻿namespace api.Utility
{
    public class Constants
    {
        public const string SuperAdminRole = "SuperAdmin";
        public const string OwnerRole = "Owner";
        public const string AdminRole = "Admin";
        public const string ManagerRole = "Manager";
        public const string UserRole = "User";

        // For controller attributes
        public const string AllRoles = SuperAdminRole + "," + OwnerRole + "," + AdminRole + "," + ManagerRole + "," + UserRole;
        public const string AllAdminRoles = SuperAdminRole + "," + OwnerRole + "," + AdminRole;
        public const string AllManagerRoles = SuperAdminRole + "," + OwnerRole + "," + AdminRole + "," + ManagerRole;
        public const string AssignableRoles = AdminRole + "," + ManagerRole + "," + UserRole;

        public const string RefreshTokenCookieName = "refreshToken";

        public const string TempFolderName = "temp";
        public static readonly string[] TimeUnitNameList = {"Day(s)", "Week(s)", "Month(s)", "Year(s)"};
    }
}
