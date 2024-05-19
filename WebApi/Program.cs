using Application.Features.Surveys.Commands;
using Application.Features.Surveys.Rules;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Context;
using Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ISurveyDbContext, SurveyAppDbContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddScoped(typeof(IVoteRepository<>), typeof(VoteRepository<>));
builder.Services.AddScoped<SurveyBusinessClass>();


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
