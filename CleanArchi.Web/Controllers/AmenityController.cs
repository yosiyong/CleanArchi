using CleanArchi.Application.Common.Interfaces;
using CleanArchi.Domain.Entities;
using CleanArchi.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchi.Web.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //var amenities = _db.VillaNumbers.Include(u => u.Villa).ToList();
            var amenities = _unitOfWork.Amenity.GetAll(includeProperties: nameof(Villa));

            return View(amenities);
        }

        public IActionResult Create()
        {
            AmenityVM amenityVM = new()
            {
                //AmenityList = _db.Villas.ToList().Select(u => new SelectListItem
                //{
                //	Text = u.Name,
                //	Value = u.Id.ToString(),
                //})

                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                })
            };

            return View(amenityVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AmenityVM obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Amenity.Add(obj.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "正常に作成しました。";
                return RedirectToAction(nameof(Index));
            }

            obj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });
            return View(obj);

        }

        public IActionResult Update(int amenityId)
        {

            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityId)
            };

            if (amenityVM.Amenity == null)
            {
                TempData["error"] = "指定データがありません。";
                return RedirectToAction("Error", "Home");
            }
            return View(amenityVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(AmenityVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Amenity.Update(obj.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "正常に更新しました。";
                return RedirectToAction(nameof(Index));
            }

            obj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });
            return View(obj);

        }

        public IActionResult Delete(int amenityId)
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),

                Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityId)
            };

            if (amenityVM.Amenity == null)
            {
                TempData["error"] = "指定データがありません。";
                return RedirectToAction("Error", "Home");
            }
            return View(amenityVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(AmenityVM obj)
        {
            //Amenity? objFromDb = _db.VillaNumbers.FirstOrDefault(u => u.Id == obj.Amenity.Id);
            Amenity? objFromDb = _unitOfWork.Amenity.Get(u => u.Id == obj.Amenity.Id);
            if (objFromDb is not null)
            {
                _unitOfWork.Amenity.Remove(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "正常に削除しました。";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "削除出来ませんでした。";
            return View(obj);
        }

        //public async Task<IActionResult> Index()
        //{
        //    var amenities = await _db.VillaNumbers.Include(u=>u.Villa).ToListAsync();

        //    return View(amenities);
        //}

        //public async Task<IActionResult> Create()
        //{
        //    AmenityVM amenityVM = new()
        //    {
        //        AmenityList = (await _db.Villas.ToListAsync()).Select(u => new SelectListItem
        //        {
        //            Text = u.Name,
        //            Value = u.Id.ToString(),
        //        })
        //    };

        //    //IEnumerable<SelectListItem> list = (await _db.Villas.ToListAsync()).Select(u => new SelectListItem
        //    //{
        //    //    Text = u.Name,
        //    //    Value = u.Id.ToString(),
        //    //});

        //    ////ViewData["AmenityList"] = list;
        //    //ViewBag.AmenityList = list;
        //    return View(amenityVM);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(AmenityVM obj)
        //{
        //    //ルーム番号存在チェック
        //    bool roomNumberExists = _db.VillaNumbers.Any(u => u.Id == obj.Amenity.Id);

        //    if (ModelState.IsValid && !roomNumberExists)
        //    {
        //        _db.VillaNumbers.Add(obj.Amenity);
        //        await _db.SaveChangesAsync();
        //        TempData["success"] = "正常に作成しました。";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    if (roomNumberExists)
        //    {
        //        TempData["error"] = "指定部屋番号は既に存在する番号です。";
        //    }
        //    else
        //    {
        //        TempData["error"] = "作成出来ませんでした。";
        //    }

        //    obj.AmenityList = (await _db.Villas.ToListAsync()).Select(u => new SelectListItem
        //    {
        //        Text = u.Name,
        //        Value = u.Id.ToString(),
        //    });
        //    return View(obj);

        //}

        //public async Task<IActionResult> Update(int amenityId)
        //{

        //    AmenityVM amenityVM = new()
        //    {
        //        AmenityList = (await _db.Villas.ToListAsync()).Select(u => new SelectListItem
        //        {
        //            Text = u.Name,
        //            Value = u.Id.ToString(),
        //        }),
        //        Amenity = await _db.VillaNumbers.AsNoTracking().FirstOrDefaultAsync(u => u.Id == amenityId)
        //    };

        //    if (amenityVM.Amenity == null)
        //    {
        //        TempData["error"] = "指定データがありません。";
        //        return RedirectToAction("Error", "Home");
        //    }
        //    return View(amenityVM);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Update(AmenityVM obj)
        //{

        //    if (ModelState.IsValid )
        //    {
        //        _db.VillaNumbers.Update(obj.Amenity);
        //        await _db.SaveChangesAsync();
        //        TempData["success"] = "正常に更新しました。";
        //        return RedirectToAction(nameof(Index));
        //    }

        //    obj.AmenityList = (await _db.Villas.ToListAsync()).Select(u => new SelectListItem
        //    {
        //        Text = u.Name,
        //        Value = u.Id.ToString(),
        //    });
        //    return View(obj);

        //}

        //public async Task<IActionResult> Delete(int amenityId)
        //{
        //    AmenityVM amenityVM = new()
        //    {
        //        AmenityList = (await _db.Villas.ToListAsync()).Select(u => new SelectListItem
        //        {
        //            Text = u.Name,
        //            Value = u.Id.ToString(),
        //        }),
        //        Amenity = await _db.VillaNumbers.AsNoTracking().FirstOrDefaultAsync(u => u.Id == amenityId)
        //    };

        //    if (amenityVM.Amenity == null)
        //    {
        //        TempData["error"] = "指定データがありません。";
        //        return RedirectToAction("Error", "Home");
        //    }
        //    return View(amenityVM);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(AmenityVM obj)
        //{
        //    Amenity? objFromDb = await _db.VillaNumbers.FirstOrDefaultAsync(u => u.Id == obj.Amenity.Id);
        //    if (objFromDb is not null)
        //    {
        //        _db.VillaNumbers.Remove(objFromDb);
        //        await _db.SaveChangesAsync();
        //        TempData["success"] = "正常に削除しました。";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    TempData["error"] = "削除出来ませんでした。";
        //    return View(obj);
        //}
    }
}
