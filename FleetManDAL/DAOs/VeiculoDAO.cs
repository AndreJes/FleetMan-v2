using FleetManDAL.Contexts;
using FleetManModel.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManDAL.DAOs
{
    public class VeiculoDAO
    {
        public VeiculoDAO()
        {
        }

        public async Task<List<Veiculo>> GetAllAsync(string cnpj)
        {
            using FleetManContext context = new FleetManContext();

            return await context.Veiculos.Where(v => v.Cliente_CNPJ == cnpj).ToListAsync();
        }

        public async Task<Veiculo> GetVeiculoAsync(string placa)
        {
            using FleetManContext context = new FleetManContext();

            return await context.Veiculos.Where(v => v.Placa == placa).FirstOrDefaultAsync();
        }

        public async Task AddVeiculoAsync(Veiculo veiculo)
        {
            try
            {
                using FleetManContext context = new FleetManContext();
                context.Veiculos.Add(veiculo);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateVeiculoAsync(Veiculo veiculo)
        {
            try
            {
                using FleetManContext context = new FleetManContext();
                AttachItem(veiculo, context);
                context.Entry(veiculo).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveVeiculoAsync(string placa)
        {
            try
            {
                using FleetManContext context = new FleetManContext();

                Veiculo veiculo = await GetVeiculoAsync(placa);

                if (veiculo != null)
                {
                    AttachItem(veiculo, context);
                    context.Veiculos.Remove(veiculo);
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AttachItem(Veiculo veiculo, FleetManContext context)
        {
            if (!context.Veiculos.Local.Contains(veiculo))
            {
                context.Veiculos.Attach(veiculo);
            }
        }
    }
}
