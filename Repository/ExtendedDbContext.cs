using Microsoft.EntityFrameworkCore;
using RPK.Model;
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
        private bool DbSetsObtained { get; set; } = false;

        private protected IEnumerable<IBindingList> dbSets;

        public IEnumerable<IBindingList> GetDbSets(bool loadDataFromBd)
        {
            if (loadDataFromBd)
                LoadDbSetsWithDbData();

            if (!DbSetsObtained)
            {
                DbSetsObtained = true;
                dbSets = GetDbSetsInternal();
            }

            return dbSets;
        }

        public virtual async Task<bool> RowCanBeChangedAsync(IBindingList dataSource, int rowIndex)
        {
            Type dataSourceDataType = dataSource.GetDataType();
            if (dataSourceDataType.IsSubclassOf(typeof(BaseModel)) is false)
                return await Task.FromResult(true);

            Range unchangeableRows = (Range)dataSourceDataType.GetProperty(nameof(BaseModel.UnchangeableRows)).GetValue(dataSource[rowIndex]);

            if (rowIndex >= unchangeableRows.Start.Value && rowIndex <= unchangeableRows.End.Value)
                return await Task.FromResult(false);
            else
                return await Task.FromResult(true);
        }

        protected abstract void LoadDbSetsWithDbData();

        protected abstract IEnumerable<IBindingList> GetDbSetsInternal();

        public abstract IAsyncEnumerable<ValidationResult> ValidateAsync(object value, IBindingList dataSource, string columnName, int rowIndex);

        public ExtendedDbContext()
        {

        }

        public ExtendedDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}