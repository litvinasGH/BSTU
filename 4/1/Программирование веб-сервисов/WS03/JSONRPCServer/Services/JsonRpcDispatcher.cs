
using System.Text.Json;
using JSONRPCServer.Models;

namespace JSONRPCServer.Services;

public class JsonRpcDispatcher
{
    private readonly CalculatorService _calculator;

    public JsonRpcDispatcher(CalculatorService calculator)
    {
        _calculator = calculator;
    }

    public async Task<object?> ProcessAsync(JsonElement request)
    {
        // Пакетная обработка
        if (request.ValueKind == JsonValueKind.Array)
        {
            if (request.GetArrayLength() == 0)
            {
                return CreateError(null, -32600, "Invalid Request");
            }

            var responses = new List<object>();

            foreach (var item in request.EnumerateArray())
            {
                var response = ProcessSingle(item);

                if (response != null)
                    responses.Add(response);
            }

            // Если все запросы были уведомлениями
            if (responses.Count == 0)
                return null;

            return responses;
        }

        return ProcessSingle(request);
    }

    private object? ProcessSingle(JsonElement request)
    {
        object? id = null;
        bool hasId = false;

        if (request.ValueKind != JsonValueKind.Object)
            return CreateError(null, -32600, "Invalid Request");

        // Получаем ID
        if (request.TryGetProperty("id", out var idElement))
        {
            hasId = true;

            if (idElement.ValueKind != JsonValueKind.Null)
                id = idElement.Clone();
        }

        // Проверяем версию протокола
        if (!request.TryGetProperty("jsonrpc", out var version) ||
            version.GetString() != "2.0")
        {
            return CreateError(id, -32600, "Invalid Request");
        }

        // Получаем название метода
        if (!request.TryGetProperty("method", out var methodElement) ||
            methodElement.ValueKind != JsonValueKind.String)
        {
            return CreateError(id, -32600, "Invalid Request");
        }

        string method = methodElement.GetString()!;

        if (method.StartsWith("rpc."))
            return CreateError(id, -32601, "Method not found");

        JsonElement parameters = default;

        request.TryGetProperty("params", out parameters);

        try
        {
            double x;
            double y;
            object? result;

            switch (method.ToUpper())
            {
                case "SUM":
                    (x, y) = ReadBinaryParams(parameters);
                    result = _calculator.SUM(x, y);
                    break;

                case "SUB":
                    (x, y) = ReadBinaryParams(parameters);
                    result = _calculator.SUB(x, y);
                    break;

                case "MUL":
                    (x, y) = ReadBinaryParams(parameters);
                    result = _calculator.MUL(x, y);
                    break;

                case "DIV":
                    (x, y) = ReadBinaryParams(parameters);
                    result = _calculator.DIV(x, y);
                    break;

                case "FACT":
                    int number = ReadFactParam(parameters);
                    result = _calculator.FACT(number);
                    break;

                default:
                    return CreateError(id, -32601, "Method not found");
            }

            // Уведомления не получают ответ
            if (!hasId)
                return null;

            return CreateResult(id, result);
        }
        catch (FormatException)
        {
            if (!hasId)
                return null;

            return CreateError(id, -32602, "Invalid params");
        }
        catch (InvalidOperationException)
        {
            if (!hasId)
                return null;

            return CreateError(id, -32602, "Invalid params");
        }
        catch (ArgumentException ex)
        {
            if (!hasId)
                return null;

            return CreateError(id, -32602, ex.Message);
        }
        catch (DivideByZeroException)
        {
            if (!hasId)
                return null;

            return CreateError(id, -32000, "Division by zero");
        }
        catch (OverflowException)
        {
            if (!hasId)
                return null;

            return CreateError(id, -32001, "Integer overflow");
        }
    }

    private (double x, double y) ReadBinaryParams(JsonElement parameters)
    {
        if (parameters.ValueKind == JsonValueKind.Array &&
            parameters.GetArrayLength() == 2)
        {
            return (
                parameters[0].GetDouble(),
                parameters[1].GetDouble()
            );
        }

        if (parameters.ValueKind == JsonValueKind.Object &&
            parameters.TryGetProperty("x", out var x) &&
            parameters.TryGetProperty("y", out var y))
        {
            return (x.GetDouble(), y.GetDouble());
        }

        throw new FormatException();
    }

    private int ReadFactParam(JsonElement parameters)
    {
        if (parameters.ValueKind == JsonValueKind.Array &&
            parameters.GetArrayLength() == 1)
        {
            return parameters[0].GetInt32();
        }

        if (parameters.ValueKind == JsonValueKind.Object &&
            parameters.TryGetProperty("x", out var x))
        {
            return x.GetInt32();
        }

        throw new FormatException();
    }

    private object CreateResult(object? id, object? result)
    {
        return new
        {
            jsonrpc = "2.0",
            result,
            id
        };
    }

    private object CreateError(object? id, int code, string message)
    {
        return new
        {
            jsonrpc = "2.0",
            error = new
            {
                code,
                message
            },
            id
        };
    }
}
