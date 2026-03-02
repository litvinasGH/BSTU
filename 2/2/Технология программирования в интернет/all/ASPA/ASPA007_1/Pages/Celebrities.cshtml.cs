using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSSQL = DAL_Celebrity_MSSQL.DAL_Celebrity_MSSQL;
using REPO = DAL_Celebrity;
using ANC25_WEBAPl_DLL;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Extensions.Options;
namespace ASPA007_1.Pages
{
    public class CelebritiesModel : PageModel
    {
        public List<MSSQL.Celebrity> Celebrities { get; set; }
       = new List<MSSQL.Celebrity>();

        public string? PhotosRequestPath;
        MSSQL.IRepository repo;


        public CelebritiesModel(MSSQL.IRepository repo, IOptions<CelebritiesConfig> conf)
        {
            this.repo = repo;
            PhotosRequestPath = conf.Value.PhotosRequestPath;
        }
        public ActionResult OnGet()
        {
            this.Celebrities = this.repo.GetAllCelebrities().ToList();
            return Page();
        }
    }
}
