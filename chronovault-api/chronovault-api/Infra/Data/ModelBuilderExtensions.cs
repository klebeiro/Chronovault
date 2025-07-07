using Microsoft.EntityFrameworkCore;

namespace chronovault_api.Infra.Data
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyConfigurationsFromAssembly(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new UserCredentialConfiguration());
        }
    }
}
