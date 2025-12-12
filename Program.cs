using JasperFx;
using JasperFx.CodeGeneration.Model;
using Wolverine;

namespace Issue1935
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddScoped<ServiceFromServiceLocation1>();
            builder.Services.AddScoped<Service1>();

            builder.UseWolverine(options =>
            {
                options.ServiceLocationPolicy = ServiceLocationPolicy.NotAllowed;

                options.CodeGeneration
                    .AlwaysUseServiceLocationFor<ServiceFromServiceLocation1>();
            });

            var host = builder.Build();

            await host.RunJasperFxCommands(args);
        }
    }

    public record Message1();
    public record Message2();

    public class ServiceFromServiceLocation1
    {
    }

    public class Service1
    {
    }

    public class Message1Handler
    {
        public void Handle(Message1 message, Service1 service1, ServiceFromServiceLocation1 sl1)
        {
        }
    }

    public class Message2Handler
    {
        public void Handle(Message2 message, Service1 service1, ServiceFromServiceLocation1 sl1)
        {
        }
    }
}
