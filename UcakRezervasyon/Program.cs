using UcakRezervasyon.Core;

Console.WriteLine("Test Başladı!");

User yenikullanici = new User
{
    Id = 1,
    UserName = "testuser",
    PasswordHash = "hashedpassword",
    Name = "Test",
    Surname = "Kullanıcı"
};

Console.WriteLine($"yeni kullanici id:{yenikullanici.Id},username: {yenikullanici.UserName},name:{yenikullanici.Name}");

Console.ReadLine();
