using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTimer.Models
{
    public interface IGeneralSettingsConnector
    {
        bool TryReadDateTime(string settingPropertyName, out DateTime value);
        bool TryReadString(string settingPropertyName, out string value);
        void WriteDateTime(string settingPropertyName, DateTime value);
        void WriteString(string settingPropertyName, string value);
    }
}
