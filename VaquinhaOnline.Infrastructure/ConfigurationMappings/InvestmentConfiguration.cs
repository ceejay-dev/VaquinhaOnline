namespace VaquinhaOnline.Infrastructure.ConfigurationMappings;

public class InvestmentConfiguration : IEntityTypeConfiguration<Investment>
{
    public void Configure(EntityTypeBuilder<Investment> builder)
    {
        builder.ToTable("Investment");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnName("Id");

        builder.Property(x => x.Value)
            .IsRequired(false)
            .HasColumnName("Value")
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.InvestmentDate)
            .IsRequired()
            .HasColumnName("PublicationDate")
            .HasColumnType("DATE");

        builder.Property(x => x.InvestmentType)
            .IsRequired()
            .HasColumnName("InvestmentType")
            .HasColumnType("text");

        builder.Property(x => x.Description)
            .IsRequired()
            .HasColumnName("Description")
            .HasColumnType("text");
    }
}

