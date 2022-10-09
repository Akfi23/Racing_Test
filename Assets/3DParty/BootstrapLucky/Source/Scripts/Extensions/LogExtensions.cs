using UnityEngine;


public static class LogExtensions
{
    public static void Log(string message)
    {
#if DEBUG
        Debug.Log(message);
#endif
    }
}