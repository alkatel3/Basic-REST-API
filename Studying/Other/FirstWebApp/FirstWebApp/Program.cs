namespace FirstWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");
            //app.UseWelcomePage();

            //app.Run(async (context)=> await context.Response.WriteAsync("Hello ASP.NET"));

            //int x = 2;
            //app.Run(async (context) =>
            //{
            //    x = x * 2;
            //    await context.Response.WriteAsync($"Result: {x}");
            //});

            //app.Run(async (context)=>
            //{
            //    var response=context.Response;
            //    response.Headers.ContentLanguage = "ua-UA";
            //    response.Headers.ContentType = "text/plain; charset=utf-8";
            //    response.Headers.Append("secret-id", "256");
            //    await response.WriteAsync("Привіт ASP.NET");
            //});

            //app.Run(async (context) =>
            //{
            //    context.Response.StatusCode = 404;
            //    await context.Response.WriteAsync("Resourse Not Found");
            //});

            //app.Run(async (context) =>
            //{
            //    var response = context.Response;
            //    response.ContentType = "text/html; charset=utf-8";
            //    await response.WriteAsync("<h2>Hello METANIT.COM</h2><h3>Welcom to ASP.NET Core</h3>");
            //});

            //app.Run(async (context) =>
            //{
            //    context.Response.ContentType = "text/html; charset=utf-8";
            //    var stringBuilder = new System.Text.StringBuilder("<table>");
            //    foreach (var header in context.Request.Headers)
            //    {
            //        stringBuilder.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
            //    }
            //    stringBuilder.Append("</table>");
            //    await context.Response.WriteAsync(stringBuilder.ToString());
            //});

            app.Run(async (context) =>
            {
                var acceptHeaderValue = context.Request.Headers.Accept;
                await context.Response.WriteAsync($"Accept: {acceptHeaderValue}");
            });

            app.Run();
        }
    }
}