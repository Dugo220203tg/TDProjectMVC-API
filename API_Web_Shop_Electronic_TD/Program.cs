using API_Web_Shop_Electronic_TD.Database;
using API_Web_Shop_Electronic_TD.Interfaces;
using API_Web_Shop_Electronic_TD.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using static API_Web_Shop_Electronic_TD.Mappers.KhachHangsMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Hshop2023Context>(options => {
	options.UseSqlServer(builder.Configuration.GetConnectionString("HShop"));
});
builder.Services.AddDistributedMemoryCache();


builder.Services.AddScoped<IHangHoaRepository, HangHoaRepository>();
builder.Services.AddScoped<IKhachHangRepository, KhachHangsRepository>();
builder.Services.AddScoped<IDanhMucSp, DanhMucSpRepository>();
builder.Services.AddScoped<IHangSp, HangSpRepository>();
builder.Services.AddScoped<IHoaDon, HoaDonRespository>();

builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();