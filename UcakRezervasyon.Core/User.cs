namespace UcakRezervasyon.Core
{
    public abstract class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; } // Şifrenin güvenli (hash'lenmiş) hali
        public string Name { get; set; }
        public string Surname { get; set; }

        public abstract bool Login(string username, string password);

    }
}
