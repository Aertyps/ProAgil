using Microsoft.EntityFrameworkCore;
using Proagil.API.Model;

namespace Proagil.API.Data
{
    public class DataContext:DbContext
    {
        
        public DataContext(DbContextOptions<DataContext>options):base(options){

        }

        public DbSet<evento>Eventos{get;set;}
    }
}