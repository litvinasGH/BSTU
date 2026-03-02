using ANC25_WEBAPl_DLL;
using ASPA008_1.Filters;
using ASPA008_1.Models;
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
            Dictionary<string, string>? references = (Dictionary<string, string>?)HttpContext.Items[InfoAsyncActionFilter.Wikipedia];

            if (celebrity != null) rc = View(new HumanModel(config.Value.PhotosRequestPath,
                                                            (Celebrity)celebrity, repo.GetLifeeventsByCelebrityId(id), references));
            return rc;
        }

        // ---- CREATE / PREVIEW ----

        [HttpGet]
        public IActionResult NewHumanForm(int? id, FormMode mode = FormMode.Create)
        {
            var pathReq = config.Value.PhotosRequestPath;
            NewHumanFormModel model;

            if ((mode == FormMode.Edit || mode == FormMode.Delete) && id.HasValue)
            {
                // загружаем из БД
                var c = repo.GetCelebrityById(id.Value);
                if (c == null) return RedirectToAction("Index");

                model = new NewHumanFormModel
                {
                    Mode = mode,
                    Id = c.id,
                    FullName = c.FullName,
                    Nationality = c.Nationality,
                    TempFileName = c.ReqPhotoPath,
                    Confirm = mode == FormMode.Delete,                // сразу readonly preview
                    PhotosRequestPath = pathReq
                };
            }
            else
            {
                // чистая Create
                model = new NewHumanFormModel
                {
                    Mode = FormMode.Create,
                    PhotosRequestPath = pathReq
                };
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> NewHumanForm(
            int? id,
            string? fullname,
            string? nationality,
            IFormFile? upload,
            string? press,
            string? tempfilename,
            FormMode mode)
        {
            var folder = config.Value.PhotosFolder;
            var pathReq = config.Value.PhotosRequestPath;

            // ==== DELETE CONFIRM ====
            if (mode == FormMode.Delete && press == "Confirm")
            {
                repo.DelCelebrity(id!.Value);
                return RedirectToAction("Index");
            }
            // ==== CREATE / EDIT PREVIEW ====
            if ((mode == FormMode.Create)
                && upload != null && press == null)
            {
                // сохраняем временно
                var tmp = Path.GetFileName(Path.GetTempFileName());
                await using var fs = System.IO.File.Create(Path.Combine(folder, tmp));
                await upload.CopyToAsync(fs);

                return View(new NewHumanFormModel
                {
                    Mode = mode,
                    Id = id,
                    FullName = fullname,
                    Nationality = nationality,
                    TempFileName = tmp,
                    Confirm = true,
                    PhotosRequestPath = pathReq
                });
            }

            // ==== CONFIRM CREATE / EDIT ====
            if (press == "Confirm")
            {
                // финальное имя и перемещение
                var namePart = fullname!.Replace(" ", "_");
                var finalName = $"{namePart}.{tempfilename}.jpg";
                System.IO.File.Move(Path.Combine(folder, tempfilename),
                                    Path.Combine(folder, finalName),
                                    overwrite: true);

                if (mode == FormMode.Create)
                {
                    repo.AddCelebrity(new Celebrity
                    {
                        FullName = fullname,
                        Nationality = nationality,
                        ReqPhotoPath = finalName
                    });
                }
                else // Edit
                {
                    var celeb = repo.GetCelebrityById(id!.Value);
                    celeb!.FullName = fullname;
                    celeb.Nationality = nationality;
                    celeb.ReqPhotoPath = finalName;
                    repo.UpdCelebrity(celeb);
                }
                return RedirectToAction("Index");
            }

            

            // ==== CANCEL or initial GET for Edit/Delete ====
            if (press == "Cancel" || press == null)
            {
                if (mode == FormMode.Edit && id.HasValue)
                {
                    var c = repo.GetCelebrityById(id.Value)!;
                    return View(new NewHumanFormModel
                    {
                        Mode = FormMode.Edit,
                        Id = c.id,
                        FullName = c.FullName,
                        Nationality = c.Nationality,
                        TempFileName = c.ReqPhotoPath,   // уже сохранённый файл
                        Confirm = true,                  // сразу показать readonly preview
                        PhotosRequestPath = pathReq
                    });
                }
                if (mode == FormMode.Delete && id.HasValue)
                {
                    var c = repo.GetCelebrityById(id.Value)!;
                    return View(new NewHumanFormModel
                    {
                        Mode = FormMode.Delete,
                        Id = c.id,
                        FullName = c.FullName,
                        Nationality = c.Nationality,
                        TempFileName = c.ReqPhotoPath,
                        Confirm = true,
                        PhotosRequestPath = pathReq
                    });
                }
            }

            // По умолчанию вернуть пустую форму Create
            return RedirectToAction("NewHumanForm");
        }

        // ---- EDIT BUTTON ----
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return RedirectToAction("NewHumanForm", new { id, mode = FormMode.Edit });
        }

        // ---- DELETE BUTTON ----
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return RedirectToAction("NewHumanForm", new { id, mode = FormMode.Delete });
        }
    }
}
