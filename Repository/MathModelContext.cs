using Microsoft.EntityFrameworkCore;
using Repository;
using RPK.Model;
using RPK.Model.MathModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace RPK.Repository.MathModel
{
    public partial class MathModelContext : ExtendedDbContext
    {
        public MathModelContext()
        {
        }

        public MathModelContext(DbContextOptions<MathModelContext> options)
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
                optionsBuilder.UseSqlite("DataSource=MathModel.db").UseLazyLoadingProxies();
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

        public override string ToString()
        {
            return "Математическая модель";
        }

        private Dictionary<IBindingList, List<long>> ParametersOccupiedIds { get; set; } = new();

        private List<string> ParametersIdNames { get; set; }

        protected override void LoadDbSetsWithDbData()
        {
            Canals.Load();
            CanalGeometryParameters.Load();
            EmpiricalCoefficientOfMathModels.Load();
            Materials.Load();
            MeasureUnits.Load();
            Parameters.Load();
            ParameterOfMaterialProperties.Load();
            VariableParameters.Load();
        }

        protected override IList<IBindingList> GetDbSetsInternal()
        {
            return new List<IBindingList>
            {
                Canals.Local.ToBindingList(),
                CanalGeometryParameters.Local.ToBindingList(),
                EmpiricalCoefficientOfMathModels.Local.ToBindingList(),
                Materials.Local.ToBindingList(),
                MeasureUnits.Local.ToBindingList(),
                Parameters.Local.ToBindingList(),
                ParameterOfMaterialProperties.Local.ToBindingList(),
                VariableParameters.Local.ToBindingList()
            };
        }

        public override async IAsyncEnumerable<ValidationResult> ValidateAsync(object value, IBindingList dataSource, string columnName,
            int rowIndex)
        {
            if (ParametersIdNames is null || !ParametersIdNames.Any())
                SetParametersIdsNames();

            if (ParametersOccupiedIds is null || !ParametersOccupiedIds.Any())
                SetOccupiedIds();

            if (await RowCanBeChangedAsync(dataSource, rowIndex) is false)
            {
                yield return await ValueTask.FromResult(new RowIndexIsInvalid("Данную строку нельзя модифицировать, " +
                    "так как она является частью базовой математической модели программы."));
                yield break;
            }

            if (value is long longValue && ParametersIdNames.Contains(columnName))
            {
                if (longValue is 0)
                {
                    yield return await ValueTask.FromResult(new ValidationResult("Id параметра не может быть равен нулю."));
                    yield break;
                }
                if (ParametersOccupiedIds[dataSource].Contains(longValue))
                {
                    yield return await ValueTask.FromResult(new ValidationResult("Этот параметр уже является " +
                        "параметром другого типа."));
                }
            }

            SetOccupiedIds();
        }

        private void SetOccupiedIds()
        {
            ParametersOccupiedIds = new Dictionary<IBindingList, List<long>>();

            IEnumerable<IBindingList> dbSets = GetDbSetsInternal();

            foreach (IBindingList dbSet in dbSets)
            {
                Type dataType = dbSet.GetDataType();

                if (!dataType.IsSubclassOf(typeof(ParameterTypeBase)))
                    continue;

                ParametersOccupiedIds[dbSet] = new List<long>();

                foreach (IBindingList otherDbSet in dbSets)
                {
                    Type otherDbSetDataType = otherDbSet.GetDataType();

                    if (!otherDbSetDataType.IsSubclassOf(typeof(ParameterTypeBase)) 
                        || otherDbSetDataType.IsAssignableTo(dataType))
                        continue;

                    foreach (ParameterTypeBase parameterType in otherDbSet)
                    {
                        ParametersOccupiedIds[dbSet].Add(parameterType.ParameterId);
                    }
                }
            }
        }

        private void SetParametersIdsNames()
        {
            this.Canals.GetType();
            ParametersIdNames = new List<string> { nameof(ParameterTypeBase.ParameterId) };
        }

        private string GetModelName<T>() where T : BaseModel
        {
            DisplayAttribute displayAttribute = typeof(T).GetCustomAttributes(true)
                .OfType<DisplayAttribute>()
                .FirstOrDefault();

            return displayAttribute?.Name ?? typeof(T).Name;
        }
    }
}
