using Microsoft.AspNetCore.Mvc;

namespace UseForwardedHeadersExample.Controllers
{
    [Route("api")]
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var connectionInfo = new ConnectionInfo
            {
                LocalIpAddress = HttpContext.Connection.LocalIpAddress.ToString(),
                LocalPort = HttpContext.Connection.LocalPort,
                RemoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString(),
                RemotePort = HttpContext.Connection.RemotePort,
                RequestHost = HttpContext.Request.Host.ToString(),
                RequestScheme = HttpContext.Request.Scheme
            };

            return Ok(connectionInfo);
        }
    }

    public class ConnectionInfo
    {
        public string LocalIpAddress { get; set; }
        public int LocalPort { get; set; }
        public string RemoteIpAddress { get; set; }
        public int RemotePort { get; set; }
        public string RequestHost { get; set; }
        public string RequestScheme { get; set; }
    }
}