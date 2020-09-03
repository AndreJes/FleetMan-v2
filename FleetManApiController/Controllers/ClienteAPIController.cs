using FleetManApiController.Configuration;
using FleetManDAL.DAOs;
using FleetManModel.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace FleetManApiController.Controllers
{
    public class ClienteAPIController : ApiController
    {
        ClienteDAO clienteDAO = new ClienteDAO();

        [HttpGet]
        public async Task<HttpResponseMessage> GetAll()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                List<Cliente> clientes = await clienteDAO.GetAllAsync();

                response.Content = new ObjectContent(typeof(List<Cliente>), clientes, JsonConfig.DefaultJsonMediaType);
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Content = new ObjectContent(typeof(Exception), ex, new JsonMediaTypeFormatter());
            }

            return response;

        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetClient(string cnpj)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Cliente cliente = await clienteDAO.GetClienteAsync(cnpj);

                if(cliente != null)
                {
                    response.Content = new ObjectContent(typeof(Cliente), cliente, JsonConfig.DefaultJsonMediaType);
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    response.Content = new StringContent("Client Not Found!");
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Content = new ObjectContent(typeof(Exception), ex, new JsonMediaTypeFormatter());
            }

            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> UpdateClient(Cliente cliente)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                await clienteDAO.UpdateCliente(cliente);
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Content = new ObjectContent(typeof(Exception), ex, new JsonMediaTypeFormatter());
            }
            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> RemoveClient(string cnpj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                await clienteDAO.RemoveClienteAsync(cnpj);
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Content = new ObjectContent(typeof(Exception), ex, new JsonMediaTypeFormatter());
            }
            return response;
        }
    }
}
