using AspDotNetLabs.Classes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();






app.UseFileServer();

app.UseFileServer(new FileServerOptions
{
    EnableDirectoryBrowsing = true,
    FileProvider = new PhysicalFileProvider(
Path.Combine(Directory.GetCurrentDirectory(), @"static"))
});
app.UseStaticFiles();



app.MapGet("/home", () => "Home Page");
app.MapGet("/home/index", () => "Home Page Index");
app.MapGet("/home/about", (contex) => contex.Response.WriteAsync(File.ReadAllText("./static/files/about.html")));


app.UseRouting();

app.UseMiddleware<LoggerMiddleware>();
app.UseMiddleware<SecretMiddleware>();


app.Use(async (context, next) => {


    
    await next(context);
    if (context.Response.StatusCode==404&&!context.Request.Path.Value.Contains("secret"))
    {
        await context.Response.WriteAsync(File.ReadAllText("./wwwroot/404.html"));
        return;
    }

});



app.Run();
