using ManageMenu.Models;
using ManageMenu.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageMenu.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService) : base()
        {
            _menuService = menuService;
        }

        [HttpGet("/Show")]
        public IActionResult Show()
        {
            return View(new MenuViewModel());
        }

        // GET: MenuController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        [HttpPost("/Create")]
        public async Task<IActionResult> Create(MenuViewModel menuViewModel)
        {
            menuViewModel.Id = new Guid();
            menuViewModel.Content = "Menu 1";
            if (!ModelState.IsValid)
            {
                return View(menuViewModel);
            }

            var create = await _menuService.CreateMenuAsync(menuViewModel.Id, menuViewModel.Content);

            if (!create)
            {
                Console.WriteLine("Tạo không thành công");
                return View(menuViewModel);
            }

            return RedirectToAction("Show", "Menu");
        }

        // GET: MenuController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenuController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
