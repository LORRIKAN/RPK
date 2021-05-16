using Microsoft.EntityFrameworkCore;
using Repository;
using RPK.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RPK.Administrator.Presenter
{

    public static class DataGridViewExtensions
    {
        public static DataGridView Bind(this DataGridView grid, IBindingList data, ExtendedDbContext dbContext,
            bool autoGenerateColumns = true)
        {
            grid.DataSource = null;
            if (autoGenerateColumns)
            {
                var dataType = data.GetDataType();

                var properties = TypeDescriptor.GetProperties(dataType);

                var dependentItems = dbContext.Model.FindEntityType(dataType).GetNavigations()
                    .Where(n => n.IsOnDependent)
                    .Select(n =>
                    {
                        Type toDataType = n.ClrType;

                        IBindingList bindingList = dbContext
                        .GetDbSets(false)
                        .FirstOrDefault(ds => ds.GetDataType() == n.ClrType);

                        var dataSource = Array.CreateInstance(n.ClrType, bindingList.Count);

                        bindingList.CopyTo(dataSource, 0);

                        return new
                        {
                            FromPropName = n.ForeignKey.Properties[0].Name,
                            ToPropName = n.ForeignKey.PrincipalKey.Properties[0].Name,
                            DataSource = dataSource
                        };
                    });

                var metedata = properties.Cast<PropertyDescriptor>()
                    .Where(p => p.IsBrowsable)
                    .Select(p =>
                    {
                        var dependment = dependentItems?.FirstOrDefault(di => di.FromPropName == p.Name);

                        return new
                        {
                            Name = p.Name,
                            HeaderText = p.Attributes.OfType<DisplayAttribute>()
                            .FirstOrDefault()?.Name ?? p.DisplayName,
                            ReadOnly = p.IsReadOnly,
                            Type = p.PropertyType,
                            Dependency = dependment
                        };
                    });

                var columns = metedata.Select(m =>
                {
                    DataGridViewColumn c;

                    c = (m.Dependency) switch
                    {
                        not null => new DataGridViewComboBoxColumn
                        {
                            DisplayMember = m.Dependency.ToPropName,
                            ValueMember = m.Dependency.ToPropName,
                            DataSource = m.Dependency.DataSource,
                        },
                        _ => new DataGridViewTextBoxColumn()
                    };

                    c.DataPropertyName = m.Name;
                    c.Name = m.Name;
                    c.HeaderText = m.HeaderText;
                    c.ReadOnly = m.ReadOnly;
                    if (m.ReadOnly)
                        c.DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.DarkGray };
                    c.ValueType = m.Type;
                    c.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                    return c;
                });

                grid.Columns.Clear();
                grid.Columns.AddRange(columns.ToArray());
            }

            grid.DataSource = data;

            return grid;
        }
    }
}