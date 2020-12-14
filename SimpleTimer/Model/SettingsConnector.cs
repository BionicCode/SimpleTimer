using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using SimpleTimer.Models.Properties;

namespace SimpleTimer.Models
{
  public class SettingsConnector : ISettingsConnector
  {
    private ApplicationSettings Settings => Properties.ApplicationSettings.Default;
    public void WriteDateTime(string settingPropertyName, DateTime value)
    {
      if (!IsSettingPropertyOfValidType<DateTime>(settingPropertyName, out Exception validationFailedException))
      {
        throw validationFailedException;
      }

      this.Settings[settingPropertyName] = value;
      this.Settings.Save();
    }

    public bool TryReadDateTime(string settingPropertyName, out DateTime value)
    {
      if (!IsSettingPropertyOfValidType<DateTime>(settingPropertyName, out Exception validationFailedException))
      {
        throw validationFailedException;
      }

      object defaultValue = this.Settings[settingPropertyName] ?? default(DateTime);
      
      value = (DateTime) defaultValue;
      return value != default;
    }

    public void WriteString(string settingPropertyName, string value)
    {
      if (!IsSettingPropertyOfValidType<string>(settingPropertyName, out Exception validationFailedException))
      {
        throw validationFailedException;
      }

      this.Settings[settingPropertyName] = value;
      this.Settings.Save();
    }

    public bool TryReadString(string settingPropertyName, out string value)
    {
      if (!IsSettingPropertyOfValidType<string>(settingPropertyName, out Exception validationFailedException))
      {
        throw validationFailedException;
      }

      value = this.Settings[settingPropertyName].ToString();
      return value != null;
    }

    private bool IsSettingPropertyOfValidType<TProperty>(string settingPropertyName, out Exception validationFailedException)
    {
      validationFailedException = null;

      Type settingPropertyType = this.Settings.Properties[settingPropertyName].PropertyType;
      if (!settingPropertyType.Equals(typeof(TProperty)))
      {
        validationFailedException = new ArgumentException(
          $"The specified property name has the wrong data type: setting '{settingPropertyName}' is of type {settingPropertyType.FullName} and not '{typeof(TProperty).FullName}'.",
          nameof(settingPropertyName));
        return false;
      }

      return true;
    }
  }
}
