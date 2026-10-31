using DAL004;
namespace ASPA005_2
{
    public class SurnameFilter : IEndpointFilter
    {
        public static Repository rep { get; set; }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var celebrity = context.GetArgument<Celebrity>(0);
            if (celebrity == null)
                throw new CelebrityArgumentException("Celebrity is null", 500);

            if (string.IsNullOrWhiteSpace(celebrity.Surname) || celebrity.Surname.Length < 2)
                throw new CelebrityArgumentException("Surname is null or too short (<2)", 409);

            if (rep.celebrities.Any(c => c.Surname == celebrity.Surname))
                throw new CelebrityArgumentException("This surname already exists", 409);

            return await next(context);
        }
    }

    public class PhotoExistFilter : IEndpointFilter
    {
        public static Repository Rep { get; set; }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var celebrity = context.GetArgument<Celebrity>(0);
            if (celebrity == null)
                throw new CelebrityArgumentException("Celebrity is null", 500);

            if (!File.Exists(Path.Combine(Rep.BasePath, celebrity.PhotoPath)))
                context.HttpContext.Response.Headers["X-Celebrity"] = $"NotFound = {celebrity.PhotoPath}";

            return await next(context);
        }
    }


    public class DeleteCelebrityFilter : IEndpointFilter
    {
        public static Repository Rep { get; set; }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var id = context.GetArgument<int>(0);

            if (Rep.GetCelebrityById(id) == null)
                throw new DelByIdException($"DELETE error: Celebrity with Id = {id} not found");

            return await next(context);
        }
    }

    public class UpdateCelebrityFilter : IEndpointFilter
    {
        public static Repository Rep { get; set; }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var id = context.GetArgument<int>(0);
            var celebrity = context.GetArgument<Celebrity>(1);

            if (Rep.GetCelebrityById(id) == null)
                throw new UpdByIdExeption($"UPDATE error: Celebrity with Id = {id} not found");

            if (string.IsNullOrWhiteSpace(celebrity.Surname) || celebrity.Surname.Length < 2)
                throw new CelebrityArgumentException("Invalid surname", 400);

            return await next(context);
        }
    }

}
