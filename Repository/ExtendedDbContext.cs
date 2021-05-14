using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class ExtendedDbContext : DbContext
    {
        private bool dbSetsObtained { get; set; } = false;

        private protected IEnumerable<IBindingList> dbSets;

        public IEnumerable<IBindingList> GetDbSets(bool loadDataFromBd)
        {
            if (loadDataFromBd)
                LoadDbSetsWithDbData();

            if (!dbSetsObtained)
            {
                dbSetsObtained = true;
                dbSets = GetDbSetsInternal();
            }

            return dbSets;
        }

        protected abstract void LoadDbSetsWithDbData();

        protected abstract IEnumerable<IBindingList> GetDbSetsInternal();

        public abstract IAsyncEnumerable<ValidationResult> ValidateAsync(object value, IBindingList dataSource, string columnName);

        public ExtendedDbContext()
        {

        }

        public ExtendedDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}