namespace FleetManDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class NewIndexes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Veiculo", "Ano", c => c.Int(nullable: false));
            AlterColumn("dbo.Veiculo", "Renavam", c => c.String(nullable: false, fixedLength: true, maxLength: 11));
            AlterColumn("dbo.Veiculo", "Chassi", c => c.String(nullable: false, fixedLength: true, maxLength: 17));
            //CreateIndex("dbo.Veiculo", "Renavam", unique: true);
            //CreateIndex("dbo.Veiculo", "Chassi", unique: true);
        }

        public override void Down()
        {
            DropIndex("dbo.Veiculo", new[] { "Chassi" });
            DropIndex("dbo.Veiculo", new[] { "Renavam" });
            AlterColumn("dbo.Veiculo", "Ano", c => c.String(unicode: false));
        }
    }
}
