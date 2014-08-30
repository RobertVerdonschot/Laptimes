namespace MarkupExtentions
{
  using System;
  using System.Diagnostics.CodeAnalysis;
  using System.Linq;
  using System.Reflection;
  using System.Windows;
  using System.Windows.Data;
  using System.Windows.Input;
  using System.Windows.Markup;

  public class BindToExtension : MarkupExtension
  {
    private Binding binding;
    private readonly string path;
    private string methodName;

    public BindToExtension()
    {
    }

    public BindToExtension(string path)
    {
      this.path = path;
    }

    public void ProcessPath(IServiceProvider serviceProvider)
    {
      if (string.IsNullOrWhiteSpace(this.path))
      {
        binding = new Binding();
        return;
      }

      var parts = this.path.Split('.').Select(o => o.Trim()).ToArray();

      RelativeSource relativeSource = null;
      string elementName = null;
      var partIndex = 0;

      if (parts[0].StartsWith("#"))
      {
        elementName = parts[0].Substring(1);
        partIndex++;
      }
      else if (parts[0].ToLower() == "ancestors" || parts[0].ToLower() == "ancestor")
      {
        if (parts.Length < 2)
        {
          throw new Exception("Invalid path, expected exactly 2 identifiers ancestors.#Type#.[Path] (e.g. Ancestors.DataGrid, Ancestors.DataGrid.SelectedItem, Ancestors.DataGrid.SelectedItem.Text)");
        }

        var sType = parts[1];
        var type = (Type)new TypeExtension(sType).ProvideValue(serviceProvider);
        relativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, type, 1);
        partIndex += 2;
      }
      else if (parts[0].ToLower() == "template" || parts[0].ToLower() == "templateparent" || parts[0].ToLower() == "templatedparent" || parts[0].ToLower() == "templated")
      {
        relativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent);
        partIndex++;
      }
      else if (parts[0].ToLower() == "thiswindow")
      {
        relativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(Window), 1);
        partIndex++;
      }
      else if (parts[0].ToLower() == "this")
      {
        relativeSource = new RelativeSource(RelativeSourceMode.Self);
        partIndex++;
      }

      var partsForPathString = parts.Skip(partIndex);
      IValueConverter valueConverter = null;

      if (partsForPathString.Any())
      {
        var lastPartForPathString = partsForPathString.Last();

        if (lastPartForPathString.EndsWith("()"))
        {
          partsForPathString = partsForPathString.Take(partsForPathString.Count() - 1);
          this.methodName = lastPartForPathString.Remove(lastPartForPathString.Length - 2);
          valueConverter = new CallMethodValueConverter(this.methodName);
        }
      }

      var s = string.Join(".", partsForPathString.ToArray());
      binding = string.IsNullOrWhiteSpace(s) ? new Binding() : new Binding(s);

      if (elementName != null)
      {
        binding.ElementName = elementName;
      }

      if (relativeSource != null)
      {
        binding.RelativeSource = relativeSource;
      }

      if (valueConverter != null)
      {
        binding.Converter = valueConverter;
      }
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      if (!(serviceProvider is IXamlTypeResolver))
      {
        return null;
        // NOTE, this is to prevent the design time editor from showing an error related to the user of TypeExtension in ProcessPath
      }

      ProcessPath(serviceProvider);
      return binding.ProvideValue(serviceProvider);
    }

    private class CallMethodValueConverter : IValueConverter
    {
      private string methodName;

      public CallMethodValueConverter(string methodName)
      {
        this.methodName = methodName;
      }

      public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
        return value == null ? null : new CallMethodCommand(value, this.methodName);
      }

      public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
        throw new NotImplementedException();
      }
    }

    private class CallMethodCommand : ICommand
    {
      private readonly object moObject;

      private readonly MethodInfo methodInfo;
      private readonly bool methodAcceptsParameter;
      private readonly MethodInfo canMethodInfo;

      public CallMethodCommand(object poObject, string methodName)
      {
        this.moObject = poObject;
        methodInfo = this.moObject.GetType().GetMethod(methodName);

        var parameters = this.methodInfo.GetParameters();
        if (parameters.Length > 2)
        {
          throw new Exception("You can only bind to a methods take take 0 or 1 parameters.");
        }

        canMethodInfo = this.moObject.GetType().GetMethod("Can" + methodName);
        if (canMethodInfo != null)
        {
          if (this.canMethodInfo.ReturnType != typeof(bool))
          {
            throw new Exception("'Can' method must return boolean.");
          }

          var canParameters = methodInfo.GetParameters();
          if (canParameters.Length > 2)
          {
            throw new Exception("You can only bind to a methods take take 0 or 1 parameters.");
          }
        }

        this.methodAcceptsParameter = parameters.Any();
      }

      public bool CanExecute(object parameter)
      {
        if (canMethodInfo == null)
        {
          return true;
        }

        var parameters = !this.methodAcceptsParameter ? null : new[] { parameter };
        return (bool)this.canMethodInfo.Invoke(this.moObject, parameters);
      }

#pragma warning disable 67 // CanExecuteChanged is not being used but is required by ICommand
      public event EventHandler CanExecuteChanged;
#pragma warning restore 67 // CanExecuteChanged is not being used but is required by ICommand

      public void Execute(object parameter)
      {
        var parameters = !this.methodAcceptsParameter ? null : new[] { parameter };
        this.methodInfo.Invoke(this.moObject, parameters);
      }
    }
  }
}
