namespace LapTimes.Converters
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Windows.Data;

  public class NumericConverter : IValueConverter
  {
    public object Convert(object value,
                          Type targetType,
                          object parameter,
                          CultureInfo culture)
    {
      if (null == value)
      {
        return null;
      }

      return targetType.UnderlyingSystemType == typeof(string) ? value.ToString() : value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (null == value)
      {
        return null;
      }

      var numericTypes = new HashSet<Type>
                {
                    typeof(Byte),
                    typeof(Decimal),
                    typeof(Double),
                    typeof(Int16),
                    typeof(Int32),
                    typeof(Int64),
                    typeof(SByte),
                    typeof(Single),
                    typeof(UInt16),
                    typeof(UInt32),
                    typeof(UInt64) };

      if (!numericTypes.Contains(targetType.UnderlyingSystemType))
      {
        return value;
      }

      var intTypes = new HashSet<Type>
                {
                    typeof (Byte),
                    typeof (Int16),
                    typeof (Int32),
                    typeof (Int64),
                    typeof (SByte),
                    typeof (UInt16),
                    typeof (UInt32),
                    typeof (UInt64)
                };
      var text = value.ToString();
      if (intTypes.Contains(targetType.UnderlyingSystemType))
      {
        try
        {
          return int.Parse(text);
        }
        catch
        {
          return null;
        }
      }

      try
      {
        return double.Parse(text);
      }
      catch
      {
        return null;
      }
    }
  }
}
