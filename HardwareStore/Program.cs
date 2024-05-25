using HardwareStore.Data;
using HardwareStore.Services;
using Microsoft.EntityFrameworkCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using AspNetCoreHero.ToastNotification;
using HardwareStore.Data.Seeders;
using HardwareStore.Data.Entities;
using HardwareStore.Helpers;
using Microsoft.AspNetCore.Identity;
using static HardwareStore.Services.IEmployeeServices;
using static HardwareStore.Services.IProductServices;


var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();

// Contexto de datos
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

builder.Services.AddIdentity<User, IdentityRole>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.Password.RequireDigit = false;
    x.Password.RequiredUniqueChars = 0;
    x.Password.RequireLowercase = false;
    x.Password.RequireUppercase = false;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequiredLength = 4;
})
 .AddEntityFrameworkStores<DataContext>()
 .AddDefaultTokenProviders();


// Agregar servicios adicionales
builder.Services.AddScoped<IProductServices, ProductService>();
builder.Services.AddScoped<IEmployeeServices, EmployeeService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IConverterHelper, ConverterHelper>();
builder.Services.AddScoped<ICombosHelper, CombosHelper>();
builder.Services.AddScoped<INotyfService, NotyfService>();

builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 10;
    config.IsDismissable = true;
    config.Position = NotyfPosition.BottomRight;
});

builder.Services.AddTransient<SeedDb>();





var app = builder.Build();






// Configurar el pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{

    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Agregar autenticación
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");



SeedData(app);

app.Run();

void SeedData(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var seedService = services.GetRequiredService<SeedDb>();
    seedService.SeedAsync().Wait();
}
