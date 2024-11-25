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
//ÇáÌÒÁ Ïå ãÎÕÕ ÚáÔÇä ÇáßæäíßÔä ÓÊÑíäÌ
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



//ÇáÓØÑ Ïå ÈÊÇÚ ÇáÓíÑİíÓ ÇáÎÇÕ ÈÇáßæäÊÑæáÑ 
//ÇáÓØÑ Ïå ÈÏá ãÇ ÇÚãá ãÑå ááÈÑæÏßä æãÑÉ ááÈÑÇäÏÒ ÇáÓØÑ Ïå ÎÇÕ ÈßÏå
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();



#region Validation Error Handling
builder.Services.Configure<ApiBehaviorOptions>(Options =>
{
    //Dictionary ÇáãæÏá ÓÊíÊ ÚÈÇÑÉ Úä 
    //Model state is Dictionary[Key & Value]
    //key=> Name Of Parameter
    //Value=>Errors
    Options.InvalidModelStateResponseFactory = (actioncontext) =>
    {               /*  ßÏå ÇáÓØÑ Çááí ÊÍÊí Ïå æÕáÊ ãä ÎáÇááå ááí ÚäÏåã ÇíÑæÑ */          /* ÚáÔÇä ÇÎÊÇÑåã */                 /* Çááí ÚÇíÒå ÚáÔÇä ÇæÕá ááÇíÑæÑÒ */
        var errors = actioncontext.ModelState.Where(P => P.Value.Errors.Count > 0).SelectMany(P => P.Value.Errors).Select(P => P.ErrorMessage).ToArray();
        var Validation = new ApiValidationError()
        {
            //ÚáÔÇä ÇÎÏ ÇáÇíÑæÑÒ Çááí ÚÑİÊ ÇæÕáåã ãä ÇáßæÏ Çááí İæŞ æÇÈÚÊåã ãÚ ÓÊíÊæÓ ßæÏ æÇáãÓÌ İí ßáÇÓ İÇáíÏíÔä
            Errors = errors

        };
        //áßä Ïí ãíËæÏ ãÚãæáåÇ ÇãÈáíãäÊíÔä Ìæå ÇáßæäÊÑæáÑ ÈíÒ ÚáÔÇä ÊÓÇÚÏß ÈÓ áßä ÇäÊ åäÇ İí ÇáÈÑæÌÑÇã İÇ áÇÒã ÊÓÊÎÏã ÇáßáÇÓ äİÓå BadRequest ØÈ åäÇ ÚÇÏí ãÇ ßäÊ ŞáÊáå ßÏå 
        return new BadRequestObjectResult(Validation);


    };
});
#endregion
builder.Services.AddServiceCollection();



#endregion







builder.Services.AddAuthentication();//UserManager//SigninManager//RoleManager


var app = builder.Build();

//////////////////////
//Ïí ÈÏá ãÇ ÇÑæÍ ÇáÈßÌ ãÇäÌÑ ßæäÓæá æÇŞæá ÇÈÏíÊ åÚãá Çááí ÊÍÊ Ïå 
//ÚáÔÇä áæ İíå Çí ÊÚÏíá åíÍÕá áã åÊÚãá Ñä ÇáÊÚÏíá åíÓãÚ æÍÏå
#region Update Databse
//ConnectionString  æÇÈæÙ Çá  Parameterless Constructor ÇáÎØæÇÊ Ïí ÚáÔÇä ãÇ ÇÚãáÔ
//Scope.Dispose();== Ïí ÈÚÏ ãÇ ÇÎáÕ åÊŞİá áí ÇáßæäíßÔä     Using åäÇ ßáãÉ
using var Scope = app.Services.CreateScope();

//2-Service It Self

var Services = Scope.ServiceProvider;
//1-Group All Services Scoped
//åäÇ ÈåäÏá Èíå ÇáÇßÓÈÔä ÚáÔÇä ÇÚÑİ ÇÊÚÇãá ãÚÇå ILogger Çá
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
    //ÈÓÊÏÚíåÇ ÈäİÓ ÇÓã ÇáßáÇÓ ÈÊÇÚåÇ ÚáÔÇä åí ÓÊÇÊíß
    await DbContextSeed.SeedAsync(DbContext);
    //=========================================
    
}
catch (Exception ex)
{
    // åäÇ ÈåäÏá Èíå ÇáÇßÓÈÔä ÚáÔÇä ÇÚÑİ ÇÊÚÇãá ãÚÇå ÚáÔÇä áæ ÍÕá ÇíÑæÑ ÇÚÑİ åæ İíä Logger Çá 
    var Logger = LoggerFactory.CreateLogger<Program>();
    Logger.LogError(ex, "An Error During Migration");
}
#endregion


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //ÚáÔÇä íÊÇßÏ ãä Çäå ãİíÔ ÓíÑİÑ ÇíÑæÑ ŞÈá ãÇ íÔÛá ÇáÈÑäÇãÌ
    // app.UseMiddleware<ExceptionMiddleWare>();
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseaSwaggerMiddleWare();
   
     
}
//app.UseStatusCodePagesWithRedirects("/errors/{0}");
//åäÇ ÈÚÏ ãÇ åíÔÛá ÇáÈÑæÌíßÊ áÇÒã íÚÏí Úáí ÓÊÇÊíß İÇíáÒ
app.UseStaticFiles();//Çááí ÇäÊ ÈÚÊå  URL Pathe åäÇ ÚáÆÇä Çãßä ÇáßÓÊÑÇá Çäå íÑæÍ íÌíÈ ÇáÕæÑ æíÚÑÖåÇ æÏå ãä ÎáÇá Çá 
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
