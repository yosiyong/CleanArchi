using CleanArchi.Domain.Entities;
using CleanArchi.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CleanArchi.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var villas = await _db.Villas.ToListAsync();
            return View(villas);
        }

        public IActionResult Create()
        {
            //int x = 10;
            //int y = 0;
            //int z = x / y;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Villa obj)
        {
            if (ModelState.IsValid)
            {
                _db.Villas.Add(obj);
                await _db.SaveChangesAsync();
                TempData["success"] = "正常に新規作成しました。";
                return RedirectToAction("Index");
            }
            TempData["error"] = "作成出来ませんでした。";
            return View();

        }

        public async Task<IActionResult> Update(int villaId)
        {
            Villa? obj = await _db.Villas.FirstOrDefaultAsync(u=>u.Id==villaId);
            if (obj == null)
            {
                TempData["error"] = "指定データがありません。";
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }


        [HttpPost]
        public async Task<IActionResult> Update(Villa obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                _db.Villas.Update(obj);
                await _db.SaveChangesAsync();
                TempData["success"] = "正常に更新しました。";
                return RedirectToAction("Index");
            }
            TempData["error"] = "更新出来ませんでした。";
            return View();

        }

        public async Task<IActionResult> Delete(int villaId)
        {
            Villa? obj = await _db.Villas.FirstOrDefaultAsync(u => u.Id == villaId);
            if (obj == null)
            {
                TempData["error"] = "指定データがありません。";
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Villa obj)
        {
            Villa? objFromDb = await _db.Villas.FirstOrDefaultAsync(u => u.Id == obj.Id);
            if (objFromDb is not null)
            {
                _db.Villas.Remove(objFromDb);
                await _db.SaveChangesAsync();
                TempData["success"] = "正常に削除しました。";
                return RedirectToAction("Index");
            }
            TempData["error"] = "削除出来ませんでした。";
            return View();
        }
    }
}
