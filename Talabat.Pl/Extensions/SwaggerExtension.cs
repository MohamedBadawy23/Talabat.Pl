namespace Talabat.Pl.Extensions
{
    //ده علشان بس اجمع الحاجات اللي شبه بعض مع بعض علشان التنظيم مش اكتر
    public static class SwaggerExtension
    {
        public static WebApplication UseaSwaggerMiddleWare(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
