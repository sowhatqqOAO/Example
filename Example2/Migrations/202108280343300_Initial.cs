namespace Example2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.String(nullable: false, maxLength: 5),
                        CompanyName = c.String(nullable: false, maxLength: 40),
                        ContactName = c.String(nullable: false, maxLength: 30),
                        ContactTitle = c.String(nullable: false, maxLength: 30),
                        Address = c.String(nullable: false, maxLength: 60),
                        City = c.String(nullable: false, maxLength: 15),
                        Region = c.String(nullable: false, maxLength: 30),
                        PostalCode = c.String(nullable: false, maxLength: 10),
                        Country = c.String(nullable: false, maxLength: 15),
                        Phone = c.String(maxLength: 24),
                        Fax = c.String(maxLength: 24),
                    })
                .PrimaryKey(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}
