using System.Security.Cryptography.X509Certificates;

namespace SmartMarketLibrary
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DBModel : DbContext
    {
        // Your context has been configured to use a 'DBModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'SmartMarketLibrary.DBModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DBModel' 
        // connection string in the application configuration file.
        public DBModel()
            : base("name=DBModel")
        {
            
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Change> Changes { get; set; }
        public virtual DbSet<Delivery> Deliveries { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}