using System.Globalization;

namespace AspDotNetLabs.Classes
{
    public class SecretMiddleware
    {
        private readonly RequestDelegate _next;
       
     
        public SecretMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path;
            if (path!=null&&path.Contains("/secret")==true)
            {

                await context.Response.WriteAsync($"SECRET INFORMATION:{GetSecretInfo(path,context)}");
            }

            await _next(context);
        }

        private string GetSecretInfo(string secret,HttpContext context)
        {
            int index = secret.IndexOf("-");
            if(index==-1) {
                return "missing secret infomation code paramenter";
            }
            string code = secret.Substring(index+1);
            return GetSecretInfoByCode(context,code);
        }
        private string GetSecretInfoByCode(HttpContext context,string code)
        {
            
            Dictionary<string, string> _secrets = new Dictionary<string, string>() {
                {"e2d33",$"Protocol: {context.Request.Protocol}" },  
                {"123",$"Method: {context.Request.Method}"},  
                {"gptp2",$"Path: {context.Request.Path}"},  
                {"gecent2",$"Content Encoding: {context.Request.Headers.ContentEncoding.ToString()??"no infomation is available"}"},  
            };
            if (_secrets.ContainsKey(code))
            {
                return _secrets[code];
            }

            return "You haven't access to this infomation";
        }

    }
}
