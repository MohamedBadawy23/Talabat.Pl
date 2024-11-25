using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Pl.Errors;

namespace Talabat.Pl.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorNotFoundController : ControllerBase
    {
        //هنا لو رحت تدور علي اند بوينت معينة مثلا وملقتهاش هترجع لي ده 
        public ActionResult Errors(int code)
        {
            return NotFound(new ApiErrorsHandling(code));
        }
        //app.UseStatusCodePagesWithRedirects("/errors/{0}"); بعدها هروح علي البروجرم 
        //علشان يدور لو ملقيش حاجة يرجع الكونترولر ده
    }
}
