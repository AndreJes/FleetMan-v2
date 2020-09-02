namespace FleetManDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        CNPJ = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.CNPJ);
            
            CreateTable(
                "dbo.Motorista",
                c => new
                    {
                        CPF = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.CPF);
            
            CreateTable(
                "dbo.Veiculo",
                c => new
                    {
                        Placa = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Placa);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Veiculo");
            DropTable("dbo.Motorista");
            DropTable("dbo.Cliente");
        }
    }
}
