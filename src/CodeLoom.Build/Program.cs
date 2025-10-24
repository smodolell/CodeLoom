using CodeLoom.Build.Commands;
using CodeLoom.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tharga.Console;
using Tharga.Console.Commands;
using Tharga.Console.Consoles;
using Tharga.Console.Entities;
using Tharga.Console.Interfaces;

internal class Program
{
    private static void Main(string[] args)
    {
        IConsole? console = null;
        try
        {

            using (console = new ClientConsole(new ConsoleConfiguration()))
            {


                var services = new ServiceCollection();

                
                services.Scan(scan => scan
                .FromAssemblies(typeof(Program).Assembly)
                .AddClasses(classes => classes.AssignableTo<ICommand>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());


                services.AddTransient<Build_ContainerCommand>();
                services.AddTransient<Build_InventoryCommand>();
                services.AddTransient<Build_FinancialCommand>();

                // 1. Configurar Logging
                var loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Debug);
                });

                var appLogger = loggerFactory.CreateLogger<Program>();
                var configCommandLogger = loggerFactory.CreateLogger<Build_ContainerCommand>();
                services.AddSingleton(configCommandLogger); 


                services.AddCore();
                var serviceProvider = services.BuildServiceProvider();

                var command = new RootCommand(console, new CommandResolver(type => (ICommand)serviceProvider.GetRequiredService(type)));

                command.RegisterCommand<Build_ContainerCommand>();

                var commandEngine = new CommandEngine(command)
                {
                    TaskRunners = new[]
                        {
                            new TaskRunner(async (c, a) =>
                            {
                                await Task.Delay(1000, c);
                            })
                        }
                };

                commandEngine.Start(args);
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine("Fatal Error.");
            console?.OutputError(exception);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        finally
        {
            console?.Dispose();
        }
    }
}