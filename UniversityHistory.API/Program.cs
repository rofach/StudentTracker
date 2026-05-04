using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using UniversityHistory.API.Auth;
using UniversityHistory.API.Middleware;
using UniversityHistory.API.Services;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Queries.GetClassmates;
using UniversityHistory.Application.Queries.GetActiveGroups;
using UniversityHistory.Application.Queries.GetGroupComposition;
using UniversityHistory.Application.Queries.GetStudentGroupOnDate;
using UniversityHistory.Application.Queries.GetStudentsInGroup;
using UniversityHistory.Application.Queries.GetTimeline;
using UniversityHistory.Application.Queries.GetAverageGrade;
using UniversityHistory.Application.Queries.GetActiveAcademicDifference;
using UniversityHistory.Application.Queries.GetStudentDisciplines;
using UniversityHistory.Application.Queries.GetDisciplineSearch;
using UniversityHistory.Application.Queries.GetStudentSearch;
using UniversityHistory.Application.Queries.GetInternalTransferJournal;
using UniversityHistory.Application.Rules;
using UniversityHistory.Application.Services;
using UniversityHistory.Application.Validation.Students;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;
using UniversityHistory.Infrastructure.Identity;
using UniversityHistory.Infrastructure.Queries;
using UniversityHistory.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UniversityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));
builder.Services.Configure<BootstrapAuthOptions>(builder.Configuration.GetSection(BootstrapAuthOptions.SectionName));

var jwtOptions = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()
    ?? throw new InvalidOperationException("JWT configuration is missing.");
var jwtSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey));

builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 8;
        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = jwtSigningKey,
            ClockSkew = TimeSpan.FromMinutes(1),
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IAcademicLeaveRepository, AcademicLeaveRepository>();
builder.Services.AddScoped<IExternalTransferRepository, ExternalTransferRepository>();
builder.Services.AddScoped<IStudyPlanRepository, StudyPlanRepository>();
builder.Services.AddScoped<IGradeRepository, GradeRepository>();
builder.Services.AddScoped<IStudentSubgroupEnrollmentRepository, StudentSubgroupEnrollmentRepository>();
builder.Services.AddScoped<IDisciplineRepository, DisciplineRepository>();
builder.Services.AddScoped<IAcademicUnitRepository, AcademicUnitRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IGroupPlanAssignmentRepository, GroupPlanAssignmentRepository>();
builder.Services.AddScoped<IStudentGroupTransferRepository, StudentGroupTransferRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IGetTimelineQueryHandler, GetTimelineQueryHandler>();
builder.Services.AddScoped<IGetClassmatesQueryHandler, GetClassmatesQueryHandler>();
builder.Services.AddScoped<IGetGroupCompositionQueryHandler, GetGroupCompositionQueryHandler>();
builder.Services.AddScoped<IGetActiveGroupsQueryHandler, GetActiveGroupsQueryHandler>();
builder.Services.AddScoped<IGetStudentsInGroupQueryHandler, GetStudentsInGroupQueryHandler>();
builder.Services.AddScoped<IGetStudentGroupOnDateQueryHandler, GetStudentGroupOnDateQueryHandler>();
builder.Services.AddScoped<IGetAverageGradeQueryHandler, GetAverageGradeQueryHandler>();
builder.Services.AddScoped<IGetActiveAcademicDifferenceQueryHandler, GetActiveAcademicDifferenceQueryHandler>();
builder.Services.AddScoped<IGetStudentDisciplinesQueryHandler, GetStudentDisciplinesQueryHandler>();
builder.Services.AddScoped<IGetDisciplineSearchQueryHandler, GetDisciplineSearchQueryHandler>();
builder.Services.AddScoped<IGetStudentSearchQueryHandler, GetStudentSearchQueryHandler>();
builder.Services.AddScoped<IGetInternalTransferJournalQueryHandler, GetInternalTransferJournalQueryHandler>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IMovementService, MovementService>();
builder.Services.AddScoped<IStudyPlanService, StudyPlanService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IStudyProcessRule, StudyProcessRule>();
builder.Services.AddScoped<IDisciplineService, DisciplineService>();
builder.Services.AddScoped<IAcademicUnitService, AcademicUnitService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IdentitySeeder>();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var identitySeeder = scope.ServiceProvider.GetRequiredService<IdentitySeeder>();
    await identitySeeder.SeedAsync();
}

await app.RunAsync();
