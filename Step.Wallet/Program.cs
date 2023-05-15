using Step.Service.Logic;
using Microsoft.OpenApi.Models;
using Step.BusinessServices;
using Step.ValueObjects;
using Step.DataAccessObjects;
using Step.Utils;
using Step.Utils.Cryptography;
using Step.Utils.RandomNumber;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();


// configure DI for application services
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ICountryCurrencyService, CountryCurrencyService>();
builder.Services.AddScoped<IFeeService, FeeService>();
builder.Services.AddSingleton<ConfigService>();
builder.Services.AddSingleton<AppConfig>();
builder.Services.AddSingleton<LogService>();
builder.Services.AddSingleton<Encryptor>();
builder.Services.AddSingleton<NumberGenerator>();
builder.Services.AddSingleton<BaseDAO>();
builder.Services.AddSingleton<WalletDAO>();
builder.Services.AddSingleton<TransactionDAO>();
builder.Services.AddSingleton<CountryCurrencyDAO>();
builder.Services.AddSingleton<FeesDAO>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Step Wallet", Version = "v1" });
    option.AddSecurityDefinition("uiKey", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Name = "uikey",
        Scheme = "apikey",
        Description = "Input apikey to access this API",
    });
    option.AddSecurityDefinition("profileId", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Name = "profileId",
        Scheme = "apikey",
        Description = "Input profileId to access this API",
    });
    option.AddSecurityDefinition("bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Description = "Input bearer token to access this API",
    });
    option.AddSecurityDefinition("Signature", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Name = "Signature",
        Scheme = "apikey",
        Description = "Input profileId to access this API",
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "uiKey" }
            },
            new[] { "Step Wallet" }
        },
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "profileId" }
            },
            new[] { "Step Wallet" }
        },
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" }
            },
            new[] { "Step Wallet" }
        },
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Signature" }
            },
            new[] { "Step Wallet" }
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
}

app.UseAuthorization();

app.MapControllers();

app.Run();

