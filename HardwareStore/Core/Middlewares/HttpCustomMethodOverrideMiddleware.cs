namespace HardwareStore.Core.Middlewares 
{
    public class HttpCustomMethodOverrideMiddleware 
    {
        private readonly RequestDelegate _next; 

        public HttpCustomMethodOverrideMiddleware(RequestDelegate next) 
        {
            _next = next; 
        }

        public async Task Invoke(HttpContext context) 
        {
            if (context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase) 
                && context.Request.HasFormContentType) 
            {
                IFormCollection form = await context.Request.ReadFormAsync(); 
                string? method = form["_method"].FirstOrDefault(); 

                if (!string.IsNullOrEmpty(method)) 
                {
                    
                    context.Request.Method = method.ToUpperInvariant();
                }
            }

            await _next(context); 
        }
    }
}
