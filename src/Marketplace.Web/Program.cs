using Marketplace.Web.Data;
using Marketplace.Web.Services;
using Marketplace.Web.Services.Contracts;
using Marketplace.Web.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var tokenSettings = builder.Configuration.GetSection(nameof(TokenSettings));

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

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection(nameof(TokenSettings)));

builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("FakeDatabase"));

builder.Services
    .AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(options => {
        options.User.RequireUniqueEmail = true;
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-._@+";
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredUniqueChars = 1;
        options.Password.RequiredLength = 7;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;
        options.SignIn.RequireConfirmedEmail = true;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();



// builder.Services.AddAuthorization();

builder.Services.ConfigureApplicationCookie(o =>
{
    o.Cookie.Name = "IdentitySample";
    o.Cookie.HttpOnly = true;
    o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    o.LoginPath = "/account/signin";
    o.LogoutPath = "/account/signout";
    o.AccessDeniedPath = "/app/access-denied";
    o.SlidingExpiration = true;
    o.SlidingExpiration = true;
    o.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
});




// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
// })
// .AddJwtBearer(options =>
// {
//     var settings = tokenSettings.Get<TokenSettings>();
//     options.RequireHttpsMetadata = false;
//     options.SaveToken = true;
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         IssuerSigningKey = new SymmetricSecurityKey(settings.GetSecret()),
//         ValidateIssuerSigningKey = true,
//         ValidateIssuer = false,
//         ValidateAudience = false
//     };
// });

builder.Services.AddScoped<ITokenService>(options =>
{
    var scope = options.CreateScope();
    return (ITokenService) new TokenService(
        scope.ServiceProvider.GetRequiredService<IOptions<TokenSettings>>());
        // scope.ServiceProvider.GetRequiredService<UserManager<User>>());
});

builder.Services.AddAuthorization();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();