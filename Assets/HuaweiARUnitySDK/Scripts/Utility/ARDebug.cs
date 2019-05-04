//#define __DEBUG__
namespace HuaweiARInternal
{

    using UnityEngine;

    public class ARDebug
    {

        public static void LogInfo(Object message)
        {
#if __DEBUG__
            Debug.Log(message);
#endif
        }
        public static void LogInfo(string format, params object[] args)
        {
#if __DEBUG__
            Debug.LogFormat(format,args);
#endif
        }

        public static void LogWarning(Object message)
        {
            Debug.LogWarning(message);
        }
        public static void LogWarning(string format, params object[] args)
        {
            Debug.LogWarningFormat(format, args);
        }

        public static void LogError(Object message)
        {
            Debug.LogError(message);
        }
        public static void LogError(string format, params object[] args)
        {
            Debug.LogErrorFormat(format, args);
        }

    }
}
