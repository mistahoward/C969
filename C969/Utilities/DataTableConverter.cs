using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace C969.Utilities
{
    public class DataTableConverter
    {
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
    }
}
