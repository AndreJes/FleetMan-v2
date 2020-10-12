namespace FleetManDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaltingLoginData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoginData", "Salt", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoginData", "Salt");
        }
    }
}
