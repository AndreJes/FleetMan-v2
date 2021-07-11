namespace FleetManDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DefaultLoginAdmin : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "LoginData", newName: "LoginModel");
            AddColumn("LoginModel", "Role", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("LoginModel", "Role");
            RenameTable(name: "LoginModel", newName: "LoginData");
        }
    }
}
