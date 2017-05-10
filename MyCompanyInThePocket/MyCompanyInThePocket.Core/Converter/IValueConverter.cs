using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Converter
{
    public interface IValueConverter<TFrom,TTo>
    {
        TTo Convert(TFrom value, Type targetType, object parameter, System.Globalization.CultureInfo culture);
    }
}
