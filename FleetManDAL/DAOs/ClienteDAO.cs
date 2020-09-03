using System;
using FleetManModel.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FleetManDAL.Contexts;
using System.Data.Entity;

namespace FleetManDAL.DAOs
{
    public class ClienteDAO
    {
        public ClienteDAO()
        {
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            try
            {
                using FleetManContext context = new FleetManContext();

                return await context.Clientes.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Cliente> GetClienteAsync(string cnpj)
        {
            try
            {
                using FleetManContext context = new FleetManContext();

                return await context.Clientes.Where(c => c.CNPJ == cnpj).FirstAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddClienteAsync(Cliente cliente)
        {
            try
            {
                using FleetManContext context = new FleetManContext();
                context.Clientes.Add(cliente);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateCliente(Cliente cliente)
        {
            try
            {
                using FleetManContext context = new FleetManContext();
                AttachItem(cliente, context);
                context.Entry(cliente).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveClienteAsync(string cnpj)
        {
            try
            {
                using FleetManContext context = new FleetManContext();

                Cliente cliente = await GetClienteAsync(cnpj);

                if (cliente != null)
                {
                    context.Clientes.Remove(cliente);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AttachItem(Cliente cliente, FleetManContext context)
        {
            if (!context.Clientes.Local.Contains(cliente))
            {
                context.Clientes.Attach(cliente);
            }
        }
    }
}
