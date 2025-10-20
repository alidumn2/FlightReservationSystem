namespace UcakRezervasyon.Core
{
    public class User
    {
        private global::System.String passwordHash;
        private global::System.String userName;
        private global::System.String surname;
        private global::System.String name;

        public int Id { get; set; }
        public string UserName { get => userName; set => userName = value; }
        public string PasswordHash { get => passwordHash; set => passwordHash = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }

    }
}
