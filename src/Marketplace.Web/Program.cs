using Marketplace.Web.Services;
using Marketplace.Web.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("Marketplace.API", configure =>
{
    configure.BaseAddress = new Uri("https://localhost:5000");
})
.ConfigurePrimaryHttpMessageHandler(() => {
    var handler = new HttpClientHandler();
    if (builder.Environment.IsDevelopment())
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
    return handler;
});


builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();