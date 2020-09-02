namespace FleetManDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "Nome", c => c.String(unicode: false));
            AddColumn("dbo.Cliente", "Endereco_Logradouro", c => c.String(unicode: false));
            AddColumn("dbo.Cliente", "Endereco_Cep", c => c.String(unicode: false));
            AddColumn("dbo.Cliente", "Endereco_Bairro", c => c.String(unicode: false));
            AddColumn("dbo.Cliente", "Endereco_Numero", c => c.String(unicode: false));
            AddColumn("dbo.Cliente", "Endereco_Complemento", c => c.String(unicode: false));
            AddColumn("dbo.Cliente", "Endereco_Cidade", c => c.String(unicode: false));
            AddColumn("dbo.Cliente", "Endereco_UF", c => c.String(unicode: false));
            AddColumn("dbo.Cliente", "Ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Motorista", "CNH", c => c.String(unicode: false));
            AddColumn("dbo.Motorista", "Nome", c => c.String(unicode: false));
            AddColumn("dbo.Motorista", "Email", c => c.String(unicode: false));
            AddColumn("dbo.Motorista", "Endereco_Logradouro", c => c.String(unicode: false));
            AddColumn("dbo.Motorista", "Endereco_Cep", c => c.String(unicode: false));
            AddColumn("dbo.Motorista", "Endereco_Bairro", c => c.String(unicode: false));
            AddColumn("dbo.Motorista", "Endereco_Numero", c => c.String(unicode: false));
            AddColumn("dbo.Motorista", "Endereco_Complemento", c => c.String(unicode: false));
            AddColumn("dbo.Motorista", "Endereco_Cidade", c => c.String(unicode: false));
            AddColumn("dbo.Motorista", "Endereco_UF", c => c.String(unicode: false));
            AddColumn("dbo.Motorista", "Ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Motorista", "Cliente_CNPJ", c => c.String(maxLength: 128, storeType: "nvarchar"));
            AddColumn("dbo.Veiculo", "Renavam", c => c.String(unicode: false));
            AddColumn("dbo.Veiculo", "Chassi", c => c.String(unicode: false));
            AddColumn("dbo.Veiculo", "Modelo", c => c.String(unicode: false));
            AddColumn("dbo.Veiculo", "Ano", c => c.String(unicode: false));
            AddColumn("dbo.Veiculo", "Marca", c => c.String(unicode: false));
            AddColumn("dbo.Veiculo", "Ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Veiculo", "Cliente_CNPJ", c => c.String(maxLength: 128, storeType: "nvarchar"));
            CreateIndex("dbo.Motorista", "Cliente_CNPJ");
            CreateIndex("dbo.Veiculo", "Cliente_CNPJ");
            AddForeignKey("dbo.Motorista", "Cliente_CNPJ", "dbo.Cliente", "CNPJ");
            AddForeignKey("dbo.Veiculo", "Cliente_CNPJ", "dbo.Cliente", "CNPJ");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Veiculo", "Cliente_CNPJ", "dbo.Cliente");
            DropForeignKey("dbo.Motorista", "Cliente_CNPJ", "dbo.Cliente");
            DropIndex("dbo.Veiculo", new[] { "Cliente_CNPJ" });
            DropIndex("dbo.Motorista", new[] { "Cliente_CNPJ" });
            DropColumn("dbo.Veiculo", "Cliente_CNPJ");
            DropColumn("dbo.Veiculo", "Ativo");
            DropColumn("dbo.Veiculo", "Marca");
            DropColumn("dbo.Veiculo", "Ano");
            DropColumn("dbo.Veiculo", "Modelo");
            DropColumn("dbo.Veiculo", "Chassi");
            DropColumn("dbo.Veiculo", "Renavam");
            DropColumn("dbo.Motorista", "Cliente_CNPJ");
            DropColumn("dbo.Motorista", "Ativo");
            DropColumn("dbo.Motorista", "Endereco_UF");
            DropColumn("dbo.Motorista", "Endereco_Cidade");
            DropColumn("dbo.Motorista", "Endereco_Complemento");
            DropColumn("dbo.Motorista", "Endereco_Numero");
            DropColumn("dbo.Motorista", "Endereco_Bairro");
            DropColumn("dbo.Motorista", "Endereco_Cep");
            DropColumn("dbo.Motorista", "Endereco_Logradouro");
            DropColumn("dbo.Motorista", "Email");
            DropColumn("dbo.Motorista", "Nome");
            DropColumn("dbo.Motorista", "CNH");
            DropColumn("dbo.Cliente", "Ativo");
            DropColumn("dbo.Cliente", "Endereco_UF");
            DropColumn("dbo.Cliente", "Endereco_Cidade");
            DropColumn("dbo.Cliente", "Endereco_Complemento");
            DropColumn("dbo.Cliente", "Endereco_Numero");
            DropColumn("dbo.Cliente", "Endereco_Bairro");
            DropColumn("dbo.Cliente", "Endereco_Cep");
            DropColumn("dbo.Cliente", "Endereco_Logradouro");
            DropColumn("dbo.Cliente", "Nome");
        }
    }
}
