namespace FleetManDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DadosLogin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoginData",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Login = c.String(unicode: false),
                        Password = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Email);
            
            AddColumn("dbo.Cliente", "Email", c => c.String(maxLength: 128, storeType: "nvarchar"));
            CreateIndex("dbo.Cliente", "Email");
            AddForeignKey("dbo.Cliente", "Email", "dbo.LoginData", "Email");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cliente", "Email", "dbo.LoginData");
            DropIndex("dbo.Cliente", new[] { "Email" });
            DropColumn("dbo.Cliente", "Email");
            DropTable("dbo.LoginData");
        }
    }
}
