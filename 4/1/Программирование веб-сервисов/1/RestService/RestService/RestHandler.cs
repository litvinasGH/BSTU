using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace Lab01
{
    internal class Lab01Service : IHttpHandler, IRequiresSessionState
    {


        private static int baseResult = 0;
        public static int BaseResult
        {
            get { return baseResult; }
            set { baseResult = value; }
        }

        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {

            Stack<int> stack = context.Session["Lab01.Stack"] as Stack<int>;

            if (stack == null)
            {
                stack = new Stack<int>();
                context.Session["Lab01.Stack"] = stack;
            }


            switch (context.Request.HttpMethod)
            {
                case "GET":

                    break;

                case "POST":
                    {
                        int value;
                        if (!TryReadValue(context, "RESULT", out value))
                        {
                            return;
                        }

                        BaseResult = value;
                        break;

                    }

                case "PUT":
                    {
                        int value;
                        if (!TryReadValue(context, "ADD", out value))
                        {
                            return;
                        }
                        stack.Push(value);
                        break;
                    }
                case "DELETE":
                    {
                        if (stack.Count <= 0)
                        {
                            JsonReply.Write(context, 409, new { Error = "Стек пуст" });
                            return;
                        }
                        stack.Pop();
                    }
                    break;
                default:

                    JsonReply.Write(context, 405, new { Error = "Неизвестный метод. Допутимы GET, POST, PUT, DELETE" });
                    return;

            }

            long top = (stack.Count == 0) ? 0 : stack.Peek();

            long Result = top + BaseResult;

            JsonReply.Write(context, 200, new { RESULT = Result });

        }


        public bool TryReadValue(HttpContext context, string name, out int value)
        {
            string rawValue = context.Request.QueryString[name];


            if (int.TryParse(rawValue, out value))
            {
                return true;
            }


            context.Response.AddHeader("Allow", "GET, POST, PUT, DELETE");
            JsonReply.Write(context, 400, new
            {
                error = "Параметр " + name + " должен быть целым числом в пределах int"

            });


            return false;
        }
    }


    public sealed class DemoHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            using (Stream stream = typeof(DemoHandler).Assembly
                .GetManifestResourceStream("RestService.index.html"))
            {
                if (stream == null)
                {
                    JsonReply.Write(context, 500, new
                    {
                        error = "Ресурс RestService.index.html не встроен в DLL."
                    });
                    return;
                }

                context.Response.ContentType = "text/html";
                context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.Cache.SetNoStore();

                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    context.Response.Write(reader.ReadToEnd());
                }
            }
        }
    }


    public static class JsonReply
    {
        public static void Write(HttpContext context, int statusCode, object body)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(new JavaScriptSerializer().Serialize(body));
        }
    }

}

