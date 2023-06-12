using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace C969.Utilities
{
    /// <summary>
    /// Utility class for converting DataTables to working items
    /// </summary>
    public class DataTableConverter
    {
        /// <summary>
        /// Converts a given DataTable to a list of objects of type T
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="dt">DataTable to convert</param>
        /// <returns>List of objects of type T converted from the DataTable</returns>
        public static List<T> ConvertDataTableToList<T>(DataTable dt) where T : class, new()
        {
            List<T> modelList = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                T model = new T();

                foreach (DataColumn column in dt.Columns)
                {
                    PropertyInfo property = model.GetType().GetProperty(column.ColumnName);

                    if (property != null && row[column] != DBNull.Value)
                    {
                        Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                        property.SetValue(model, Convert.ChangeType(row[column], convertTo));
                    }
                }

                modelList.Add(model);
            }

            return modelList;
        }

        /// <summary>
        /// Converts a given DataRow to an object of type T
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="row">DataRow to convert</param>
        /// <returns>Object of type T converted from the DataRow</returns>
        public static T ConvertDataRowToModel<T>(DataRow row) where T : class, new()
        {
            T model = new T();

            foreach (DataColumn column in row.Table.Columns)
            {
                PropertyInfo property = model.GetType().GetProperty(column.ColumnName);

                if (property != null && row[column] != DBNull.Value)
                {
                    Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                    property.SetValue(model, Convert.ChangeType(row[column], convertTo));
                }
            }

            return model;
        }
    }
}
