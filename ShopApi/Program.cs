using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using ShopApi.Filters;
using ShopApi.Middlewares;
using ShopApi.OData;
using ShopApplication;
using ShopInfrastructure;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1.0);
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;

    }).AddMvc()
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });


builder.Services.AddControllers(option =>
{
    option.Filters.Add<ValidationFilter>();
    option.Filters.Add<ApiResultFilter>();
}).AddOData(option =>
{
    option.AddRouteComponents("OData", ODataEdmModelBuilder.CreateEdmModel())
        .Select()
        .Filter()
        .OrderBy()
        .Expand().Count().SetMaxTop(100);
});




builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddRazorPages();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.DisplayRequestDuration();
    foreach (var description in app.DescribeApiVersions())
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant());
    }
});

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.GetRequiredService<SeedData>().SeedAsync();
}

app.Run();