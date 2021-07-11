using FleetManApiController.Configuration;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManApiController.Services
{
    public static class OwinService
    {
        private static IDisposable service;

        public static bool Start()
        {
            try
            {
                string serviceURL = @"http://localhost:54321/";

                service = WebApp.Start<OwinConfiguration>(serviceURL);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Stop()
        {
            try
            {
                if(service != null)
                {
                    service.Dispose();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
