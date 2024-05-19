using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using WLISBackWITHOUTCOMMUNIKATION___.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WLISBackWITHOUTCOMMUNIKATION___.Auntefication
{
    [Controller]
    public class authService(FullContext dbcontext, WebApplication app, HttpContext context)
    {

        FullContext cx = dbcontext;
        WebApplication app = app;
        HttpContext context = context;

        [HttpGet("/accessdenied")]
        public async void AccessDenied()
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Access Denied");
        }

        [HttpGet("/login")]
        public async void Login()
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            // html-форма для ввода логина/пароля
            string loginForm = @"<!DOCTYPE html>
                                <html>
                                <head>
                                    <meta charset='utf-8' />
                                    <title>METANIT.COM</title>
                                </head>
                                <body>
                                    <h2>Login Form</h2>
                                    <form method='post'>
                                        <p>
                                            <label>Email</label><br />
                                            <input name='email' />
                                        </p>
                                        <p>
                                            <label>Password</label><br />
                                            <input type='password' name='password' />
                                        </p>
                                        <input type='submit' value='Login' />
                                    </form>
                                </body>
                                </html>";
            await context.Response.WriteAsync(loginForm);
        }

        [HttpPost("login")]
        public async void GetLogin()
        {
            var form = context.Request.Form;

            if (!form.ContainsKey("email") || !form.ContainsKey("password"))
                return;

            string email = form["email"];
            string password = form["password"];

            // находим пользователя 
            User? person = dbcontext.Users.FirstOrDefault(p => p.Login == email && p.Password == password);
            // если пользователь не найден, отправляем статусный код 401
            if (person is null) return;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await context.SignInAsync(claimsPrincipal);

            return;
        }

        [HttpGet("/admin")]
        public async void AdminPanel()
        {
            await context.Response.WriteAsync("ADMINSSSS");
        }

               //app.MapGet("/logout", async (HttpContext context) =>
        //{
        //    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return "Данные удалены";
        //});
        //    }
    }
}
