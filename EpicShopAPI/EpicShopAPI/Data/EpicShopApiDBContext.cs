using EpicShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EpicShopAPI.Data
{
    public class EpicShopApiDBContext : DbContext
    {
        public EpicShopApiDBContext(DbContextOptions<EpicShopApiDBContext> options):base(options)
        {

        }

        public DbSet<CartModel> CartSet { get; set; }

        public DbSet<CategoryModel> CategorySet { get; set; }

        public DbSet<OrderModel> OrderSet { get; set; }

        public DbSet<PreviousOrdersModel> PreviousOrdersSet { get; set; }

        public DbSet<ProductModel> ProductSet { get; set; }

        public DbSet<RoleModel> RoleSet { get; set; }

        public DbSet<UserModel> UserSet { get; set; }

        public DbSet<WalletModel> WalletSet { get; set; }
    }
}
