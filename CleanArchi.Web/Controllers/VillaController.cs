using CleanArchi.Application.Common.Interfaces;
using CleanArchi.Domain.Entities;
using CleanArchi.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CleanArchi.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;

        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;

		}

		public IActionResult Index()
		{
			var villas = _unitOfWork.Villa.GetAll();
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
		public IActionResult Create(Villa obj)
		{
			if (obj.Name == obj.Description)
			{
				ModelState.AddModelError("name", "説明が名前と一致していません。");
			}

			if (ModelState.IsValid)
			{
				if (obj.Image != null)
				{
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
					string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\VillaImage");
					using (var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
					//アップロードファイルを指定のフォルダにコピー保存する。
					obj.Image.CopyTo(fileStream);
					obj.ImageUrl = @"\images\VillaImage\" + fileName;
				}
				else
				{
					obj.ImageUrl = "https://placehold.co/600x400";
				}

				_unitOfWork.Villa.Add(obj);
				_unitOfWork.Save();
				TempData["success"] = "正常に新規作成しました。";
				return RedirectToAction("Index");
			}
			TempData["error"] = "作成出来ませんでした。";
			return View();

		}

		public IActionResult Update(int villaId)
		{
			Villa? obj = _unitOfWork.Villa.Get(u => u.Id == villaId);
			if (obj == null)
			{
				TempData["error"] = "指定データがありません。";
				return RedirectToAction("Error", "Home");
			}
			return View(obj);
		}

		[HttpPost]
		public IActionResult Update(Villa obj)
		{
			if (ModelState.IsValid && obj.Id > 0)
			{
				_unitOfWork.Villa.Update(obj);
				_unitOfWork.Save();
				TempData["success"] = "正常に更新しました。";
				return RedirectToAction("Index");
			}
			TempData["error"] = "更新出来ませんでした。";
			return View();

		}

		public IActionResult Delete(int villaId)
		{
			Villa? obj = _unitOfWork.Villa.Get(u => u.Id == villaId);
			if (obj == null)
			{
				TempData["error"] = "指定データがありません。";
				return RedirectToAction("Error", "Home");
			}
			return View(obj);
		}

		[HttpPost]
		public IActionResult Delete(Villa obj)
		{
			Villa? objFromDb = _unitOfWork.Villa.Get(u => u.Id == obj.Id);
			if (objFromDb is not null)
			{
				_unitOfWork.Villa.Remove(objFromDb);
				_unitOfWork.Save();
				TempData["success"] = "正常に削除しました。";
				return RedirectToAction("Index");
			}
			TempData["error"] = "削除出来ませんでした。";
			return View();
		}

		//public async Task<IActionResult> Index()
  //      {
  //          var villas = await _db.Villas.ToListAsync();
  //          return View(villas);
  //      }

  //      public IActionResult Create()
  //      {
  //          //int x = 10;
  //          //int y = 0;
  //          //int z = x / y;
  //          return View();
  //      }

  //      [HttpPost]
  //      public async Task<IActionResult> Create(Villa obj)
  //      {
  //          if (ModelState.IsValid)
  //          {
  //              _db.Villas.Add(obj);
  //              await _db.SaveChangesAsync();
  //              TempData["success"] = "正常に新規作成しました。";
  //              return RedirectToAction("Index");
  //          }
  //          TempData["error"] = "作成出来ませんでした。";
  //          return View();

  //      }

  //      public async Task<IActionResult> Update(int villaId)
  //      {
  //          Villa? obj = await _db.Villas.FirstOrDefaultAsync(u=>u.Id==villaId);
  //          if (obj == null)
  //          {
  //              TempData["error"] = "指定データがありません。";
  //              return RedirectToAction("Error", "Home");
  //          }
  //          return View(obj);
  //      }


  //      [HttpPost]
  //      public async Task<IActionResult> Update(Villa obj)
  //      {
  //          if (ModelState.IsValid && obj.Id > 0)
  //          {
  //              _db.Villas.Update(obj);
  //              await _db.SaveChangesAsync();
  //              TempData["success"] = "正常に更新しました。";
  //              return RedirectToAction("Index");
  //          }
  //          TempData["error"] = "更新出来ませんでした。";
  //          return View();

  //      }

  //      public async Task<IActionResult> Delete(int villaId)
  //      {
  //          Villa? obj = await _db.Villas.FirstOrDefaultAsync(u => u.Id == villaId);
  //          if (obj == null)
  //          {
  //              TempData["error"] = "指定データがありません。";
  //              return RedirectToAction("Error", "Home");
  //          }
  //          return View(obj);
  //      }

  //      [HttpPost]
  //      public async Task<IActionResult> Delete(Villa obj)
  //      {
  //          Villa? objFromDb = await _db.Villas.FirstOrDefaultAsync(u => u.Id == obj.Id);
  //          if (objFromDb is not null)
  //          {
  //              _db.Villas.Remove(objFromDb);
  //              await _db.SaveChangesAsync();
  //              TempData["success"] = "正常に削除しました。";
  //              return RedirectToAction("Index");
  //          }
  //          TempData["error"] = "削除出来ませんでした。";
  //          return View();
  //      }
    }
}
