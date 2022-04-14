using Microsoft.EntityFrameworkCore;
using Server.BasicBank.Data;
using Server.BasicBank.Data.Entity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ApplicationDbContext>();

builder.Services.AddDbContext<DbContext,ApplicationDbContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("Connection").ToString()));
 // Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
