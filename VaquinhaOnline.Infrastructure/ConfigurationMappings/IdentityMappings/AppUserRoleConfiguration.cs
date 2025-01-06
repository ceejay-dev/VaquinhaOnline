namespace VaquinhaOnline.Infrastructure.ConfigurationMappings.IdentityMappings;

public class AppUserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
{
    public void Configure(EntityTypeBuilder<AppUserRole> builder)
    {
        builder.ToTable("IdentityUserRole");

        // Chave composta
        builder.HasKey(e => new { e.UserId, e.RoleId });

        // Configuração explícita das propriedades de navegação e chaves estrangeiras
        builder.HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); ;

        builder.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); ;
    }
}
