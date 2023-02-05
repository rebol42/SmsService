using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmsService.Models.Domains;
using System.Data.Entity;

namespace SmsService.Repositories
{
    public class ErrorRepository
    {

        public List<Error> GetLastErrors()
        {
            using (var context = new ApplicationDbContext())
            {

                var errors = context.
                               Errors.Where(x => x.send == false)
                               .AsQueryable();

                return errors.ToList();



                //   new Error {Message = "Błąd testowy 1", Date = DateTime.Now},
                //   new Error {Message = "Błąd testowy 2", Date = DateTime.Now}
            };
            
        
        }
        
        public List<User> GetUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                
                var users = context.
                               Users
                               .AsQueryable();
                               
                return users.ToList();
            }
        }

        public void UpdateError(Error error)
        {
            using (var context = new ApplicationDbContext())
            {
                var errToUpdate = context.Errors.Find(error.Id);

                errToUpdate.send = true; ;
                context.SaveChanges();

            }
        }
        
    }
}
