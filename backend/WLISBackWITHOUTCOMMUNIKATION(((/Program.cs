using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WLISBackWITHOUTCOMMUNIKATION___.Auntefication;
using WLISBackWITHOUTCOMMUNIKATION___.Contexts;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//autor
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
 //   {
 //       options.LoginPath = "/login";
 //       options.AccessDeniedPath = "/accessdenied";
  //  });
//builder.Services.AddAuthorization();
//autor



builder.Services.AddDbContext<FullContext>(
    options =>
    {
        options.UseSqlite("Data Source=wlisdb.CX");
    }
);

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.UseCors(x =>
{
    x.WithHeaders().AllowAnyHeader();
    x.WithOrigins().AllowAnyOrigin();
    x.WithMethods().AllowAnyMethod();
});

app.Run();
