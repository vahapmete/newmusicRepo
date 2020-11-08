using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainMusicStore.Data;
using MainMusicStore.DataAccess.IMainRepository;
using MainMusicStore.Models.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace MainMusicStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _uow;

        public CoverTypeController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            return View();
        }
        #region APİ CALLS

        public IActionResult GetAll()
        {
            var allObj = _uow.CoverType.GetAll();
            return Json(new { data = allObj });
        }

        public IActionResult Delete(int id)
        {
            var deleteData = _uow.CoverType.Get(id);
            if (deleteData == null)
            {
                return Json(new { success = false, message = "Data Not Found" });
            }
            _uow.CoverType.Remove(deleteData);
            _uow.Save();
            return Json(new {success = true, message = "Delete Operation successfully"});
        }
        #endregion

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if (id == null)
            {
                return View(coverType);
            }

            coverType = _uow.CoverType.Get((int)id);
            if (coverType != null)
            {
                return View(coverType);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                if (coverType.Id==0)
                {
                    _uow.CoverType.Add(coverType);
                }
                else
                {
                    _uow.CoverType.Update(coverType);
                }
                _uow.Save();
                return RedirectToAction("Index");
            }

            return View(coverType);
        }

    }

}
