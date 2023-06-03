using ManageMenu.Models;

namespace MenuManagement.Utils
{
    public class Helpers
    {
        public static bool AppendChild(ref List<MenuViewModel>? list, Guid rootId, List<MenuViewModel>? child)
        {
            if (list == null || child == null || list.Count == 0 || child.Count == 0)
                return false;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == rootId)
                {
                    list[i].Children = child;
                    return true;
                }

                if (list[i].Children != null)
                {
                    var children = list[i].Children;
                    var check = AppendChild(ref children, rootId, child);
                    if (check)
                    {
                        list[i].Children = children;
                        return check;
                    }
                }
            }

            return false;
        }

        public static bool AppendChild(ref List<MenuViewModel>? list, Guid rootId, MenuViewModel child)
        {
            if (list == null || child == null || list.Count == 0)
                return false;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == rootId)
                {
                    if (list[i].Children == null)
                        list[i].Children = new List<MenuViewModel>();
                    list[i].Children.Add(child);
                    return true;
                }

                if (list[i].Children != null)
                {
                    var children = list[i].Children;
                    var check = AppendChild(ref children, rootId, child);
                    if (check)
                    {
                        list[i].Children = children;
                        return check;
                    }
                }
            }

            return false;
        }

        public static bool RemoveItem(ref List<MenuViewModel>? list, Guid id)
        {
            if (list == null || list.Count == 0)
                return false;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == id)
                {
                    list.RemoveAt(i);
                    return true;
                }

                if (list[i].Children != null)
                {
                    var children = list[i].Children;
                    var check = RemoveItem(ref children, id);
                    if (check)
                    {
                        list[i].Children = children;
                        return check;
                    }
                }
            }

            return false;
        }

        public static bool ShrinkList(ref List<MenuViewModel>? list, Guid id)
        {
            if (list == null || list.Count == 0)
                return false;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == id)
                {
                    list[i].Children = null;
                    return true;
                }

                if (list[i].Children != null)
                {
                    var children = list[i].Children;
                    var check = ShrinkList(ref children, id);
                    if (check)
                    {
                        list[i].Children = children;
                        return check;
                    }
                }
            }

            return false;
        }

        public static bool EditContent(ref List<MenuViewModel>? list, MenuViewModel item)
        {
            if (list == null || list.Count == 0)
                return false;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == item.Id)
                {
                    list[i].Content = item.Content;
                    return true;
                }

                if (list[i].Children != null)
                {
                    var children = list[i].Children;
                    var check = EditContent(ref children, item);
                    if (check)
                    {
                        list[i].Children = children;
                        return check;
                    }
                }
            }

            return false;
        }
    }
}
