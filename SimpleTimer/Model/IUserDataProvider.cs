using System;

namespace SimpleTimer.Models
{
  public interface IUserDataProvider
  {
    DateTime GetLastUserLoginTimeStamp();
    string GetUserAlias();
    string GetUserEmail();
    void SetLastUserLoginTimeStamp(DateTime lastLoginTimeStamp);
    void SetUserAlias(string alias);
    void SetUserEmail(string email);
  }
}