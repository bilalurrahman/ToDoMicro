
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
        var request = context.Request;
        var requestDetails = $"Request: {request.Method} {request.Path}{request.QueryString}";

        //_logger.LogInformation(requestDetails);

        var requestBodyCopy = string.Empty;
        if (request.ContentLength != null && request.ContentLength > 0)
        {
           
            using (var reader = new StreamReader(request.Body))
            {
                requestBodyCopy = await reader.ReadToEndAsync();
               // _logger.LogInformation($"Request Body:{requestBodyCopy}");                
            }
            var requestBodyStream = new MemoryStream(Encoding.UTF8.GetBytes(requestBodyCopy));
            context.Request.Body = requestBodyStream;

            // Reset the position of the request body stream
            context.Request.Body.Position = 0; // Reset the request body position for further processing

            // Process the request body copy or use requestBodyCopy as needed
        }

        _logger.LogInformation($"{requestDetails} | Request Body:{requestBodyCopy}");

        //MemoryStream requestBody = new MemoryStream();
        //await request.Body.CopyToAsync(requestBody);



        //_logger.LogInformation(requestText);

        // Capture the response
        var originalBodyStream = context.Response.Body;
        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;

            await _next.Invoke(context);

            // Log the response details
            var response = context.Response;
            //var responseDetails = ;
            //_logger.LogInformation(responseDetails);

            // Log the response body
            responseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();
            _logger.LogInformation($"Response Status: {response.StatusCode} | Response: {responseBodyText}");

            // Copy the response body to the original stream
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }

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
