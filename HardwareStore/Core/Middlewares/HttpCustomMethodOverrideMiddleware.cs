namespace HardwareStore.Core.Middlewares // Define el espacio de nombres y declara la clase HttpCustomMethodOverrideMiddleware.
{
    public class HttpCustomMethodOverrideMiddleware // Declara la clase HttpCustomMethodOverrideMiddleware.
    {
        private readonly RequestDelegate _next; // Declara una variable para almacenar la referencia al siguiente delegado de solicitud.

        public HttpCustomMethodOverrideMiddleware(RequestDelegate next) // Constructor de la clase HttpCustomMethodOverrideMiddleware.
        {
            _next = next; // Inicializa la variable para almacenar la referencia al siguiente delegado de solicitud.
        }

        public async Task Invoke(HttpContext context) // Método Invoke que procesa la solicitud HTTP.
        {
            if (context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase) // Verifica si la solicitud es POST.
                && context.Request.HasFormContentType) // Verifica si la solicitud tiene contenido de formulario.
            {
                IFormCollection form = await context.Request.ReadFormAsync(); // Lee el contenido del formulario de la solicitud.
                string? method = form["_method"].FirstOrDefault(); // Obtiene el valor del campo "_method" del formulario, si existe.

                if (!string.IsNullOrEmpty(method)) // Verifica si se ha especificado un método personalizado.
                {
                    // Convertir el método a mayúsculas y establecerlo en la solicitud.
                    context.Request.Method = method.ToUpperInvariant();
                }
            }

            await _next(context); // Pasa la solicitud al siguiente delegado en la cadena de middleware.
        }
    }
}
