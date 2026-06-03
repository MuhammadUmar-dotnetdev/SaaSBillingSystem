using Microsoft.EntityFrameworkCore;
using SaaSBillingSystem.Application;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Infrastructure.Persistence;
using SaaSBillingSystem.Infrastructure.Repositories;
using SaaSBillingSystem.Application.Features.Auth.RegisterUser;
using MediatR;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using SaaSBillingSystem.Infrastructure.Services;
using SaaSBillingSystem.API.Extensions;
using SaaSBillingSystem.API.Middlewares;
using StackExchange.Redis;
using Scalar.AspNetCore;

namespace SaaSBillingSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddPersistence(builder.Configuration);

            builder.Services.AddAppServices();

            builder.Services.AddMediatRConfig();
            //builder.Services.AddScoped<RegisterUserHandler>();

            builder.Services.AddJwtAuthentication(builder.Configuration);

            //builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var connectionString = builder.Configuration["Redis:ConnectionString"];
                return ConnectionMultiplexer.Connect(connectionString!);
            });

            builder.Services.AddScoped<ICacheService, RedisCacheService>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                {
                    policy.RequireRole("Admin");
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options.WithTitle("SaaSBillingApp")
                    .WithTheme(ScalarTheme.Purple)
                    .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
                });
            }

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.MapGet("/", () =>
            {
                return Results.Ok("Welcome To SaaS App");
            });

            app.Run();
        }
    }
}
