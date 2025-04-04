using GameConsoleFiXiK;
using System;
using UnityEngine;

namespace GC
{
    public static class GameConsole
    {
        public static event Action<string, TypeMessage> ActionMessage;

        public static bool IsUnityLogs;

        public static void Log(string message)
        {
            ActionMessage?.Invoke(message, TypeMessage.TypeLog);

            if (IsUnityLogs) 
                Debug.Log(message);
        }

        public static void LogWarning(string message)
        {
            ActionMessage?.Invoke(message, TypeMessage.TypeWarning);

            if (IsUnityLogs) 
                Debug.LogWarning(message);
        }

        public static void LogError(string message)
        {
            ActionMessage?.Invoke(message, TypeMessage.TypeError);

            if (IsUnityLogs) 
                Debug.LogError(message);
        }

        public static void LogCorrect(string message)
        {
            ActionMessage?.Invoke(message, TypeMessage.TypeCorrect);

            if (IsUnityLogs) 
                Debug.Log(message);
        }

        public static void LogExclusive(string message)
        {
            ActionMessage?.Invoke(message, TypeMessage.TypeExclusive);

            if (IsUnityLogs) 
                Debug.Log(message);
        }
    }
}