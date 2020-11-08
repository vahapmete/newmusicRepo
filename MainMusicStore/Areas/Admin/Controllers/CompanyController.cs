using MainMusicStore.DataAccess.IMainRepository;
using MainMusicStore.Models.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace MainMusicStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        #region Veriables

        private readonly IUnitOfWork _uow;

        #endregion

        #region CTOR

        public CompanyController(IUnitOfWork uow)
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
            var allObj = _uow.Company.GetAll();
            return Json(new {data = allObj});
        }

        public IActionResult Delete(int id)
        {
            var deleteData = _uow.Company.Get(id);
            if (deleteData == null)
            {
                return Json(new {success = false, message = "Data Not Found!"});
            }

            _uow.Company.Remove(deleteData);
            _uow.Save();
            return Json(new {success = true, message = "Delete Operation Successfully"});
        }

        #endregion

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Company Company = new Company();
            if (id == null)
            {
                return View(Company);
            }

            Company = _uow.Company.Get((int) id);
            if (Company != null)
            {
                return View(Company);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company Company)
        {
            if (ModelState.IsValid)
            {
                if (Company.Id == 0)
                {
                    //create
                    _uow.Company.Add(Company);

                }
                else
                {
                    //update
                    _uow.Company.Update(Company);
                }

                _uow.Save();
                return RedirectToAction("Index");
            }

            return View(Company);
        }

    }
}
