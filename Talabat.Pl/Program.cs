using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories;
using Talabat.Pl.Errors;
using Talabat.Pl.Extensions;
using Talabat.Pl.Helper;
using Talabat.Pl.MiddleWares;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Context;
using Talabat.Repository.Identity;
using Talabat.Repository.Identity.Seeding;
using Talabat.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region For Add ConnectionStrings [Database] &&Add Services
//����� �� ���� ����� ��������� ������
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TalabatDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdenetityConnection"));
});
//-----------------------------------------


builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
{
    var Connection = builder.Configuration.GetConnectionString("RedisConnection");
    return ConnectionMultiplexer.Connect(Connection);
});

builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile())); 



//����� �� ���� �������� ����� ����������� 
//����� �� ��� �� ���� ��� �������� ���� �������� ����� �� ��� ����
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();



#region Validation Error Handling
builder.Services.Configure<ApiBehaviorOptions>(Options =>
{
    //Dictionary ������ ���� ����� �� 
    //Model state is Dictionary[Key & Value]
    //key=> Name Of Parameter
    //Value=>Errors
    Options.InvalidModelStateResponseFactory = (actioncontext) =>
    {               /*  ��� ����� ���� ���� �� ���� �� ������ ��� ����� ����� */          /* ����� ������� */                 /* ���� ����� ����� ���� �������� */
        var errors = actioncontext.ModelState.Where(P => P.Value.Errors.Count > 0).SelectMany(P => P.Value.Errors).Select(P => P.ErrorMessage).ToArray();
        var Validation = new ApiValidationError()
        {
            //����� ��� �������� ���� ���� ������ �� ����� ���� ��� ������� �� ������ ��� ������ �� ���� ��������
            Errors = errors

        };
        //��� �� ����� ������� ����������� ��� ���������� ��� ����� ������ �� ��� ��� ��� �� ��������� �� ���� ������ ������ ���� BadRequest �� ��� ���� �� ��� ����� ��� 
        return new BadRequestObjectResult(Validation);


    };
});
#endregion
builder.Services.AddServiceCollection();



#endregion







builder.Services.AddAuthentication();//UserManager//SigninManager//RoleManager


var app = builder.Build();

//////////////////////
//�� ��� �� ���� ����� ����� ������ ����� ����� ���� ���� ��� �� 
//����� �� ��� �� ����� ����� �� ����� �� ������� ����� ����
#region Update Databse
//ConnectionString  ����� ��  Parameterless Constructor ������� �� ����� �� �����
//Scope.Dispose();== �� ��� �� ���� ����� �� ���������     Using ��� ����
using var Scope = app.Services.CreateScope();

//2-Service It Self

var Services = Scope.ServiceProvider;
//1-Group All Services Scoped
//��� ����� ��� �������� ����� ���� ������ ���� ILogger ��
var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
try
{

    //3-Ask clr For Crating Object From DbContext Explicite
    var DbContext = Services.GetRequiredService<TalabatDbContext>();
    await DbContext.Database.MigrateAsync();

    var Idenetity=Services.GetRequiredService<AppIdentityDbContext>();
    await Idenetity.Database.MigrateAsync();
    var User = Services.GetRequiredService<UserManager<AppUser>>();
    await AppIdenetityDbContextSeed.IdenetitySeed(User);
    //-Seeding-------------==============
    //�������� ���� ��� ������ ������ ����� �� ������
    await DbContextSeed.SeedAsync(DbContext);
    //=========================================
    
}
catch (Exception ex)
{
    // ��� ����� ��� �������� ����� ���� ������ ���� ����� �� ��� ����� ���� �� ��� Logger �� 
    var Logger = LoggerFactory.CreateLogger<Program>();
    Logger.LogError(ex, "An Error During Migration");
}
#endregion


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //����� ����� �� ��� ���� ����� ����� ��� �� ���� ��������
    // app.UseMiddleware<ExceptionMiddleWare>();
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseaSwaggerMiddleWare();
   
     
}
//app.UseStatusCodePagesWithRedirects("/errors/{0}");
//��� ��� �� ����� ��������� ���� ���� ��� ������ �����
app.UseStaticFiles();//���� ��� ����  URL Pathe ��� ����� ���� �������� ��� ���� ���� ����� ������� ��� �� ���� �� 
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
