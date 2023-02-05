namespace SmsService
{
    using System.Data.Entity;
    using SmsService.Models.Domains;
    using SmsService.Models.Configurations;




    public class ApplicationDbContext : DbContext
    {
       
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {


        }

        //DbSet - klasa generyczna , która na podstawie klas Domains bedzie wiedziala jakie ma utworzyc tabele
        public DbSet<Error> Errors { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ErrorConfiguration());
        }


    }


}