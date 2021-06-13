using FleetManApiController.Configuration;
using FleetManApiController.Services;
using FleetManDAL.DAOs;
using FleetManModel.Classes;
using SimpleWPFLogger.Enums;
using SimpleWPFLogger.Model;
using SimpleWPFLogger.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Windows.Documents;

namespace FleetManApiController.Controllers
{
    [Authorize]
    public class VeiculoAPIController : ApiController
    {
        VeiculoDAO VeiculoDAO = new VeiculoDAO();

        Logger Logger = LoggerManager.GetInstance().Loggers["MainLogger"];

        [HttpGet]
        public async Task<HttpResponseMessage> GetAll(string cnpj)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                List<Veiculo> veiculos = await VeiculoDAO.GetAllAsync(cnpj);
                response.Content = new ObjectContent(typeof(List<Veiculo>), veiculos, JsonConfig.DefaultJsonMediaType);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Content = new ObjectContent(typeof(Exception), ex, JsonConfig.DefaultJsonMediaType);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            ApiLog(response.StatusCode);

            return response;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetVeiculo(string placa)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Veiculo veiculo = await VeiculoDAO.GetVeiculoAsync(placa);
                response.Content = new ObjectContent(typeof(Veiculo), veiculo, JsonConfig.DefaultJsonMediaType);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Content = new ObjectContent(typeof(Exception), ex, JsonConfig.DefaultJsonMediaType);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            ApiLog(response.StatusCode);

            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddVeiculo(Veiculo veiculo)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                await VeiculoDAO.AddVeiculoAsync(veiculo);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Content = new ObjectContent(typeof(Exception), ex, JsonConfig.DefaultJsonMediaType);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            ApiLog(response.StatusCode);

            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> UpdateVeiculo(Veiculo veiculo)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                await VeiculoDAO.UpdateVeiculoAsync(veiculo);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Content = new ObjectContent(typeof(Exception), ex, JsonConfig.DefaultJsonMediaType);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            ApiLog(response.StatusCode);

            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> RemoveVeiculo(string placa)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                await VeiculoDAO.RemoveVeiculoAsync(placa);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Content = new ObjectContent(typeof(Exception), ex, JsonConfig.DefaultJsonMediaType);
                response.StatusCode = HttpStatusCode.InternalServerError;
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
