using ANC25_WEBAPl_DLL;
using ASPA008_1.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static DAL_Celebrity_MSSQL.DAL_Celebrity_MSSQL;

namespace ASPA008_1.Controllers
{
    public class CelebritiesController : Controller
    {
        IRepository repo;
        IOptions<CelebritiesConfig> config;
        public CelebritiesController(IRepository repo, IOptions<CelebritiesConfig> config)
        {
            this.repo = repo;
            this.config = config;
        }

        public record IndexModel(string PhotosRequestPath, List<Celebrity> Celebrities);
        public IActionResult Index()
        {
            return View(new IndexModel(config.Value.PhotosRequestPath, repo.GetAllCelebrities()));
        }
        public record HumanModel(string photosrequestpath, Celebrity celebrity, List<Lifeevent> lifeevents, Dictionary<string, string>? references);
        [InfoAsyncActionFilter(infotype: "Wikipedia, Facebook")]
        public IActionResult Human(int id)
        {
            IActionResult rc = NotFound();
            Celebrity? celebrity = repo.GetCelebrityById(id);
           Dictionary<string, string>? references = (Dictionary<string, string>?) HttpContext.Items[InfoAsyncActionFilter.Wikipedia];

            if (celebrity != null) rc = View(new HumanModel(config.Value.PhotosRequestPath,
                                                            (Celebrity)celebrity, repo.GetLifeeventsByCelebrityId(id), references));
            return rc;
        }
        public IActionResult NewHumanForm()
        {
            return View("NewHumanForm", config.Value.PhotosRequestPath);
        }
    }
}
