namespace LapTimes.Converters
{
  using System;
  using System.Globalization;
  using System.Windows;
  using System.Windows.Data;

  using MarkupExtentions;

  public class EnumVisibilityConverter : IValueConverter
  {
    public bool IsReversed { get; set; }

    public bool UseHidden { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      // Check for EnumerationMember
      if (value is EnumerationExtension.EnumerationMember)
      {
        value = (value as EnumerationExtension.EnumerationMember).Value;
      }

      var matched = Match(value, parameter);

      if (targetType == typeof(Visibility))
      {
        return matched
                   ? (IsReversed ? (UseHidden ? Visibility.Hidden : Visibility.Collapsed) : Visibility.Visible)
                   : (IsReversed ? Visibility.Visible : (UseHidden ? Visibility.Hidden : Visibility.Collapsed));
      }

      return IsReversed ? !matched : matched;
    }

    private bool Match(object value, object parameter)
    {
      if (parameter == null || !(value is Enum))
      {
        return false;
      }

      var values = parameter.ToString().Split(new[] { ',', ';' });
      foreach (var enumValue in values)
      {
        var compareValue = Enum.Parse(value.GetType(), enumValue);
        if (value.Equals(compareValue))
        {
          return true;
        }
      }

      return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
