using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WeMail.Common.Converts
{
    public class SexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                switch (intValue)
                {
                    case 0:
                        return "男";
                    case 1:
                        return "女";
                }
                return null;
            }
            else if (value is string str)
            {
                bool isParse = int.TryParse(str, out int result);
                if (isParse)
                {
                    switch (result)
                    {
                        case 0:
                            return "男";
                        case 1:
                            return "女";
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            if (value is string str)
            {
                if (str.Equals("男"))
                    return 0;
                else if (str.Equals("女"))
                    return 1;
                else
                    return null;
            }
            return null;
        }
    }
}
