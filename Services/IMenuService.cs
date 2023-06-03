using ManageMenu.Models;

namespace ManageMenu.Services
{
    public interface IMenuService
    {
        public Task<List<MenuViewModel>?> ShowMenuAsync(Guid? id);
        public Task<MenuViewModel> CreateMenuAsync(MenuViewModel menuViewModel);
        public Task<bool> EditMenuAsync(MenuViewModel menuViewModel);
        public Task<bool> DeleteMenuAsync(Guid id);
    }
}
