namespace Talabat.Pl.Errors
{
    public class ApiExceptionError:ApiErrorsHandling
    {
        public string? Details { get; set; }
        public ApiExceptionError(int StausCode, string? ErrorMessage = null, string? _Details = null):base(StausCode,ErrorMessage)  
        {
            Details = _Details;
        }
    }
}
