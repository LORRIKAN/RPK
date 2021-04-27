using Microsoft.EntityFrameworkCore;
using RPK.Model;

#nullable disable

namespace RPK.Researcher.Repository
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Canal> Canals { get; set; }
        public virtual DbSet<CanalGeometryParameter> CanalGeometryParameters { get; set; }
        public virtual DbSet<EmpiricalCoefficientOfMathModel> EmpiricalCoefficientOfMathModels { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<MeasureUnit> MeasureUnits { get; set; }
        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<ParameterOfMaterialProperty> ParameterOfMaterialProperties { get; set; }
        public virtual DbSet<VariableParameter> VariableParameters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("DataSource=Repository\\Database.db").UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Canal>(entity =>
            {
                entity.ToTable("Canal");

                entity.Property(e => e.Brand).IsRequired();
            });

            modelBuilder.Entity<CanalGeometryParameter>(entity =>
            {
                entity.HasKey(e => new { e.CanalId, e.ParameterId });

                entity.HasOne(d => d.Canal)
                    .WithMany(p => p.CanalGeometryParameters)
                    .HasForeignKey(d => d.CanalId);

                entity.HasOne(d => d.Parameter)
                    .WithMany(p => p.CanalGeometryParameters)
                    .HasForeignKey(d => d.ParameterId);
            });

            modelBuilder.Entity<EmpiricalCoefficientOfMathModel>(entity =>
            {
                entity.HasKey(e => new { e.ParameterId, e.MaterialId });

                entity.ToTable("EmpiricalCoefficientOfMathModel");

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.EmpiricalCoefficientOfMathModels)
                    .HasForeignKey(d => d.MaterialId);

                entity.HasOne(d => d.Parameter)
                    .WithMany(p => p.EmpiricalCoefficientOfMathModels)
                    .HasForeignKey(d => d.ParameterId);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Material");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<MeasureUnit>(entity =>
            {
                entity.HasKey(e => e.Value);

                entity.ToTable("MeasureUnit");
            });

            modelBuilder.Entity<Parameter>(entity =>
            {
                entity.ToTable("Parameter");

                entity.HasIndex(e => e.MeasureUnit, "IX_Relationship2");

                entity.Property(e => e.Designation).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.MeasureUnitNavigation)
                    .WithMany(p => p.Parameters)
                    .HasForeignKey(d => d.MeasureUnit)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ParameterOfMaterialProperty>(entity =>
            {
                entity.HasKey(e => new { e.ParameterId, e.MaterialId });

                entity.ToTable("ParameterOfMaterialProperty");

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.ParameterOfMaterialProperties)
                    .HasForeignKey(d => d.MaterialId);

                entity.HasOne(d => d.Parameter)
                    .WithMany(p => p.ParameterOfMaterialProperties)
                    .HasForeignKey(d => d.ParameterId);
            });

            modelBuilder.Entity<VariableParameter>(entity =>
            {
                entity.HasKey(e => new { e.MaterialId, e.CanalId, e.ParameterId });

                entity.ToTable("VariableParameter");

                entity.HasOne(d => d.Canal)
                    .WithMany(p => p.VariableParameters)
                    .HasForeignKey(d => d.CanalId);

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.VariableParameters)
                    .HasForeignKey(d => d.MaterialId);

                entity.HasOne(d => d.Parameter)
                    .WithMany(p => p.VariableParameters)
                    .HasForeignKey(d => d.ParameterId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
