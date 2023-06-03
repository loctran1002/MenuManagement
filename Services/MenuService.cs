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

        public async Task<MenuViewModel> CreateMenuAsync(MenuViewModel menuViewModel)
        {
            var checkLevel = await (from m in _context.Menu
                                    where m.Id == menuViewModel.Id
                                    select m).FirstOrDefaultAsync();
            if (checkLevel == null)
            {
                var newChild = new Menu()
                {
                    Id = menuViewModel.Id == Guid.Empty ? Guid.NewGuid() : menuViewModel.Id,
                    Level = 1,
                    Content = menuViewModel.Content == null ? "Menu" : menuViewModel.Content,
                    RootId = null,
                };

                var res = await _context.Menu.AddAsync(newChild);
                await _context.SaveChangesAsync();

                if (res.State == EntityState.Added || res.State == EntityState.Unchanged)
                {
                    return new MenuViewModel()
                    {
                        Id = newChild.Id,
                        Content = newChild.Content,
                        Children = null,
                    };
                }

                return null;
            }

            var numChildren = await (from m in _context.Menu
                                     where m.RootId == menuViewModel.Id
                                     select m).ToListAsync();

            var newLevel = new Menu()
            {
                Id = Guid.NewGuid(),
                Level = checkLevel.Level + 1,
                Content = checkLevel.Content + " - " + (numChildren.Count() + 1).ToString(),
                Root = null,
                RootId = checkLevel.Id,
            };

            var result = await _context.Menu.AddAsync(newLevel);
            await _context.SaveChangesAsync();

            if (result.State == EntityState.Added || result.State == EntityState.Unchanged)
            {
                return new MenuViewModel()
                {
                    Id = newLevel.Id,
                    Content = newLevel.Content,
                    Children = null,
                };
            }

            return null;
        }

        public async Task<bool> DeleteMenuAsync(Guid id)
        {
            var item = await (from m in _context.Menu
                                    where m.Id == id
                                    select m).FirstOrDefaultAsync();
            if (item == null)
            {
                return false;
            }

            var res = _context.Menu.Remove(item);
            await _context.SaveChangesAsync();

            if (res.State == EntityState.Deleted || res.State == EntityState.Detached)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> EditMenuAsync(MenuViewModel menuViewModel)
        {
            var item = await (from m in _context.Menu
                              where m.Id == menuViewModel.Id
                              select m).FirstOrDefaultAsync();
            if (item == null)
            {
                return false;
            }

            item.Content = menuViewModel.Content;

            var res = _context.Menu.Update(item);
            await _context.SaveChangesAsync();

            if (res.State == EntityState.Modified || res.State == EntityState.Unchanged)
            {
                return true;
            }

            return false;
        }

        public async Task<List<MenuViewModel>?> ShowMenuAsync(Guid? id)
        {
            var checkLevel = await (from m in _context.Menu
                                    where m.Id == id
                                    select m).FirstOrDefaultAsync();
            if (checkLevel == null)
            {
                var rootLevel = await (from m in _context.Menu
                                       where m.Level == 1
                                       orderby m.Content ascending
                                       select new MenuViewModel
                                       {
                                           Id = m.Id,
                                           Content = m.Content,
                                       }).ToListAsync();
                return rootLevel;
            }

            var numChildren = await (from m in _context.Menu
                                     where m.RootId == id
                                     orderby m.Content ascending
                                     select new MenuViewModel
                                     {
                                         Id = m.Id,
                                         Content = m.Content,
                                     }).ToListAsync();

            return numChildren;
        }
    }
}
