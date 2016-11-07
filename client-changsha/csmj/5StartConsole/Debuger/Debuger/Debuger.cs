using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEngine
{
    public static class Debuger
    {
        /// <summary>
        /// DEBUG日志开关 0关闭 1控制台 2unity3d
        /// </summary>
        static public int EnableLog = 0;
        /// <summary>
        /// 红
        /// </summary>
        /// <param name="message"></param>
        static public void LogRed(object message)
        {
            Log("<color=red>" + message + "</color>", null);
        }
        /// <summary>
        /// 绿
        /// </summary>
        /// <param name="message"></param>
        static public void LogGreen(object message)
        {
            Log("<color=green>" + message + "</color>", null);
        }
        /// <summary>
        /// 蓝
        /// </summary>
        /// <param name="message"></param>
        static public void LogBlue(object message)
        {
            Log("<color=blue>" + message + "</color>", null);
        }
        /// <summary>
        /// 黄
        /// </summary>
        /// <param name="message"></param>
        static public void LogYellow(object message)
        {
            Log("<color=yellow>" + message + "</color>", null);
        }
        static public void Log(object message)
        {
            Log(message, null);
        }
        static public void Log(object message, Object context)
        {
            if (EnableLog != 0)
            {
                if (EnableLog == 1)
                    System.Console.WriteLine(message);
                if (EnableLog == 2)
                    Debug.Log(message, context);
            }
        }
        static public void LogError(object message)
        {
            LogError(message, null);
        }
        static public void LogError(object message, Object context)
        {
            if (EnableLog != 0)
            {
                if (EnableLog == 1)
                    System.Console.WriteLine(message);
                if (EnableLog == 2)
                    Debug.LogError(message, context);
            }
        }
        static public void LogWarning(object message)
        {
            LogWarning(message, null);
        }
        static public void LogWarning(object message, Object context)
        {
            if (EnableLog != 0)
            {
                if (EnableLog == 1)
                    System.Console.WriteLine(message);
                if (EnableLog == 2)
                    Debug.LogWarning(message, context);
            }
        }
    }
}
