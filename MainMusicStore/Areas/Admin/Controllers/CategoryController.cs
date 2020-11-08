using MainMusicStore.DataAccess.IMainRepository;
using MainMusicStore.Models.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace MainMusicStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        #region Veriables

        private readonly IUnitOfWork _uow;

        #endregion

        #region CTOR

        public CategoryController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        #endregion

        #region Actions

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region APICALL

        public IActionResult GetAll()
        {
            var allObj = _uow.category.GetAll();
            return Json(new {data = allObj});
        }

        public IActionResult Delete(int id)
        {
            var deleteData = _uow.category.Get(id);
            if (deleteData == null)
            {
                return Json(new {success = false, message = "Data Not Found!"});
            }

            _uow.category.Remove(deleteData);
            _uow.Save();
            return Json(new {success = true, message = "Delete Operation Successfully"});
        }

        #endregion

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null)
            {
                return View(category);
            }

            category = _uow.category.Get((int) id);
            if (category != null)
            {
                return View(category);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    //create
                    _uow.category.Add(category);

                }
                else
                {
                    //update
                    _uow.category.Update(category);
                }

                _uow.Save();
                return RedirectToAction("Index");
            }

            return View(category);
        }

    }
}
