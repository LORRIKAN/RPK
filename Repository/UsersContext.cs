using Microsoft.EntityFrameworkCore;
using Repository;
using RPK.Model.Users;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

#nullable disable

namespace RPK.Repository.Users
{
    public partial class UsersContext : ExtendedDbContext
    {
        public UsersContext()
        {
        }

        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("DataSource=Users.db").UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("Role");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Login);

                entity.ToTable("User");

                entity.HasIndex(e => e.RoleId, "IX_");

                entity.Property(e => e.Password).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public override string ToString()
        {
            return "Пользователи";
        }
        protected override IList<IBindingList> GetDbSetsInternal()
        {
            return new List<IBindingList> { Roles.Local.ToBindingList(), Users.Local.ToBindingList() };
        }

        protected override void LoadDbSetsWithDbData()
        {
            Roles.Load();
            Users.Load();
        }

        public override async IAsyncEnumerable<ValidationResult> ValidateAsync(object value, IBindingList dataSource, string columnName)
        {
            yield return await ValueTask.FromResult<ValidationResult>(null);
        }
    }
}
