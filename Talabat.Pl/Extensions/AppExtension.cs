using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Repositories;
using Talabat.Pl.Errors;
using Talabat.Pl.Helper;
using Talabat.Repository.Repositories;

namespace Talabat.Pl.Extensions
{
    public static class AppExtension
    {
        public static IServiceCollection AddServiceCollection(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(typeof(ProductProfile));
            return Services;


            #region Validation Error Handling
             Services.Configure<ApiBehaviorOptions>(Options =>
            {
                //Dictionary المودل ستيت عبارة عن 
                //Model state is Dictionary[Key & Value]
                //key=> Name Of Parameter
                //Value=>Errors
                Options.InvalidModelStateResponseFactory = (actioncontext) =>
                {               /*  كده السطر اللي تحتي ده وصلت من خلالله للي عندهم ايرور */          /* علشان اختارهم */                 /* اللي عايزه علشان اوصل للايرورز */
                    var errors = actioncontext.ModelState.Where(P => P.Value.Errors.Count > 0).SelectMany(P => P.Value.Errors).Select(P => P.ErrorMessage).ToArray();
                    var Validation = new ApiValidationError()
                    {
                        //علشان اخد الايرورز اللي عرفت اوصلهم من الكود اللي فوق وابعتهم مع ستيتوس كود والمسج في كلاس فاليديشن
                        Errors = errors

                    };
                    //لكن دي ميثود معمولها امبليمنتيشن جوه الكونترولر بيز علشان تساعدك بس لكن انت هنا في البروجرام فا لازم تستخدم الكلاس نفسه BadRequest طب هنا عادي ما كنت قلتله كده 
                    return new BadRequestObjectResult(Validation);


                };
            });
            #endregion
        }
    }
}
