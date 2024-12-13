using HotelFlow.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HotelFlow.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HotelFlowContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelFlowContext") ?? throw new InvalidOperationException("Connection string 'HotelFlowContext' not found.")));
// Add services to the container.
builder.Services.AddControllersWithViews();

// เพิ่มบริการ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44391/Api"); // กำหนด Base URL ของ API
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

///************************************************************** Conect Database **************************************************************
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();


/// สำรหับ Connect DB

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapAddroomEndpoints();
app.MapControllers();

app.Run();
