using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Builder;
using Owin;
using DpWebApp.Models;
using System.Web.ApplicationServices;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartupAttribute(typeof(DpWebApp.Startup))]
namespace DpWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User 
            if (!roleManager.RoleExists("Admin"))
            {
                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website	
                var user = UserManager.FindByName("officer@admin.com");		
                if ( user == null)
                {
                    user = new ApplicationUser();
                    user.UserName = "officer@admin.com";
                    user.Email = "officer@admin.com";

                    string userPWD = "111111";

                    var chkUser = UserManager.Create(user, userPWD);

                    //Add default User to Role Admin
                    if (chkUser.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(user.Id, "Admin");

                    }
                }
            }

            //  Creating Agent role 
            if (!roleManager.RoleExists("Agency"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Agency";
                roleManager.Create(role);

                //Here we create a test user
                var user = UserManager.FindByName("user@agency.com");
                if (user == null)
                {
                    user = new ApplicationUser();
                    user.UserName = "user@agency.com";
                    user.Email = "user@agency.com";

                    string userPWD = "111111";

                    var chkUser = UserManager.Create(user, userPWD);

                    //Add default User to Role Admin
                    if (chkUser.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(user.Id, "Agency");

                    }
                }

            }
        }
    }
}
