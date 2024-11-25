namespace Talabat.Pl.Errors
{
    public class ApiErrorsHandling
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }

        public ApiErrorsHandling(int _statusCode, string? _errorMessage=null)
        {
            StatusCode = _statusCode;
             
            ErrorMessage = _errorMessage??GetErrorMessage(StatusCode);//الحتة بتاعة الميثود اللي هعملها تحت دي علشان لو في حالة فيه مسج هيعرضها لكن لو مفيش هيدخل علي الميثود للي تحت وهياخد واحدة بناء علي الكود
        }

        private string? GetErrorMessage(int statusCode)
        {
            //هبعت له كل احتمالات الايرورز
            //500 server Error
            //400 Bad Requist
            //401 Un Authorized Error
            //C# 7
            
            return StatusCode switch
            {
                400 => "Bad Request",
                401 => "You are Not Authorize",
                404 => "Resource Not Found",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}
