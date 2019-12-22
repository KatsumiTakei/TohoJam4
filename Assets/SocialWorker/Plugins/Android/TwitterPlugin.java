import java.io.File;
import java.net.URLEncoder;
import java.util.List;
import java.util.Locale;

import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.pm.ResolveInfo;
import android.net.Uri;
import android.util.Log;

import com.unity3d.player.UnityPlayer;

public class TwitterPlugin
 {
	// タグ 
	public static final String TAG = TwitterPlugin.class.getSimpleName();
	// 改行 
	public static final String BR = System.getProperty("line.separator");
	
	// UnitySendMessage：GameObject名 
	public static final String UNITY_SEND_GAMEOBJECT = "SocialWorker";
	// UnitySendMessage：コールバック名 
	public static final String UNITY_SEND_CALLBACK = "OnSocialWorkerResult";
	
	/**
	 * Twitter投稿
	 * @param message メッセージ
	 * @param url URL。空文字の場合は処理されない。
	 * @param imagePath 画像パス(PNG/JPGのみ)。空文字の場合は処理されない。
	 */
	public void postTwitter(String message, String url, String imagePath) 
	{
		try 
		{
			String type = (imagePath.equals("")) ? "text/plain" : getIntentTypeForImage(imagePath);
			Intent intent = createAppIntent(Intent.ACTION_SEND, type);
    		if(intent != null)
			{
				String txt = (message.equals("")) ? url : message + BR + url;
    			intent.putExtra(Intent.EXTRA_TEXT, txt);
     			if(!imagePath.equals("")) 
				{
    				intent.putExtra(Intent.EXTRA_STREAM, Uri.fromFile(new File(imagePath)));
    			}
				
    			UnityPlayer.currentActivity.startActivity(intent);
    			UnityPlayer.UnitySendMessage(UNITY_SEND_GAMEOBJECT, UNITY_SEND_CALLBACK, "SUCCESS");
    		} 
			else 
			{
    			UnityPlayer.UnitySendMessage(UNITY_SEND_GAMEOBJECT, UNITY_SEND_CALLBACK, "NOT_AVAILABLE");
    		}
    	} 
		catch(Exception e)
		{
    		Log.e(TAG, "postTwitter", e);
    		UnityPlayer.UnitySendMessage(UNITY_SEND_GAMEOBJECT, UNITY_SEND_CALLBACK, "ERROR");
    	}
	}
    
	/**
	 * 画像のIntentタイプを取得
	 * @param imagePath 画像パス(PNG/JPGのみ)
	 * @return Intentタイプ
	 */
	private String getIntentTypeForImage(String imagePath)
 	{
		String extension = imagePath.substring(imagePath.lastIndexOf(".") + 1).toLowerCase(Locale.getDefault()) ;
		return (extension == ".png") ? "Temp/png" : "Temp/jpg";
	}
	
	/**
	 * アプリを起動させるためのIntentを生成
	 * @param action Intentアクション
	 * @param type Intentタイプ
	 * @return Intent。アプリがない場合は null
	 */
	private Intent createAppIntent(String action, String type) throws Exception 
	{
		try 
		{
			Intent intent = new Intent(action);
			intent.setType(type);
			intent.setClassName("com.twitter.android", "com.twitter.composer.ComposerActivity");
			
			return intent;
			
		} 
		catch (Exception e)
		{
			throw e;
		}
	}
}
