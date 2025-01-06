namespace VaquinhaOnline.Infrastructure.ConfigurationMappings;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Project");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnName("Id");

        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnName("Title")
            .HasColumnType("text")
            .HasMaxLength(255);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasColumnName("Description")
            .HasColumnType("text");

        builder.Property(x => x.Status)
            .IsRequired()
            .HasColumnName("Status")
            .HasColumnType("text")
            .HasMaxLength(50);

        builder.Property(x => x.GoalValue)
            .IsRequired()
            .HasColumnName("GoalValue")
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.CurrentValue)
            .IsRequired()
            .HasColumnName("CurrentValue")
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.PublicationDate)
            .IsRequired()
            .HasColumnName("PublicationDate")
            .HasColumnType("DATE");

        builder.Property(x => x.ClosingDate)
            .IsRequired()
            .HasColumnName("ClosingDate")
            .HasColumnType("DATE");

        builder.Property(x => x.Progress)
            .IsRequired()
            .HasColumnName("Progress")
            .HasColumnType("text");

        builder.HasOne(x => x.Investment)
            .WithOne(x => x.Project) 
            .HasForeignKey<Investment>(x => x.ProjectId);
    }
}   