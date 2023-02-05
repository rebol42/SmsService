using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using SmsService.Models.Domains;

namespace SmsService.Models.Configurations
{
    class ErrorConfiguration : EntityTypeConfiguration<Error>
    {
        public ErrorConfiguration()
        {
            ToTable("dbo.Error");

            HasKey(x => x.Id);
        }
    
    }
}
