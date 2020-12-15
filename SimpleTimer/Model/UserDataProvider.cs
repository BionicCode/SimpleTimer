using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTimer.Models
{
  public class UserDataProvider : IUserDataProvider
  {
    private ISettingsConnector SettingsConnector { get; set; }

    public UserDataProvider()
    {
      this.SettingsConnector = new SettingsConnector();
    }

    public string GetUserAlias() =>
      (this.SettingsConnector.TryReadString(Properties.StringResources.UserAlias, out string alias))
        ? alias
        : string.Empty;

    public void SetUserAlias(string alias) =>
      this.SettingsConnector.WriteString(Properties.StringResources.UserAlias, alias);

    public string GetUserEmail() =>
      (this.SettingsConnector.TryReadString(Properties.StringResources.UserEmail, out string email))
        ? email
        : string.Empty;

    public void SetUserEmail(string email) =>
      this.SettingsConnector.WriteString(Properties.StringResources.UserEmail, email);

    public DateTime GetLastUserLoginTimeStamp() =>
      (this.SettingsConnector.TryReadDateTime(Properties.StringResources.LastLoginTimeStamp, out DateTime email))
        ? email
        : DateTime.MinValue;

    public void SetLastUserLoginTimeStamp(DateTime lastLoginTimeStamp) =>
      this.SettingsConnector.WriteDateTime(Properties.StringResources.LastLoginTimeStamp, lastLoginTimeStamp);
  }
}
