using Microsoft.EntityFrameworkCore;
using webApiTest.Model;

namespace webApiTest.MyDataBaseContext
{
    public partial class MyDataBaseContext_main : DbContext
    {
        public DbSet<user> users { get; set; }
        public DbSet<student> student { get; set; }
        public virtual DbSet<t_Master> t_Master { get; set; }

        public MyDataBaseContext_main(DbContextOptions<MyDataBaseContext_main> option) : base(option)
        {

        }
    }
}
