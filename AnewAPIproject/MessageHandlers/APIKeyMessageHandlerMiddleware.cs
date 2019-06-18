﻿
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnewAPIproject.MessageHandlers
{
    public class APIKeyMessageHandlerMiddleware
    {
        private const string APIKeyToCheck = "5567GGH7225ASW890";

        private RequestDelegate next;

        public APIKeyMessageHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context) {

            bool validKey = false;
            var checkApiKeyExists = context.Request.Headers.ContainsKey("APIKey");
            if (!checkApiKeyExists)
            {
                if (context.Request.Headers["APIKey"].Equals(APIKeyToCheck))
                {
                    validKey = true;
                }
            }
            if (!validKey)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsync("Invalid Api Key");
            }
            else
            {
                await next.Invoke(context);
            }
        }

        
    }
    public static class MyHandlerExtentions
    {
        public static IApplicationBuilder UseAPIKeyMessageHandlerMiddleware(this IApplicationBuilder builder) {
            return builder.UseMiddleware<APIKeyMessageHandlerMiddleware>();

        }
    }
}


