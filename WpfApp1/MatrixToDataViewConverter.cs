using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApp1
{
    public class MatrixToDataViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var array = value as int[,];
            if (array == null) return null;

            //var array = ILMath.rand(3, 5);

            var rows = array.GetLength(0);
            var columns = array.GetLength(1);

            var t = new DataTable();
            for (var c = 0; c < columns; c++)
            {
                t.Columns.Add(new DataColumn(c.ToString()));
            }

            for (var r = 0; r < rows; r++)
            {
                var newRow = t.NewRow();
                for (var c = 0; c < columns; c++)
                {
                    var v = (double)array[r, c];

                    // Round if parameter is set
                    if (parameter != null)
                    {
                        int digits;
                        if (int.TryParse(parameter.ToString(), out digits))
                            v = Math.Round(v, digits);
                    }

                    newRow[c] = v;
                }

                t.Rows.Add(newRow);
            }


            return t.DefaultView;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
