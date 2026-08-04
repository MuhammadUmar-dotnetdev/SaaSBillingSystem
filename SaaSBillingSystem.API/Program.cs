using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Infrastructure.Services;
using SaaSBillingSystem.API.Extensions;
using SaaSBillingSystem.API.Middlewares;
using StackExchange.Redis;
using Scalar.AspNetCore;
using SaaSBillingSystem.Shared.Common.ConfigurationOptions;
using Hangfire;
using Hangfire.PostgreSql;
using OllamaSharp;
using Hangfire.PostgreSql.Properties;
using Microsoft.Extensions.AI;
using SaaSBillingSystem.Shared.Common;

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

                options.AddPolicy("UserOnly", policy =>
                {
                    policy.RequireRole("User");
                });

                options.AddPolicy("OwnerOnly", policy =>
                {
                    policy.RequireRole("Owner");
                });

                //options.AddPolicy("d")
            });

            builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection("Email"));
            builder.Services.Configure<FrontendOptions>(builder.Configuration.GetSection("Frontend"));

            builder.Services.AddHangfire(config =>
            {
                config.UsePostgreSqlStorage(options =>
                {
                    options.UseNpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
                });
            });

            builder.Services.AddHangfireServer();

            builder.Services.AddChatClient(sp =>
            {
                IChatClient innerClient = new OllamaApiClient(
                    uri: new Uri("http://localhost:11434"),
                    defaultModel: "llama3.2"
                //defaultModel: "phi3:mini"
                );

                return new ChatClientBuilder(innerClient)
                .UseFunctionInvocation()
                .Build();
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
                app.UseHangfireDashboard();
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

            app.MapPost("/api/chat", async (PromptRequest request, IChatClient client, IChatService chatService) =>
            {
                if(string.IsNullOrEmpty(request.Prompt))
                {
                    return Results.BadRequest("Prompt can't be empty");
                }

                var options = new ChatOptions
                {
                    Tools = new AIFunction[]
                    {
                        AIFunctionFactory.Create(chatService.CalculateTotalTax),
                        AIFunctionFactory.Create(chatService.CheckInvitationsList)
                    }
                };

                var response = await client.GetResponseAsync(request.Prompt, options);
                return Results.Ok(new { answer = response.Text });
            });

            app.Run();
        }
    }
}
