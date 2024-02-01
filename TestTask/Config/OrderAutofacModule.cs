using Autofac;
using TestTask.Repositories.Implementation;
using TestTask.Repositories.Interfaces;
using TestTask.Services.Implementation;
using TestTask.Services.Interfaces;

namespace TestTask.Config
{
    public class OrderAutofacModule : Module
    {
        public IConfiguration Configuration { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            int defaultPageSize = Configuration.GetValue<int>("Settings:DefaultOrderPageSize");

            builder.RegisterType<OrderRepository>()
                .As<IOrderRepository>()
                .WithParameter("defaultLimit", defaultPageSize)
                .InstancePerLifetimeScope();

            builder.RegisterType<OrderService>()
                .As<IOrderService>()
                .InstancePerLifetimeScope();
        }
    }
}

