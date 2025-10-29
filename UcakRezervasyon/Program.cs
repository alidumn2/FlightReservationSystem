using UcakRezervasyon.Core;
using System;

namespace UcakRezervasyon.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Yeni Müşteri Kayıt Ekranı ---");

            // 1. Kullanıcıdan bilgileri al
            Console.Write("Lütfen adınızı girin: ");
            string girilenAd = Console.ReadLine();

            Console.Write("Lütfen soyadınızı girin: ");
            string girilenSoyad = Console.ReadLine();

            Console.Write("Lütfen kullanıcı adınızı girin: ");
            string girilenKullaniciAdi = Console.ReadLine();

            // 2. Alınan bilgilerle yeni bir Customer nesnesi oluştur
            User yeniMusteri = new User
            {
                Name = girilenAd,
                Surname = girilenSoyad,
                UserName = girilenKullaniciAdi
                
            };

            
            Console.WriteLine("\n--- Müşteri Başarıyla Oluşturuldu! ---");
            Console.WriteLine($"Ad: {yeniMusteri.Name}");
            Console.WriteLine($"Soyad: {yeniMusteri.Surname}");
            Console.WriteLine($"Kullanıcı Adı: {yeniMusteri.UserName}");

            
            Console.WriteLine("\nÇıkmak için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}
