using CleanArchi.Domain.Entities;
using CleanArchi.Infrastructure.Data;
using CleanArchi.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CleanArchi.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VillaNumberController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var villaNumbers = await _db.VillaNumbers.Include(u=>u.Villa).ToListAsync();
            
            return View(villaNumbers);
        }

        public async Task<IActionResult> Create()
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = (await _db.Villas.ToListAsync()).Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                })
            };

            //IEnumerable<SelectListItem> list = (await _db.Villas.ToListAsync()).Select(u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.Id.ToString(),
            //});

            ////ViewData["VillaList"] = list;
            //ViewBag.VillaList = list;
            return View(villaNumberVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VillaNumberVM obj)
        {
            //ルーム番号存在チェック
            bool roomNumberExists = _db.VillaNumbers.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);

            if (ModelState.IsValid && !roomNumberExists)
            {
                _db.VillaNumbers.Add(obj.VillaNumber);
                await _db.SaveChangesAsync();
                TempData["success"] = "正常に作成しました。";
                return RedirectToAction(nameof(Index));
            }
            if (roomNumberExists)
            {
                TempData["error"] = "指定部屋番号は既に存在する番号です。";
            }
            else
            {
                TempData["error"] = "作成出来ませんでした。";
            }

            obj.VillaList = (await _db.Villas.ToListAsync()).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });
            return View(obj);

        }

        public async Task<IActionResult> Update(int villaNumberId)
        {

            VillaNumberVM villaNumberVM = new()
            {
                VillaList = (await _db.Villas.ToListAsync()).Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                VillaNumber = await _db.VillaNumbers.AsNoTracking().FirstOrDefaultAsync(u => u.Villa_Number == villaNumberId)
            };
            
            if (villaNumberVM.VillaNumber == null)
            {
                TempData["error"] = "指定データがありません。";
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(VillaNumberVM obj)
        {

            if (ModelState.IsValid )
            {
                _db.VillaNumbers.Update(obj.VillaNumber);
                await _db.SaveChangesAsync();
                TempData["success"] = "正常に更新しました。";
                return RedirectToAction(nameof(Index));
            }

            obj.VillaList = (await _db.Villas.ToListAsync()).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });
            return View(obj);

        }

        public async Task<IActionResult> Delete(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = (await _db.Villas.ToListAsync()).Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                VillaNumber = await _db.VillaNumbers.AsNoTracking().FirstOrDefaultAsync(u => u.Villa_Number == villaNumberId)
            };

            if (villaNumberVM.VillaNumber == null)
            {
                TempData["error"] = "指定データがありません。";
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VillaNumberVM obj)
        {
            VillaNumber? objFromDb = await _db.VillaNumbers.FirstOrDefaultAsync(u => u.Villa_Number == obj.VillaNumber.Villa_Number);
            if (objFromDb is not null)
            {
                _db.VillaNumbers.Remove(objFromDb);
                await _db.SaveChangesAsync();
                TempData["success"] = "正常に削除しました。";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "削除出来ませんでした。";
            return View(obj);
        }
    }
}
