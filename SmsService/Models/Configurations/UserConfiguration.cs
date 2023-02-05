using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using SmsService.Models.Domains;

namespace SmsService.Models.Configurations
{
    class UserConfiguration : EntityTypeConfiguration<Error>
    {
        public UserConfiguration()
        {
            ToTable("dbo.User");

            HasKey(x => x.Id);
        }
    }
}
