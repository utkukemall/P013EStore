using Microsoft.EntityFrameworkCore;
using P013EStore.Core.Entities;
using P013EStore.Data.Configurations;
using System.Reflection;

namespace P013EStore.Data
{
    public class DatabaseContext : DbContext
    {
        // Katmanlı mimaride bir proje katmanından başka bir katmana erişebilmek için bulunduğumuz data projesinin dependencies kısmına sağ tıklayıp > Add project references diyerek açılan pencereden Core projesine Tik atıp ok diyerek pencereyi kapatmamız gerekiyor.
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // OnConfiguring metodu EntityFrameworkCore ile gelir ve veritabanı bağlantı ayarlarını yapmamızı sağlar.
            optionsBuilder.UseSqlServer(@"Server=(localdb)/MSSQLLocalDB; Database=P013EStore; Trusted_Connection=True");
            // optionsBuilder.UseSqlServer(@"Server=(CanlıServerAdı; Database=CanlıdakiDatabase; Username=CanlıVeritabanıKullanıcıAdı; Password = CanlıVeriTabanıŞifre");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // FluentAPI ile veritabanı tablolarımız oluşurken veri tiplerini db kurallarını burada tanımlayabiliriz.
            modelBuilder.Entity<AppUser>().Property(a => a.Name).IsRequired().HasColumnType("varchar(50).").HasMaxLength(50);  // FluentAPI ile AppUser class ının Name Property si için oluşacak veritabanı kolonu ayarlarını bu şekilde belirleyebiliyoruz.
            modelBuilder.Entity<AppUser>().Property(a => a.Surname).IsRequired().HasColumnType("varchar(50).").HasMaxLength(50);
            modelBuilder.Entity<AppUser>().Property(a => a.UserName).IsRequired().HasColumnType("varchar(50).").HasMaxLength(50);
            modelBuilder.Entity<AppUser>().Property(a => a.Password).IsRequired().HasColumnType("varchar(50).").HasMaxLength(50);
            modelBuilder.Entity<AppUser>().Property(a => a.Email).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<AppUser>().Property(a => a.Phone).HasMaxLength(50);

            // FluentAPI HasData ile db oluştuktan sonra başlangıç kayıtları ekleme

            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id=1,
                Email="info@P013EStore.com",
                Password="123",
                UserName="Admin",
                IsActive = true,
                IsAdmin = true,
                Name = "Admin",
                UserGuid=Guid.NewGuid(), // Kullanıcıya benzersiz bir id no oluştur

            });

            //modelBuilder.ApplyConfiguration(new BrandConfigurations()); // Marka için yaptığımız konfigürasyon ayarlarını çağırdık.

            //modelBuilder.ApplyConfiguration(new CategoryConfigurations()); // Bu satırları kapatıp tek satırda hepsini çağıracağız.

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Uygulamadaki tüm Configurations class larını burada çalıştır.

            // Fluent Validation : data annotationdaki hata mesajları vb işlemlerini yönetebileceğimiz 3.parti paket.

            base.OnModelCreating(modelBuilder);
        }
    }
}