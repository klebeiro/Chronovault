using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using chronovault_api.Models;

public class UserCredentialConfiguration : IEntityTypeConfiguration<UserCredential>
{
    public void Configure(EntityTypeBuilder<UserCredential> builder)
    {
        builder.HasKey(uc => uc.UserId);

        builder.HasOne(uc => uc.User)
            .WithOne()
            .HasForeignKey<UserCredential>(uc => uc.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}