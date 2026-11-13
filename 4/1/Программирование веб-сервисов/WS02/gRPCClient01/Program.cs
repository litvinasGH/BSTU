using Grpc.Core;
using Grpc.Net.Client;
using gRPCServer01;

class Program
{
    static void Main(string[] args)
    {
        int port = args.Length > 0
            ? int.Parse(args[0])
            : 5000;

        using var channel = GrpcChannel.ForAddress($"http://localhost:{port}");
        var client = new Calculator.CalculatorClient(channel);


        var sum = client.Sum(new BinaryRequest { X = 10, Y = 5 });
        Console.WriteLine($"SUM(10, 5) = {sum.Result}");

        var sub = client.Sub(new BinaryRequest { X = 10, Y = 5 });
        Console.WriteLine($"SUB(10, 5) = {sub.Result}");

        var mul = client.Mul(new BinaryRequest { X = 10, Y = 5 });
        Console.WriteLine($"MUL(10, 5) = {mul.Result}");

        var div = client.Div(new BinaryRequest { X = 10, Y = 5 });
        Console.WriteLine($"DIV(10, 5) = {div.Result}");

        var fact = client.Fact(new FactorialRequest { X = 5 });
        Console.WriteLine($"FACT(5) = {fact.Result}");

        try
        {
            client.Div(new BinaryRequest { X = 10, Y = 0 });
        }
        catch (RpcException ex)
        {
            Console.WriteLine($"DIV error: {ex.Status.Detail}");
        }

        try
        {
            client.Fact(new FactorialRequest { X = 13 });
        }
        catch (RpcException ex)
        {
            Console.WriteLine($"FACT error: {ex.Status.Detail}");
        }

    }
}
