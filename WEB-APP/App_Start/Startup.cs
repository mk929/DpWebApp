using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Builder;
using Owin;
using DpWebApp.Models;
using System.Web.ApplicationServices;

[assembly: OwinStartupAttribute(typeof(DpWebApp.Startup))]
namespace DpWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
