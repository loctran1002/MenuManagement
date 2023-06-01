namespace ManageMenu.Models
{
    public class MenuViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public List<MenuViewModel> Children { get; set; } = new List<MenuViewModel>();
    }
}
