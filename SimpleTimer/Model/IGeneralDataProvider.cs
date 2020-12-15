using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTimer.Models
{
    public interface IGeneralDataProvider
    {
        string GetTimeLimit();
        void SetTimeLimit(string timeLimit);
    }
}
