using FleetManApiController.Configuration;
using FleetManApiController.Services;
using FleetManDAL.DAOs;
using FleetManModel.Classes;
using Newtonsoft.Json;
using SimpleWPFLogger.Enums;
using SimpleWPFLogger.Model;
using SimpleWPFLogger.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Windows.Documents;

namespace FleetManApiController.Controllers
{
    [Authorize]
    public class ClienteAPIController : ApiController
    {
        ClienteDAO clienteDAO = new ClienteDAO();

        Logger Logger = LoggerManager.GetInstance().Loggers["MainLogger"];

        [HttpGet]
        [Authorize(Roles = "Admin")]
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
                response.Content = new ObjectContent(typeof(Exception), ex, JsonConfig.DefaultJsonMediaType);
            }

            ApiLog(response.StatusCode);

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

            ApiLog(response.StatusCode);

            return response;
        }


        [HttpPost]
        public async Task<HttpResponseMessage> AddClient(Cliente cliente)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                await clienteDAO.AddClienteAsync(cliente);
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Content = new ObjectContent(typeof(Exception), ex, new JsonMediaTypeFormatter());
            }

            ApiLog(response.StatusCode);

            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> UpdateClient(Cliente cliente)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                await clienteDAO.UpdateClienteAsync(cliente);
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Content = new ObjectContent(typeof(Exception), ex, new JsonMediaTypeFormatter());
            }

            ApiLog(response.StatusCode);

            return response;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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

            ApiLog(response.StatusCode);

            return response;
        }

        private void ApiLog(HttpStatusCode statusCode)
        {
            Logger.Dispatcher.Invoke(() => Logger.PrintText(new Run(
                    Request.Method + " - " + Request.RequestUri + Environment.NewLine
                    + "\t\t\t  Host: " + Request.Headers.Host + Environment.NewLine
                    + "\t\t\t  Status Code: " + statusCode.ToString("G"))
                , new Run(" --> ")
                , new DateOptions(TextDecorationOptions.BOLD))
            );
        }
    }
}
