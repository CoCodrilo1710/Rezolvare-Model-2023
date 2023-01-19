using Exam2023.Data;
using Exam2023.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Exam2023.Controllers
{
    public class GiftCardsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        //INDEX
        public IActionResult Index()
        {
            var giftCards = db.GiftCards.Include("Brand");
            ViewBag.giftCards = giftCards;
            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"];
            }

            return View();
        }

        //SHOW
        public IActionResult Show(int id)
        {
            var giftCards = db.GiftCards.Find(id);
            var brands = db.Brands.Find(giftCards.BrandId);
            ViewBag.Brand = brands;
            return View(giftCards);
        }


        //DELETE
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var giftCards = db.GiftCards.Find(id);
            db.GiftCards.Remove(giftCards);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        //NEW
        public IActionResult New()
        {
            GiftCard giftcards = new GiftCard();
            giftcards.Brands = GetAllBrands();
            return View(giftcards);
        }
        [HttpPost]
        public IActionResult New(GiftCard giftcards)
        {
            giftcards.Brands = GetAllBrands();
            if (ModelState.IsValid)
            {
                db.GiftCards.Add(giftcards);
                db.SaveChanges();
                TempData["message"] = "Gift card was added!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(giftcards);
            }

        }

        //EDIT
        public IActionResult Edit(int id)
        {
            GiftCard giftcards = db.GiftCards.Find(id);
            giftcards.Brands = GetAllBrands();
            return View(giftcards);
        }
        [HttpPost]
        public IActionResult Edit(GiftCard giftcards)
        {
            giftcards.Brands = GetAllBrands();
            if (ModelState.IsValid)
            {
                db.GiftCards.Update(giftcards);
                db.SaveChanges();
                TempData["message"] = "Gift card was Updated!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(giftcards);
            }

        }

        //SEARCH
        public IActionResult Search(string search)
        {
            var giftCards = from item in
                                db.GiftCards.Include("Brand")
                            where (
                                item.Denumire.Contains(search)
                                && item.Procent >= 30
                                && item.DataExp > System.DateTime.Now.AddDays(5)
                            )
                            orderby item.DataExp descending
                            select item;
            ViewBag.giftCards = giftCards;
            return View();
        }
        
        
        private IEnumerable<SelectListItem> GetAllBrands()
        {
            var selectList = new List<SelectListItem>();
            var brands = db.Brands.ToList();
            foreach (var brand in brands)
            {
                selectList.Add(new SelectListItem
                {
                    Value = brand.Id.ToString(),
                    Text = brand.Nume.ToString()
                });
            }
            return selectList;
        }
    }
}
