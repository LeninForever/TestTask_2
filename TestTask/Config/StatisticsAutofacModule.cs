using Autofac;
using TestTask.Repositories.Implementation;
using TestTask.Repositories.Interfaces;
using TestTask.Services.Implementation;
using TestTask.Services.Interfaces;

namespace TestTask.Config
{
    public class StatisticsAutofacModule : Module
    {
        public IConfiguration Configuration { get; set; }

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<StatisticsRepository>()
                .As<IStatisticsRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StatisticsService>()
                .As<IStatisticsService>()
                .InstancePerLifetimeScope();
        }
    }
}
