using UnityEngine;

/// <summary>
/// ソフトウェアキーボードの表示領域を管理するクラス
/// </summary>
public static class SoftwareKeyboaryArea
{
    /// <summary>
    /// 高さを返します
    /// </summary>
    public static int Height
    {
        get
        {
#if !UNITY_EDITOR && UNITY_ANDROID

                using ( var unityPlayer = new AndroidJavaClass( "com.unity3d.player.UnityPlayer" ) )
                {
                    var view = unityPlayer
                        .GetStatic<AndroidJavaObject>( "currentActivity" )
                        .Get<AndroidJavaObject>( "mUnityPlayer" )
                        .Call<AndroidJavaObject>( "getView" )
                    ;

                    using ( var rect = new AndroidJavaObject( "android.graphics.Rect" ) )
                    {
                        view.Call( "getWindowVisibleDisplayFrame", rect );

                        return Screen.height - rect.Call<int>( "height" );
                    }
                }
#else
            return (int)TouchScreenKeyboard.area.height;
#endif
        }
    }
}