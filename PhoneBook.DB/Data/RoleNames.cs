namespace PhoneBook.DB.Data
{
    public static class RoleNames
    {
        public const string Administrator = "Администратор";
        public const string User = "Пользователь";

        public static IEnumerable<string> AllRoles
        {
            get
            {
                yield return Administrator;
                yield return User;
            }
        }
    }
}