using Grpc.Core;
using gRPCServer01;

namespace gRPCServer01.Services;

public class CalculatorService : Calculator.CalculatorBase
{
    public override Task<DoubleResponse> Sum(
        BinaryRequest request, ServerCallContext context)
    {
        return Task.FromResult(new DoubleResponse
        {
            Result = request.X + request.Y
        });
    }

    public override Task<DoubleResponse> Sub(
        BinaryRequest request, ServerCallContext context)
    {
        return Task.FromResult(new DoubleResponse
        {
            Result = request.X - request.Y
        });
    }

    public override Task<DoubleResponse> Mul(
        BinaryRequest request, ServerCallContext context)
    {
        return Task.FromResult(new DoubleResponse
        {
            Result = request.X * request.Y
        });
    }

    public override Task<DoubleResponse> Div(
        BinaryRequest request, ServerCallContext context)
    {
        if (request.Y == 0)
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, "Division by zero"));
        }

        return Task.FromResult(new DoubleResponse
        {
            Result = request.X / request.Y
        });
    }

    public override Task<IntResponse> Fact(
        FactorialRequest request, ServerCallContext context)
    {
        if (request.X < 0)
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument,
                    "Factorial of a negative number is undefined"));
        }

        int result = 1;

        try
        {
            for (int i = 2; i <= request.X; i++)
            {
                result = checked(result * i);
            }
        }
        catch (OverflowException)
        {
            throw new RpcException(
                new Status(StatusCode.OutOfRange,
                    "Factorial exceeds Int32 range"));
        }

        return Task.FromResult(new IntResponse
        {
            Result = result
        });
    }
}