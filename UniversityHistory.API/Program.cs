using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using UniversityHistory.API.Middleware;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Queries.GetClassmates;
using UniversityHistory.Application.Queries.GetActiveGroups;
using UniversityHistory.Application.Queries.GetGroupComposition;
using UniversityHistory.Application.Queries.GetStudentGroupOnDate;
using UniversityHistory.Application.Queries.GetStudentsInGroup;
using UniversityHistory.Application.Queries.GetTimeline;
using UniversityHistory.Application.Queries.GetAverageGrade;
using UniversityHistory.Application.Queries.GetStudentDisciplines;
using UniversityHistory.Application.Services;
using UniversityHistory.Application.Validation.Students;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;
using UniversityHistory.Infrastructure.Queries;
using UniversityHistory.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UniversityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IAcademicLeaveRepository, AcademicLeaveRepository>();
builder.Services.AddScoped<IExternalTransferRepository, ExternalTransferRepository>();
builder.Services.AddScoped<IStudyPlanRepository, StudyPlanRepository>();
builder.Services.AddScoped<IGradeRepository, GradeRepository>();
builder.Services.AddScoped<ISubgroupAssignmentRepository, SubgroupAssignmentRepository>();
builder.Services.AddScoped<IDisciplineRepository, DisciplineRepository>();
builder.Services.AddScoped<IAcademicUnitRepository, AcademicUnitRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IGroupPlanAssignmentRepository, GroupPlanAssignmentRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IGetTimelineQueryHandler, GetTimelineQueryHandler>();
builder.Services.AddScoped<IGetClassmatesQueryHandler, GetClassmatesQueryHandler>();
builder.Services.AddScoped<IGetGroupCompositionQueryHandler, GetGroupCompositionQueryHandler>();
builder.Services.AddScoped<IGetActiveGroupsQueryHandler, GetActiveGroupsQueryHandler>();
builder.Services.AddScoped<IGetStudentsInGroupQueryHandler, GetStudentsInGroupQueryHandler>();
builder.Services.AddScoped<IGetStudentGroupOnDateQueryHandler, GetStudentGroupOnDateQueryHandler>();
builder.Services.AddScoped<IGetAverageGradeQueryHandler, GetAverageGradeQueryHandler>();
builder.Services.AddScoped<IGetStudentDisciplinesQueryHandler, GetStudentDisciplinesQueryHandler>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IMovementService, MovementService>();
builder.Services.AddScoped<IStudyPlanService, StudyPlanService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IDisciplineService, DisciplineService>();
builder.Services.AddScoped<IAcademicUnitService, AcademicUnitService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddValidatorsFromAssemblyContaining<StudentCreateDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
        opts.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();
