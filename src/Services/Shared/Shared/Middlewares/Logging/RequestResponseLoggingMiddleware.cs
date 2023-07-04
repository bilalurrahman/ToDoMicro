
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;
public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public RequestResponseLoggingMiddleware(RequestDelegate next, 
        ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        //var originalBodyStream = context.Response.Body;
        //var request = await GetRequestAsTextAsync(context.Request);

        //_logger.LogInformation(request);

        //await using var responseBody = new MemoryStream();
        //context.Response.Body = responseBody;
        //await _next(context);

        //var response = await GetResponseAsTextAsync(context.Response);
        //_logger.LogInformation(response);

        //await responseBody.CopyToAsync(originalBodyStream);

        var orignalBodyStream = context.Request.Body;
        _logger.LogInformation($"Query Keys:{context.Request.QueryString}");
        //MemoryStream requestBody = new MemoryStream();
        //await context.Request.Body.CopyToAsync(requestBody);
        //requestBody.Seek(0, SeekOrigin.Begin);
        //String requestText = await new StreamReader(requestBody).ReadToEndAsync();
        //requestBody.Seek(0, SeekOrigin.Begin);
        string requestBody = "";
        using (StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8))
        {
            requestBody = await reader.ReadToEndAsync();
        }

      // 
        string responseBody = "";
        //using (StreamReader reader = new StreamReader(context.Response.Body, Encoding.UTF8))
        //{
        //    responseBody = await reader.ReadToEndAsync();
        //}

        //await context.Response.Body.CopyToAsync(orignalBodyStream);
        //  await responseBody.CopyToAsync(orignalBodyStream);

        _logger.LogInformation($"Request:{requestBody}");
        //_logger.LogInformation($"Response:{responseBody}");

        await _next.Invoke(context);
    }

    private async Task<string> GetRequestAsTextAsync(HttpRequest request)
    {
        var body = request.Body;
        
        //Set the reader for the request back at the beginning of its stream.
        //request.EnableBuffering();

        //Read request stream
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];

        //Copy into  buffer.
        await request.Body.ReadAsync(buffer, 0, buffer.Length);

        //Convert the byte[] into a string using UTF8 encoding...
        var bodyAsText = Encoding.UTF8.GetString(buffer);

        //Assign the read body back to the request body
        request.Body = body;

        return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
    }

    private async Task<string> GetResponseAsTextAsync(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        //Create stream reader to write entire stream
        var text = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);

        return text;
    }




}
