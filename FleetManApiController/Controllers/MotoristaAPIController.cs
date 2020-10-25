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
    public class MotoristaAPIController : ApiController
    {
        MotoristaDAO MotoristaDAO = new MotoristaDAO();

        Logger Logger = LoggerManager.GetInstance().Loggers["MainLogger"];

        [HttpGet]
        public async Task<HttpResponseMessage> GetAll(string cnpj)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                List<Motorista> motoristas = await MotoristaDAO.GetAllAsync(cnpj);
                response.Content = new ObjectContent(typeof(List<Motorista>), motoristas, JsonConfig.DefaultJsonMediaType);
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
        public async Task<HttpResponseMessage> GetMotorista(string cpf)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Motorista motorista = await MotoristaDAO.GetMotoristaAsync(cpf);
                response.Content = new ObjectContent(typeof(Motorista), motorista, JsonConfig.DefaultJsonMediaType);
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
        public async Task<HttpResponseMessage> AddMotorista(Motorista motorista)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                await MotoristaDAO.AddMotoristaAsync(motorista);
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
        public async Task<HttpResponseMessage> UpdateMotorista(Motorista motorista)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                await MotoristaDAO.UpdateMotoristaAsync(motorista);
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
        public async Task<HttpResponseMessage> RemoveMotorista(string cpf)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                await MotoristaDAO.RemoveMotoristaAsync(cpf);
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
