namespace LapTimes.Converters
{
  using System;
  using System.Linq;
  using System.Windows;
  using System.Windows.Data;

  public class OrOperatorConverter : IMultiValueConverter
  {
    public bool IsReversed { get; set; }

    public bool UseHidden { get; set; }

    public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      var matched = values.OfType<bool>().Aggregate(false, (current, value) => current || value);

      if (targetType == typeof(Visibility))
      {
        return matched
                   ? (IsReversed ? (UseHidden ? Visibility.Hidden : Visibility.Collapsed) : Visibility.Visible)
                   : (IsReversed ? Visibility.Visible : (UseHidden ? Visibility.Hidden : Visibility.Collapsed));
      }

      return IsReversed ? !matched : matched;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
