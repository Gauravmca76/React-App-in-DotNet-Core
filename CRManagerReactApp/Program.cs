var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseStaticFiles();// Serves static files from wwwroot

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});


// Fallback to React index.html for SPA routes
app.Use(async (context, next) =>
{
    if (!context.Request.Path.Value.StartsWith("/api") &&
        !System.IO.File.Exists($"wwwroot{context.Request.Path.Value}"))
    {
        context.Response.ContentType = "text/html";
        await context.Response.SendFileAsync("wwwroot/react/index.html");
    }
    else
    {
        await next();
    }
});


app.Run();
