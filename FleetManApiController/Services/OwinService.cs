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
                string serviceURL = @"http://*:54321/";

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
                service.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
