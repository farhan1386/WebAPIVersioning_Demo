using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebAPIVersioning_Demo.WebAPIVersioning.CustomMediaTypesVersioning
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

            // Get the version number from the Custom media type

            // We need to use the regular expression for mataching the pattern of the 
            // media type. We have given a name for the matched group that contains
            // the version number which enables us to retrieve the version number 
            // using the group name("version") instead of ZERO based index
            string regex = @"application\/vnd\.localhost:51994\.([a-z]+)\.v(?<version>[0-9]+)\+([a-z]+)";

            // Users can include multiple Accept headers in the request.
            // So we need to check atlest if any of the Accept headers has our custom 
            // media type by checking if there is a match with regular expression specified
            var acceptHeader = request.Headers.Accept
                .Where(a => Regex.IsMatch(a.MediaType, regex, RegexOptions.IgnoreCase));

            // If there is atleast one Accept header with our custom media type
            if (acceptHeader.Any())
            {
                // Retrieve the first custom media type
                var match = Regex.Match(acceptHeader.First().MediaType, regex, RegexOptions.IgnoreCase);
                // From the version group, get the version number
                versionNumber = match.Groups["version"].Value;
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