namespace LapTimes.Converters
{
  using System;
  using System.Windows.Data;
  using System.Globalization;
  using System.Windows;

  public sealed class BooleanToVisibilityConverter : IValueConverter
  {
    public bool IsReversed { get; set; }

    public bool UseHidden { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var val = System.Convert.ToBoolean(value, CultureInfo.InvariantCulture);
      if (IsReversed)
      {
        val = !val;
      }

      if (val)
      {
        return Visibility.Visible;
      }

      return this.UseHidden ? Visibility.Hidden : Visibility.Collapsed;
    }


    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
