using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTimer.Models
{
    public class GeneralDataProvider : IGeneralDataProvider
    {
        private ISettingsConnector SettingsConnector { get; set; }

        public GeneralDataProvider()
        {
            this.SettingsConnector = new SettingsConnector();
        }

        public string GetTimeLimit() =>
            (this.SettingsConnector.TryReadString(Properties.StringResources.TimeLimit, out string timeLimit))
            ? timeLimit
            : string.Empty;

        public void SetTimeLimit(string timeLimit) =>
            this.SettingsConnector.WriteString(Properties.StringResources.TimeLimit, timeLimit);
    }
}
