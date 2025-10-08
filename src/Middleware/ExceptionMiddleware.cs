using IceCreamWorld.Errors;

namespace IceCreamWorld.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BadRequestException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString() ?? string.Empty)
                    : new ApiException(context.Response.StatusCode, ex.Message, "Bad Request");

                await context.Response.WriteAsJsonAsync(response);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;

                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString() ?? string.Empty)
                    : new ApiException(context.Response.StatusCode, ex.Message, "Not found");

                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex) // general errors
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString() ?? string.Empty)
                    : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

                await context.Response.WriteAsJsonAsync(response);
            }
        }




    }
}