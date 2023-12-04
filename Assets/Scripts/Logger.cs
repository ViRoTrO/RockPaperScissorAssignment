using UnityEngine;

public class Logger
{

    public static void Log(string className, string functionName, string error)
    {
        Debug.Log($"{className} : {functionName} Error => {error}");
    }

}
