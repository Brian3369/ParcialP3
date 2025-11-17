using Microsoft.EntityFrameworkCore;
using ParcialP3.Application.Interfaces;
using ParcialP3.Persistence.Context;
using ParcialP3.Persistence.Interfaces;
using ParcialP3.Persistence.Repositories;
using ParcialP3.WEB.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBMVC")));

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IInmueblesRepository, InmueblesRepository>();
builder.Services.AddScoped<ICondicionRepository, CondicionRepository>();
builder.Services.AddScoped<ITipoPropiedadRepository, TipoPropiedadRepository>();
builder.Services.AddScoped<ICiudadesRepository, CiudadesRepository>();


builder.Services.AddControllersWithViews();

builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(60);
    opt.Cookie.HttpOnly = true;
    //opt.Cookie.IsEssential = true; // Make the session cookie essential
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserSeccion, UserSession>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
