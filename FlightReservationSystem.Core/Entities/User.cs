namespace FlightReservation.Core
{
    public abstract class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public abstract bool Login(string username, string password);

    }
}
