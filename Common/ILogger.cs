using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface ILogger
    {
        void Log(string format, params object[] args);
        void LogError(string format, params object[] args);
        void LogWarning(string format, params object[] args);
        void LogException(string description, Exception e);
    }
}
