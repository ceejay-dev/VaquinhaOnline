namespace VaquinhaOnline.Infrastructure.ConfigurationMappings.IdentityMappings;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("IdentityUser");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnName("Id");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("text")
            .HasMaxLength(50);

        builder.Property(x => x.ProfilePhoto)
            .IsRequired(false)
            .HasColumnName("ProfilePhoto")
            .HasColumnType("text");

        builder.Property(x => x.CreationDate)
            .IsRequired()
            .HasColumnName("CreationDate")
            .HasColumnType("DATE");

        builder.Property(x => x.RefreshToken)
            .IsRequired(false)
            .HasColumnName("RefreshToken")
            .HasColumnType("text")
            .HasMaxLength(500);

        builder.Property(x => x.RefreshTokenExpiryTime)
            .HasColumnName("RefreshTokenExpiryTime");

        builder.HasMany(x => x.UserRoles)
           .WithOne(x => x.User)
           .HasForeignKey(x => x.UserId)
           .IsRequired();

        builder.HasMany(x => x.Projects)
            .WithOne()
            .HasForeignKey(x => x.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Investments)
            .WithOne()
            .HasForeignKey(x => x.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
