using ManageMenu.DBContext.Repositories;
using ManageMenu.Entities;
using ManageMenu.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageMenu.Services
{
    public class MenuService : IMenuService
    {
        private readonly MenuDBContext _context;

        public MenuService(MenuDBContext menuDBContext)
        {
            _context = menuDBContext;
        }

        public async Task<bool> CreateMenuAsync(Guid root, string content)
        {
            var checkLevel = await (from m in _context.Menu
                                    where m.Id == root
                                    select m).FirstOrDefaultAsync();
            if (checkLevel == null)
            {
                return false;
            }

            var numChildren = await (from m in _context.Menu
                                     where m.RootId == root
                                     select m).ToListAsync();

            var newLevel = new Menu()
            {
                Id = root,
                Level = checkLevel.Level + 1,
                Content = content,
                Root = null,
                RootId = checkLevel.Id,
            };

            var result = await _context.Menu.AddAsync(newLevel);
            await _context.SaveChangesAsync();

            if (result.State == EntityState.Added)
            {
                return true;
            }
            return false;
        }

        public Task<bool> DeleteMenuAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditMenuAsync(Guid root)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MenuViewModel>?> ShowMenuAsync(Guid id)
        {
            var checkLevel = await (from m in _context.Menu
                                    where m.Id == id
                                    select m).FirstOrDefaultAsync();
            if (checkLevel == null)
            {
                return null;
            }

            var numChildren = await (from m in _context.Menu
                                     where m.RootId == id
                                     select new MenuViewModel
                                     {
                                         Id = m.Id,
                                         Content = m.Content,
                                         Children = null,
                                     }).ToListAsync();
            
            return numChildren;
        }
    }
}
