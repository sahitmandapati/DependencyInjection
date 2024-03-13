using Microsoft.Extensions.DependencyInjection;

public interface IService
{
    void PrintMessage();
}

public class SingletonService : IService
{
    private readonly int _number;

    public SingletonService()
    {
        _number = new Random().Next();
    }

    public void PrintMessage()
    {
        Console.WriteLine($"Singleton: Hello {_number}");
    }
}

public class ScopedService : IService
{
    private readonly int _number;

    public ScopedService()
    {
        _number = new Random().Next();
    }

    public void PrintMessage()
    {
        Console.WriteLine($"Scoped: Hello {_number}");
    }
}

public class TransientService : IService
{
    private readonly int _number;

    public TransientService()
    {
        _number = new Random().Next();
    }

    public void PrintMessage()
    {
        Console.WriteLine($"Transient: Hello {_number}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddSingleton<SingletonService>();
        services.AddScoped<ScopedService>();
        services.AddTransient<TransientService>();

        var serviceProvider = services.BuildServiceProvider();

        for (int i = 0; i < 3; i++)
        {
            using (var scope = serviceProvider.CreateScope())
            {

                var singleton1 = scope.ServiceProvider.GetService<SingletonService>();
                var singleton2 = scope.ServiceProvider.GetService<SingletonService>();
                var scoped1 = scope.ServiceProvider.GetService<ScopedService>();
                var scoped2 = scope.ServiceProvider.GetService<ScopedService>();
                var transient1 = scope.ServiceProvider.GetService<TransientService>();
                var transient2 = scope.ServiceProvider.GetService<TransientService>();


                Console.WriteLine("1-----");

                singleton1.PrintMessage();
                scoped1.PrintMessage();
                transient1.PrintMessage();


                Console.WriteLine("2-----");

                singleton2.PrintMessage();
                scoped2.PrintMessage();
                transient2.PrintMessage();

                Console.WriteLine("-----");
            }
        }
    }


}

