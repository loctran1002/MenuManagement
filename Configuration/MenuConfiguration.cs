using ManageMenu.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageMenu.Configuration
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menu");
            builder.HasKey(menu => menu.Id);
            builder.Property(menu => menu.Level).HasColumnType("int").IsRequired();
            builder.Property(menu => menu.RootId).HasColumnType("char(36)").IsRequired(false);
            builder.Property(menu => menu.Content).HasColumnType("varchar(50)");

            builder.HasOne(m => m.Root)
                .WithMany()
                .HasForeignKey(m => m.RootId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
