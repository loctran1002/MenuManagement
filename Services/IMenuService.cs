using ManageMenu.Models;

namespace ManageMenu.Services
{
    public interface IMenuService
    {
        public Task<List<MenuViewModel>?> ShowMenuAsync(Guid id);
        public Task<bool> CreateMenuAsync(Guid root, string content);
        public Task<bool> EditMenuAsync(Guid root);
        public Task<bool> DeleteMenuAsync(Guid id);
    }
}
