namespace Talabat.Pl.Errors
{
    public class ApiValidationError:ApiErrorsHandling
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationError() : base(400)
        {
            //هنا هتتخزن لي الرسايل اللي هتوضح الايرور نوعه وسببه ايه
            Errors = new List<string>();
        }
        //تاني خطوة تروح تظبط الدنيا في البرجرام
    }
}
