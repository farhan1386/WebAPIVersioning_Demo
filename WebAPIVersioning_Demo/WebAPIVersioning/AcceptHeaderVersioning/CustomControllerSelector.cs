﻿using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebAPIVersioning_Demo.AcceptHeaderVersioning
{
    public class CustomControllerSelector : DefaultHttpControllerSelector
    {
        private readonly HttpConfiguration httpConfiguration;
        public CustomControllerSelector(HttpConfiguration configuration) : base(configuration)
        {
            this.httpConfiguration = configuration;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            // First fetch all the available Web API controllers
            var controllers = GetControllerMapping();

            // Get the controller name and the parameter values from the request URI
            var routeData = request.GetRouteData();

            // Get the controller name from route data.
            // The name of the controller in our case is "Employees"
            var controllerName = routeData.Values["controller"].ToString();

            // Set the Default version number to 1
            string versionNumber = "1";

            // Get the version number from the Accept header
            // Users can include multiple Accept headers in the request
            // Check if any of the Accept headers has a parameter with name version
            var acceptHeader = request.Headers.Accept.Where(a => a.Parameters
                                .Count(p => p.Name.ToLower() == "version") > 0);

            // If there is atleast one header with a "version" parameter
            if (acceptHeader.Any())
            {
                // Get the version parameter value from the Accept header
                versionNumber = acceptHeader.First().Parameters
                                .First(p => p.Name.ToLower() == "version").Value;
            }

            if (versionNumber == "1")
            {
                // if the version number is 1, then append V1 to the controller name.
                // So at this point the, controller name will become EmployeesV1
                controllerName = controllerName + "V1";
            }
            else
            {
                // if version number is 2, then append V2 to the controller name.
                // So at this point the controller name will become EmployeesV2
                controllerName = controllerName + "V2";
            }

            HttpControllerDescriptor controllerDescriptor;
            if (controllers.TryGetValue(controllerName, out controllerDescriptor))
            {
                return controllerDescriptor;
            }

            return null;
        }
    }
}