using Autofac;
using TestTask.Repositories.Implementation;
using TestTask.Repositories.Interfaces;
using TestTask.Services.Implementation;
using TestTask.Services.Interfaces;

namespace TestTask.Config
{
    public class ClientAutofacModule : Module
    {
        public IConfiguration Configuration { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            int defaultPageSize = Configuration.GetValue<int>("Settings:DefaultClientPageSize");

            builder.RegisterType<ClientRepository>()
                .As<IClientRepository>()
                .WithParameter("defaultLimit", defaultPageSize)
                .InstancePerLifetimeScope();

            builder.RegisterType<ClientService>()
                .As<IClientService>()
                .InstancePerLifetimeScope();
        }
    }
}
