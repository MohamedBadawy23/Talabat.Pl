using System.Net;
using System.Text.Json;
using Talabat.Pl.Errors;

namespace Talabat.Pl.MiddleWares
{
    //دي انا هعملها علشان لو معداش منها هيرجع اللي هعمله
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleWare(RequestDelegate Next,ILogger<ExceptionMiddleWare> logger,IHostEnvironment environment)
        {
            _next = Next;
            _logger = logger;
            _environment = environment;
        }
     
        //هتروح تبعتها في البروجرام
   //دي ههندل فيها السيرفر 
        public async Task InvokAsync(HttpContext context)
        {
            try
            {
                //علشان لو عدا يبقي مفيش سيرفر ايرور دي بتعمل كده context 
                await _next.Invoke(context);
            }
            catch(Exception ex)
            {
                //كده انا مسكت الايرور
                _logger.LogError(ex, ex.Message);
                //هروح اشوف البيءة اللي شغال فيها
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;//بدل ما اكتبه بيدي
                //طريقة تعاملي هتكون مختلفة Development لو البيءة بتاعته انه لسه في
                //if (_environment.IsDevelopment())
                //{
                //    var Response = new ApiExceptionError((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString());

                //}
                //else
                //{
                //    var Response = new ApiExceptionError((int)HttpStatusCode.InternalServerError);
                //}
                var Response=_environment.IsDevelopment() ? new ApiExceptionError((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) : new ApiExceptionError((int)HttpStatusCode.InternalServerError);
                //CamelCase هعدله بحيث يكون بطريقة الفرونت يفهمه وهي طريقة 
                var Option = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy=JsonNamingPolicy.CamelCase

                };
                var jsonResponse=JsonSerializer.Serialize(Response,Option);
                context.Response.WriteAsync(jsonResponse);





            }
           
        }
    }
}
