using FlightReservation.Core;       
using FlightReservation.Core.Entities;
using FlightReservation.Core.Helpers;
using FlightReservation.DataAccess.Context; 
using FlightReservation.WinFormUI;  
using FlightReservation.WinFormUI.Forms.AdminForms;    
using FlightReservation.WinFormUI.Forms.AuthForms;     
using FlightReservation.WinFormUI.Forms.CustomerForms; 
using Microsoft.EntityFrameworkCore;

namespace FlightReservationSystem.WinFormUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // BAÞLANGIÇ VERÝSÝ
            // Program her açýldýðýnda veritabanýný kontrol et
            using (var db = new AppDbContext())
            {
                // Veritabaný dosyasý yoksa oluþtur
                db.Database.Migrate();

                // Eðer Kullanýcýlar tablosu boþsa, varsayýlan Admin'i ekle
                if (!db.Users.Any())
                {
                    db.Users.Add(new Admin
                    {
                        UserName = "admin",
                        Password = SecurityHelper.HashPassword("123"),
                        Name = "Sistem",
                        Surname = "Yöneticisi"
                    });

                    db.SaveChanges();
                }

                if (!db.Airports.Any())
                {
                    db.Airports.AddRange(
                        new Airport { City = "Istanbul", Name = "Istanbul Havalimaný", Code = "IST" },
                        new Airport { City = "Istanbul", Name = "Sabiha Gökçen", Code = "SAW" },
                        new Airport { City = "Ankara", Name = "Esenboða", Code = "ESB" },
                        new Airport { City = "Izmir", Name = "Adnan Menderes", Code = "ADB" },
                        new Airport { City = "Antalya", Name = "Antalya Havalimaný", Code = "AYT" },
                        new Airport { City = "Mersin", Name = "Çukurova Uluslararasý", Code = "COV" },
                        new Airport { City = "Rize", Name = "Rize-Artvin Havalimaný", Code = "RZV" },
                        new Airport { City = "Trabzon", Name = "Trabzon Havalimaný", Code = "TZX" },
                        new Airport { City = "Muðla", Name = "Dalaman", Code = "DLM" },
                        new Airport { City = "Muðla", Name = "Milas-Bodrum", Code = "BJV" },
                        new Airport { City = "Giresun", Name = "Ordu-Giresun Havalimaný", Code = "OGU" },
                        new Airport { City = "Gaziantep", Name = "Gaziantep Havalimaný", Code = "GZT" },
                        new Airport { City = "Kayseri", Name = "Erkilet", Code = "ASR" },
                        new Airport { City = "Samsun", Name = "Çarþamba", Code = "SZF" },
                        new Airport { City = "Van", Name = "Ferit Melen", Code = "VAN" },
                        new Airport { City = "Diyarbakýr", Name = "Diyarbakýr Havalimaný", Code = "DIY" },
                        new Airport { City = "Erzurum", Name = "Erzurum Havalimaný", Code = "ERZ" },
                        new Airport { City = "Konya", Name = "Konya Havalimaný", Code = "KYA" },
                        new Airport { City = "Hatay", Name = "Hatay Havalimaný", Code = "HTY" },
                        new Airport { City = "Malatya", Name = "Erhaç Havalimaný", Code = "MLX" },
                        new Airport { City = "Elazýð", Name = "Elazýð Havalimaný", Code = "EZS" },
                        new Airport { City = "Þanlýurfa", Name = "GAP Havalimaný", Code = "GNY" },
                        new Airport { City = "Mardin", Name = "Prof. Dr. Aziz Sancar", Code = "MQM" },
                        new Airport { City = "Batman", Name = "Batman Havalimaný", Code = "BAL" },
                        new Airport { City = "Sivas", Name = "Nuri Demirað", Code = "VAS" },
                        new Airport { City = "Erzincan", Name = "Yýldýrým Akbulut", Code = "ERC" },
                        new Airport { City = "Kars", Name = "Harakani Havalimaný", Code = "KSY" },
                        new Airport { City = "Aðrý", Name = "Ahmed-i Hani", Code = "AJI" },
                        new Airport { City = "Iðdýr", Name = "Þehit Bülent Aydýn", Code = "IGD" },
                        new Airport { City = "Hakkari", Name = "Yüksekova Selahaddin Eyyubi", Code = "YKO" },
                        new Airport { City = "Sýrnak", Name = "Þerafettin Elçi", Code = "NKT" },
                        new Airport { City = "Bingöl", Name = "Bingöl Havalimaný", Code = "BGG" },
                        new Airport { City = "Muþ", Name = "Sultan Alparslan", Code = "MSR" },
                        new Airport { City = "Kahramanmaraþ", Name = "Kahramanmaraþ Havalimaný", Code = "KCM" },
                        new Airport { City = "Adýyaman", Name = "Adýyaman Havalimaný", Code = "ADF" },
                        new Airport { City = "Tokat", Name = "Tokat Havalimaný", Code = "TJK" },
                        new Airport { City = "Denizli", Name = "Çardak Havalimaný", Code = "DNZ" },
                        new Airport { City = "Isparta", Name = "Süleyman Demirel", Code = "ISE" },
                        new Airport { City = "Nevþehir", Name = "Kapadokya Havalimaný", Code = "NAV" },
                        new Airport { City = "Balýkesir", Name = "Koca Seyit", Code = "EDO" },
                        new Airport { City = "Canakkale", Name = "Çanakkale Havalimaný", Code = "CKZ" },
                        new Airport { City = "Kocaeli", Name = "Cengiz Topel", Code = "KCO" },
                        new Airport { City = "Bursa", Name = "Yeniþehir", Code = "YEI" },
                        new Airport { City = "Tekirdað", Name = "Çorlu Atatürk", Code = "TEQ" },
                        new Airport { City = "Kütahya", Name = "Zafer Havalimaný", Code = "KZR" },
                        new Airport { City = "Sinop", Name = "Sinop Havalimaný", Code = "SIC" },
                        new Airport { City = "Kastamonu", Name = "Kastamonu Havalimaný", Code = "KFS" },
                        new Airport { City = "Amasya", Name = "Merzifon", Code = "MZH" },
                        new Airport { City = "Zonguldak", Name = "Çaycuma", Code = "ONQ" },
                        new Airport { City = "Siirt", Name = "Siirt Havalimaný", Code = "SXZ" },
                        new Airport { City = "Uþak", Name = "Uþak Havalimaný", Code = "USQ" }
                    );
                    db.SaveChanges();
                }
            }
            Application.Run(new LoginForm());
        }
    }
}