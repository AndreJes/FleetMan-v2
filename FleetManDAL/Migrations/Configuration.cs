namespace FleetManDAL.Migrations
{
    using FleetManModel.Classes;
    using FleetManModel.Security;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FleetManDAL.Contexts.FleetManContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FleetManDAL.Contexts.FleetManContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            Endereco endpadrao = new Endereco() { Logradouro = "Rua um", Bairro = "Penha", Cep = "00000-000", Cidade = "São Paulo", Complemento = "", Numero = "100", UF = "SP" };

            var hasher = new FleetManHash();

            string salt1 = hasher.GetSalt();
            string salt2 = hasher.GetSalt();

            List<LoginData> logins = new List<LoginData>()
            {
                new LoginData(){Email="cliente1@gmail.com", Login="12345678900001", Password=hasher.GetHashedPassword("123456789", salt1), Salt=salt1},
                new LoginData(){Email="cliente2@gmail.com", Login="12345678900002", Password=hasher.GetHashedPassword("123456789", salt2), Salt=salt2}
            };

            context.LoginDatas.AddOrUpdate(logins.ToArray());
            context.SaveChanges();

            List<Cliente> clientes = new List<Cliente>()
            {
                new Cliente() {Ativo = true, Nome = "Cliente 1", CNPJ="12345678900001", Endereco=endpadrao, Email="cliente1@gmail.com"},
                new Cliente() {Ativo = false, Nome = "Cliente 2", CNPJ="12345678900002", Endereco=endpadrao, Email="cliente2@gmail.com"}
            };

            context.Clientes.AddOrUpdate(clientes.ToArray());
            context.SaveChanges();

            List<Veiculo> veiculos = new List<Veiculo>()
            {
                new Veiculo() {Ativo = true, Placa="JKU-2324", Ano=2002, Marca="Ford", Modelo="Focus", Renavam="01085793408", Chassi="BHP0RXR54AKNJLEZW", Cliente=clientes[0]},
                new Veiculo() {Ativo = false, Placa="JHU-2134", Ano=2002, Marca="Ford", Modelo="Focus", Renavam="01085793407", Chassi="BHP0RXR54AKNJLEZZ", Cliente=clientes[0]},
                new Veiculo() {Ativo = true, Placa="JOU-2214", Ano=2002, Marca="Ford", Modelo="Focus", Renavam="01085793405", Chassi="BHP0RXR54AKNJLEZY", Cliente=clientes[1]},
                new Veiculo() {Ativo = false, Placa="JKK-1324", Ano=2002, Marca="Ford", Modelo="Focus", Renavam="01085793404", Chassi="BHP0RXR54AKNJLEZX", Cliente=clientes[1]}
            };

            context.Veiculos.AddOrUpdate(veiculos.ToArray());
            context.SaveChanges();

            List<Motorista> motoristas = new List<Motorista>()
            {
                new Motorista() {Ativo = true, Nome="João", CPF="12345678900", CNH="98765432101", Email="Joao@gmail.com", Endereco = endpadrao, Cliente=clientes[0]},
                new Motorista() {Ativo = false, Nome="Maria", CPF="12345678901", CNH="98765432102", Email="Maria@gmail.com", Endereco = endpadrao, Cliente=clientes[0]},
                new Motorista() {Ativo = false, Nome="Jorge", CPF="12345678902", CNH="98765432103", Email="Jorge@gmail.com", Endereco = endpadrao, Cliente=clientes[1]},
                new Motorista() {Ativo = true, Nome="Ana", CPF="12345678903", CNH="98765432104", Email="Ana@gmail.com", Endereco = endpadrao, Cliente=clientes[1]}
            };

            context.Motoristas.AddOrUpdate(motoristas.ToArray());
            context.SaveChanges();

        }
    }
}
