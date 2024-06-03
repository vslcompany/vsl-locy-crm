using System.Net;
using System.Text.Json;
using vsl_crm_api.Exceptions;
using vsl_crm_api.Models.Errors;

namespace vsl_crm_api.Middlewares
{
    public class ErrorHandleMiddleware: IMiddleware
    {
        public ErrorHandleMiddleware()
        {
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var errResponse = new ErrorResponse();

            try
            {
                await next(context);

                if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden || context.Response.StatusCode == 405)
                {
                    errResponse.status = false;
                    errResponse.statusCode = context.Response.StatusCode;
                    errResponse.title = "Not permitted access";
                    errResponse.message = "You don't have permission to access";
                }

                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    errResponse.status = false;
                    errResponse.statusCode = (int)HttpStatusCode.Unauthorized;
                    errResponse.title = "Unauthorized";
                    errResponse.message = "You have to login";
                }

                // Serialize custom response object to JSON
                var json = JsonSerializer.Serialize(errResponse);

                // Set response content type
                context.Response.ContentType = "application/json";

                // Write JSON response
                await context.Response.WriteAsync(json);
            }
            catch (Exception e)
            {
                if (!context.Response.HasStarted)
                {
                    if (e is ErrorException errorException)
                    {
                        errResponse.status = false;
                        errResponse.statusCode = errorException.statusCode;
                        errResponse.title = errorException.title;
                        errResponse.message = errorException.Message;
                    }
                    else if (e is UnauthorizedAccessException)
                    {
                        errResponse.status = false;
                        errResponse.statusCode = (int)HttpStatusCode.Unauthorized;
                        errResponse.title = "Unauthorized";
                        errResponse.message = "Access denied";
                    }

                    context.Response.StatusCode = errResponse.statusCode;

                    // Serialize custom response object to JSON
                    var json = JsonSerializer.Serialize(errResponse);

                    // Write JSON response
                    await context.Response.WriteAsync(json);
                }
            }
        }
    }
}
