using SmallManagerSpace.Resources.DataBase.NewFolder1;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallManagerSpace.Resources.DataBase.BookTest
{
    class MyBookDB: DbContext
    {

        public MyBookDB() : base("name=UserCon")
        {

        }
        //定义属性，便于外部访问数据表
        public DbSet<Book> Books { get { return Set<Book>(); } }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.Entity<Book>();
            //ModelConfiguration.Configure(modelBuilder);
            //var init = new SqliteCreateDatabaseIfNotExists<MyBookDB>(modelBuilder);
            //Database.SetInitializer(init);
        }

    }

    public class ModelConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            ConfigureBookEntity(modelBuilder);
        }
        private static void ConfigureBookEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>();
        }
    }
}
