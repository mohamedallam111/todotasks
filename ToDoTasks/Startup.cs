using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToDoTasks.Startup))]
namespace ToDoTasks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
