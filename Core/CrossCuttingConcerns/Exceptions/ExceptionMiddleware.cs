﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpExceptionHandler _httpExceptionHandler;
        //private readonly IHttpContextAccessor _contextAccessor;
       // private readonly LoggerServiceBase _loggerService;

        public ExceptionMiddleware(RequestDelegate next/*,
            IHttpContextAccessor contextAccessor, /LoggerServiceBase loggerService*/)
        {
            _next = next;
            _httpExceptionHandler = new HttpExceptionHandler();
           // _contextAccessor = contextAccessor;
           // _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            //catch (ValidationException validationException)
            //{
            //    context.Response.StatusCode = 400; // Bad Request
            //    context.Response.ContentType = "application/json";
            //    await context.Response.WriteAsync(validationException.Message);
            //}
            catch (Exception exception)
            {
               // await LogException(context, exception);
                await HandleExceptionAsync(context.Response, exception);
            }

        

            // Do everything that your middleware needs to do
            // When finished, call the next delegate/middleware in the pipeline.
            // await _next(context);
        }

        //private Task LogException(HttpContext context, Exception exception)
        //{
        //    List<LogParameter> logParameters = new()
        //    {
        //        new LogParameter{Type=context.GetType().Name, Value=exception.ToString()}
        //    };

        //    LogDetailWithException logDetail = new()
        //    {
        //        ExceptionMessage = exception.Message,
        //        MethodName = _next.Method.Name,
        //        Parameters = logParameters,
        //        User = _contextAccessor.HttpContext?.User.Identity?.Name ?? "?"
        //    };

        //    _loggerService.Error(JsonSerializer.Serialize(logDetail));

        //    return Task.CompletedTask;
        //}

        private Task HandleExceptionAsync(HttpResponse response, Exception exception)
        {
            response.ContentType = "application/json";
            _httpExceptionHandler.Response = response;
            return _httpExceptionHandler.HandleExceptionAsync(exception);
        }
    
}
}
