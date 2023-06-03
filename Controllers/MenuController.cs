using ManageMenu.Models;
using ManageMenu.Services;
using MenuManagement.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ManageMenu.Controllers
{
    [Route("Menu")]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService) : base()
        {
            _menuService = menuService;
        }

        [HttpGet("show")]
        public async Task<IActionResult> Show()
        {
            return View(new List<MenuViewModel>());
        }

        [HttpPost("show")]
        public async Task<IActionResult> Show([FromQuery] Guid? id)
        {
            if (!ModelState.IsValid)
            {
                return View(new List<MenuViewModel>());
            }

            var listMenu = await _menuService.ShowMenuAsync(id);
            if (listMenu == null)
                listMenu = new List<MenuViewModel>();
            return View(listMenu);
        }

        [HttpPost("show-details")]
        public async Task<IActionResult> ShowDetail([FromForm] string? dataListMenu, [FromForm] Guid id, bool shrink = false)
        {
            if (dataListMenu == null)
                return View("show", new List<MenuViewModel>());

            List<MenuViewModel>? listMenu = JsonSerializer.Deserialize<List<MenuViewModel>>(dataListMenu);
            if (listMenu == null)
                listMenu = new List<MenuViewModel>();

            if (shrink)
            {
                Helpers.ShrinkList(ref listMenu, id);
                return View("show", listMenu);
            }

            var children = await _menuService.ShowMenuAsync(id);
            if (children != null || children.Count != 0)
                Helpers.AppendChild(ref listMenu, id, children);
            return View("show", listMenu);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMenuAsync([FromForm] string? dataListMenu, [FromForm] MenuViewModel menuViewModel)
        {
            if (dataListMenu == null)
                return View(new List<MenuViewModel>());

            List<MenuViewModel> listMenu = JsonSerializer.Deserialize<List<MenuViewModel>>(dataListMenu);
            var create = await _menuService.CreateMenuAsync(menuViewModel);

            if (create == null)
            {
                Console.WriteLine("Tạo không thành công");
                return View("show", listMenu);
            }

            if (listMenu == null)
                listMenu = new List<MenuViewModel>();

            if (menuViewModel.Id == Guid.Empty)
            {
                listMenu.Add(create);
            }
            else
            {
                Helpers.AppendChild(ref listMenu, menuViewModel.Id, create);
            }

            return View("show", listMenu);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromForm] string? dataListMenu, [FromForm] MenuViewModel menuViewModel)
        {
            if (dataListMenu == null)
                return View("show", new List<MenuViewModel>());

            List<MenuViewModel> listMenu = JsonSerializer.Deserialize<List<MenuViewModel>>(dataListMenu);
            var edit = await _menuService.EditMenuAsync(menuViewModel);

            if (listMenu == null)
                listMenu = new List<MenuViewModel>();

            if (!edit)
            {
                Console.WriteLine("Chỉnh sửa không thành công");
                return View("show", listMenu);
            }

            Helpers.EditContent(ref listMenu, menuViewModel);

            return View("show", listMenu);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAsync([FromForm] string? dataListMenu, [FromForm] Guid id)
        {
            if (dataListMenu == null)
                return View("show", new List<MenuViewModel>());

            List<MenuViewModel> listMenu = JsonSerializer.Deserialize<List<MenuViewModel>>(dataListMenu);
            var delete = await _menuService.DeleteMenuAsync(id);

            if (listMenu == null)
                listMenu = new List<MenuViewModel>();

            if (!delete)
            {
                Console.WriteLine("Xóa không thành công");
                return View("show", listMenu);
            }

            Helpers.RemoveItem(ref listMenu, id);

            return View("show", listMenu);
        }
    }
}
