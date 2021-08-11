﻿using Microsoft.EntityFrameworkCore;
using TigerAdmin.Books;
using TigerAdmin.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;

namespace TigerAdmin.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See TigerAdminMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class TigerAdminDbContext : AbpDbContext<TigerAdminDbContext>
    {
        public DbSet<AppUser> Users { get; set; }

        //3.EF Core需要你将实体和 DbContext 建立关联.最简单的做法是在Acme.BookStore.EntityFrameworkCore项目的BookStoreDbContext类中添加DbSet属性.如下所示:
        public DbSet<Book> Books { get; set; }

        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside TigerAdminDbContextModelCreatingExtensions.ConfigureTigerAdmin
         */

        public TigerAdminDbContext(DbContextOptions<TigerAdminDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser
                
                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                /* Configure mappings for your additional properties
                 * Also see the TigerAdminEfCoreEntityExtensionMappings class
                 */
            });

            /* Configure your own tables/entities inside the ConfigureTigerAdmin method */

            builder.ConfigureTigerAdmin();
        }
    }
}
