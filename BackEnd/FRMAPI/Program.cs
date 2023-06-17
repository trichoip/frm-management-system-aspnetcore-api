using BAL.Authorization;
using BAL.AutoMapperProfile;
using BAL.Models;
using BAL.Services.Implements;
using BAL.Services.Interfaces;
using DAL;
using BAL.Models;
using DAL.Entities;
using System.Reflection;
using DAL.Infrastructure;
using BAL.Services.Implements;
using BAL.Services.Interfaces;
using DAL.Repositories.Implements;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Reflection;
using BAL.AutoMapperProfile;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath);
    #region Group 5 - Authentication & Authorization
    s.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Beare Scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    s.OperationFilter<SecurityRequirementsOperationFilter>();
    #endregion
});





builder.Services.AddMvc()
     .AddNewtonsoftJson(
          options =>
          {
              options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
          });

builder.Services.AddDbContext<FRMDbContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
  options.IdleTimeout = TimeSpan.FromMinutes(30);//We set Time here 
});

//Register Dependency Injection
builder.Services.AddScoped<IDbFactory, DbFactory>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITrainingProgramService, TrainingProgramService>();



builder.Services.AddScoped<IPermissionRightRepository, PermissionRightRepository>();
builder.Services.AddScoped<IPermissionRightService, PermissionRightService>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IRightRepository, RightRepository>();
builder.Services.AddScoped<IRightService, RightService>();
builder.Services.AddScoped<IAssignmentSchemaRepository, AssignmentSchemaRepository>();
builder.Services.AddScoped<IHistoryMaterialRepository, HistoryMaterialRepository>();
builder.Services.AddScoped<IHistoryMaterialService, HistoryMaterialService>();
builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IFormatTypeRepository, FormatTypeRepository>();
builder.Services.AddScoped<IDeliveryTypeRepository, DeliveryTypeRepository>();
builder.Services.AddScoped<IOutputStandardRepository, OutputStandardRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<ICurriculumService, CurriculumService>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<ISyllabusRepository, SyllabusRepository>();
builder.Services.AddScoped<ILevelRepository, LevelRepository>();
builder.Services.AddScoped<ISyllabusService, SyllabusService>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IClassSelectedDateRepository, ClassSelectedDateRepository>();
builder.Services.AddScoped<IClassTraineeRepository, ClassTraineeRepository>();
builder.Services.AddScoped<IClassAdminReporitory, ClassAdminRepository>();
builder.Services.AddScoped<IClassMentorRepository, ClassMentorRepository>();
builder.Services.AddScoped<ICurriculumRepository, CurriculumRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ITrainingProgramRepository, TrainingProgramRepository>();
builder.Services.AddScoped<ITrainingProgramService, TrainingProgramService>();
builder.Services.AddScoped<IHistoryTrainingProgramRepository, HistoryTrainingProgramRepository>();
builder.Services.AddScoped<IHistoryTrainingProgramService, HistoryTrainingProgramService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IHistorySyllabusRepository, HistorySyllabusRepository>();
builder.Services.AddScoped<IHistorySyllabusService, HistorySyllabusService>();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IClassSiteRepository, ClassSiteRepository>();
builder.Services.AddScoped<IAttendeeTypeRepository, AttendeeTypeRepository>();
builder.Services.AddScoped<IClassProgramCodeRepository, ClassProgramCodeRepository>();
builder.Services.AddScoped<IFsucontactPointService, FsucontactPointService>();
builder.Services.AddScoped<IFSUContactPointRepository, FSUContactPointRepository>();
builder.Services.AddScoped<IClassSelectedDateService, ClassSelectedDateService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IClassLocationService, ClassLocationService>();
builder.Services.AddScoped<IClassLocationRepository, ClassLocationRepository>();
builder.Services.AddScoped<IClassStatusRepository, ClassStatusRepository>();
builder.Services.AddScoped<IClassStatusService, ClassStatusService>();
builder.Services.AddScoped<IAttendeeTypeRepository, AttendeeTypeRepository>();
builder.Services.AddScoped<IAttendeeTypeService, AttendeeTypeService>();
builder.Services.AddScoped<IClassAdminService, ClassAdminService>();
builder.Services.AddScoped<IClassMentorService, ClassMentorService>();
builder.Services.AddScoped<IClassTraineeService, ClassTraineeService>();
builder.Services.AddScoped<IFsoftUnitService, FsoftUnitService>();
builder.Services.AddScoped<IFsoftUnitRepository, FsoftUnitRepository>();

builder.Services.AddScoped<IClassStatusRepository, ClassStatusRepository>();
builder.Services.AddScoped<IFsoftUnitRepository, FsoftUnitRepository>();
builder.Services.AddScoped<IClassUniversityCodeRepository, ClassUniversityCodeRepository>();
builder.Services.AddScoped<IClassTechnicalGroupRepository, ClassTechnicalGroupRepository>();
builder.Services.AddScoped<IClassSiteRepository, ClassSiteRepository>();
builder.Services.AddScoped<IClassFormatTypeRepository, ClassFormatTypeRepository>();
builder.Services.AddScoped<IClassProgramCodeRepository, ClassProgramCodeRepository>();
builder.Services.AddScoped<IAttendeeTypeRepository, AttendeeTypeRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuerSigningKey = true,
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                               builder.Configuration.GetSection("AppSettings:SerectKey").Value)),
                           ValidateIssuer = false,
                           ValidateAudience = false
                       };
                   });

builder.Services.AddAutoMapper(typeof(User));
builder.Services.AddAutoMapper(typeof(TrainingProgramViewModel));
builder.Services.AddAutoMapper(typeof(TrainingProgramDetailViewModel));
builder.Services.AddAutoMapper(typeof(ClassProfile));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(TrainingProgramProfile));
builder.Services.AddAutoMapper(typeof(PermissionRight));
builder.Services.AddAutoMapper(typeof(HistorySyllabusProfile));
builder.Services.AddAutoMapper(
    typeof(SyllabusProfile), typeof(AssignmentSchemaProfile), typeof(SessionProfile),
    typeof(UnitProfile), typeof(LessonProfile), typeof(MaterialProfile)
    );


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>

                      {
                          policy.WithOrigins("http://127.0.0.1:5500");
                          policy.WithMethods("GET", "POST", "PUT");
                          policy.WithHeaders("Content-Type", "Access-Control-Allow-Headers");
                          //policy.AllowAnyHeader();
                          //policy.AllowAnyMethod();
                      });
});



var app = builder.Build();

//Allow Cross Origin to Create Separated Front-end
app.UseCors(MyAllowSpecificOrigins);
//Allow Cross Origin to Create Separated Front-end
app.UseSession();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
   { c.SwaggerEndpoint("/swagger/v1/swagger.json", "FSoft Training Management API v1"); });

app.UseHttpsRedirection();

#region Group 5 - Authentication & Authorization
app.UseAuthentication();
#endregion

app.UseAuthorization();

app.MapControllers();

//Auto migration
bool autoMigrate = app.Configuration.GetValue<bool>("MigrationSettings:autoMigrate");
if (autoMigrate)
{
    DbContext context = new FRMDbContext();
    context.Database.Migrate();
}

app.Run();
