using irede.api;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configServices = new CfgServices(builder);
configServices.AddDependenceServices();



// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "irede.api",
        Version = "v1.2",
        Description = "API para o gerenciamento de produtos na iRede",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Henrique Amorim",
            Email = "henrique.amorim@gmail.com",
            Url = new Uri("https://github.com/DuarteAmorim")
        },

    });
});


var app = builder.Build();

// Habilitar CORS
app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwagger();
    //app.UseSwaggerUI(options =>
    //{
    //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "IRede API v3");
    //    options.DisplayRequestDuration();
    //}); ;

    //app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "IRede API v4");
    options.DisplayRequestDuration();
}); ;

//app.UseHttpsRedirection();




app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();