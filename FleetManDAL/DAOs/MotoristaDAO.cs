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
    public class MotoristaDAO
    {
        public MotoristaDAO()
        {
        }

        public async Task<List<Motorista>> GetAllAsync(string cnpj)
        {
            using FleetManContext context = new FleetManContext();

            return await context.Motoristas.Where(m => m.Cliente_CNPJ == cnpj).ToListAsync();
        }

        public async Task<Motorista> GetMotoristaAsync(string cpf)
        {
            using FleetManContext context = new FleetManContext();

            return await context.Motoristas.Where(m => m.CPF == cpf).FirstOrDefaultAsync();
        }

        public async Task AddMotoristaAsync(Motorista motorista)
        {
            try
            {
                using FleetManContext context = new FleetManContext();
                context.Motoristas.Add(motorista);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateMotoristaAsync(Motorista motorista)
        {
            try
            {
                using FleetManContext context = new FleetManContext();
                AttachItem(motorista, context);
                context.Entry(motorista).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveMotoristaAsync(string cpf)
        {
            try
            {
                using FleetManContext context = new FleetManContext();

                Motorista motorista = await GetMotoristaAsync(cpf);

                if (motorista != null)
                {
                    AttachItem(motorista, context);
                    context.Motoristas.Remove(motorista);
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AttachItem(Motorista motorista, FleetManContext context)
        {
            if (!context.Motoristas.Local.Contains(motorista))
            {
                context.Motoristas.Attach(motorista);
            }
        }
    }
}
