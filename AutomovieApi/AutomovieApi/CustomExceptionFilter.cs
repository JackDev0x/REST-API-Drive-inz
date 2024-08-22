using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AutomovieApi
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            ProblemDetails problemDetails;

            switch (exception)
            {
                case BrandNotFoundException brandNotFoundException:
                    problemDetails = new ProblemDetails
                    {
                        Title = "Brand not found",
                        Status = (int)HttpStatusCode.NotFound,
                        Detail = brandNotFoundException.Message,
                        Instance = context.HttpContext.Request.Path
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);
                    break;

                case ModelNotFoundException modelNotFoundException:
                    problemDetails = new ProblemDetails
                    {
                        Title = "Model not found",
                        Status = (int)HttpStatusCode.NotFound,
                        Detail = modelNotFoundException.Message,
                        Instance = context.HttpContext.Request.Path
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);
                    break;
                case NotFoundException notFoundException:
                    problemDetails = new ProblemDetails
                    {
                        Title = "Resource not found",
                        Status = (int)HttpStatusCode.NotFound,
                        Detail = notFoundException.Message,
                        Instance = context.HttpContext.Request.Path
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);
                    break;

                case ServiceException serviceException:
                    problemDetails = new ProblemDetails
                    {
                        Title = "Service error",
                        Status = (int)HttpStatusCode.InternalServerError,
                        Detail = serviceException.Message,
                        Instance = context.HttpContext.Request.Path
                    };
                    context.Result = new ObjectResult(problemDetails)
                    {
                        StatusCode = problemDetails.Status
                    };
                    break;

                default:
                    problemDetails = new ProblemDetails
                    {
                        Title = "An unexpected error occurred",
                        Status = (int)HttpStatusCode.InternalServerError,
                        Detail = exception.Message,
                        Instance = context.HttpContext.Request.Path
                    };
                    context.Result = new ObjectResult(problemDetails)
                    {
                        StatusCode = problemDetails.Status
                    };
                    break;
            }

            _logger.LogError(exception, "An error occurred: {Message}", exception.Message);
            context.ExceptionHandled = true;
        }
    }
    public class BrandNotFoundException : Exception
    {
        public BrandNotFoundException(string message) : base(message) { }
    }

    public class ModelNotFoundException : Exception
    {
        public ModelNotFoundException(string message) : base(message) { }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    public class ServiceException : Exception
    {
        public ServiceException(string message, Exception innerException)
            : base(message, innerException) { }
    }


}
