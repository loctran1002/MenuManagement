namespace ManageMenu.Entities
{
    public class Menu
    {
        public Guid Id { get; set; }
        public int Level { get; set; }
        public string Content { get; set; }
        public Guid? RootId { get; set; }

        public Menu? Root { get; set; }
    }
}