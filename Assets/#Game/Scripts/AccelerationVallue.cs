using UnityEngine;

public static class AccelerationVallue
{
    public static float AccelerationX { get; private set; } = 0f;


//    public static void Init()
//    {
//#if  UNITY_ANDROID && !UNITY_EDITOR
        
//        Input.gyro.enabled = true;

//        // サポートするかの確認
//        Debug.Assert(SystemInfo.supportsGyroscope, "Don't support gyro!");
//#endif  //  UNITY_ANDROID && !UNITY_EDITOR
//    }

    public static void ManualUpdate ()
    {
        AccelerationX = Input.acceleration.x;
    }
}
