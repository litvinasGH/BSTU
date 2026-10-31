using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using static DAL_Celebrity_MSSQL.DAL_Celebrity_MSSQL;

namespace ASPA008_1.Filters
{
    public class InfoAsyncActionFilter : Attribute, IAsyncActionFilter
    {
        public static readonly string Wikipedia = "WIKI";
        public static readonly string Facebook = "FACE";

        string infotype;
        public InfoAsyncActionFilter(string infotype = "")
        {
            this.infotype = infotype;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IRepository? repo = context.HttpContext.RequestServices.GetService<IRepository>();
            int id = (int)(context.ActionArguments["id"] ?? -1);
            Celebrity? celebrity = repo?.GetCelebrityById(id);
            if (infotype.ToUpper().Contains(Wikipedia) && celebrity != null)
                context.HttpContext.Items.Add(Wikipedia, await WikiInfoCelebrity.GetReferences(celebrity.FullName));
            if (infotype.ToUpper().Contains(Facebook) && celebrity != null)
                context.HttpContext.Items.Add(Facebook, getFromFace(celebrity.FullName));
            await next();
        }

        string getFromWiki(string fullname)
        {
            string rc = "Info from Wiki";
            // WikiClient request to Wikipedia
            return rc;
        }

        string getFromFace(string fullname)
        {
            string rc = "Info from Face";
            // FacebookClient request to Facebook
            return rc;
        }


        public class WikiInfoCelebrity
        {
            HttpClient client;
            string wikiURItemplate = "https://en.wikipedia.org/w/api.php?action=opensearch&search=\"{0}\"&prop=info&format=json";
            Dictionary<string, string> wikiReferences { get; set; }
            string wikiURI;
            private WikiInfoCelebrity(string fullName)
            {
                this.client = new HttpClient();
                this.wikiReferences = new Dictionary<string, string>();
                this.wikiURItemplate = fullName;
                this.wikiURI = string.Format("https://en.wikipedia.org/w/api.php?action=opensearch&search={0}&prop=info&format=json", fullName);
            }

            public static async Task<Dictionary<string, string>> GetReferences(string fullName)
            {
                WikiInfoCelebrity info = new WikiInfoCelebrity(fullName);
                HttpResponseMessage message = await info.client.GetAsync(info.wikiURI);
                if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<dynamic>? result = await message.Content.ReadFromJsonAsync<List<dynamic>>() ?? default(List<dynamic>);
                    List<string>? ls1 = JsonSerializer.Deserialize<List<string>>(result[1]);
                    List<string>? ls3 = JsonSerializer.Deserialize<List<string>>(result[3]);
                    for (int i = 0; i < ls1.Count; i++)
                    {
                        info.wikiReferences.Add(ls1[i], ls3[i]);
                    }
                }
                return info.wikiReferences;
            }
        }
    }

}
