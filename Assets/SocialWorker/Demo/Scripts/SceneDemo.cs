//----------------------------------------------
// SocialWorker
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;

namespace SWorker
{
    /// <summary>
    /// デモシーン
    /// </summary>
	public class SceneDemo : MonoBehaviour
	{
		private static readonly string ExtensionImage = ".png";
//		private static readonly string ExtensionImage = ".jpeg";

		public Text Result;
		public RawImage Image;

        /// <summary>
        /// 開始処理
        /// </summary>
        void Start()
        {
            //StartCoroutine(WriteFileProcess());
            //// Post画像は端末から読み込むので、ない場合はあらかじめ保存しておくこと
            //string imagePath = Application.persistentDataPath + "/image" + ExtensionImage;
            //if (!File.Exists(imagePath))
            //{
            //    Texture2D texture = (Texture2D)Image.texture;
            //    byte[] data = (ExtensionImage == ".png") ? texture.EncodeToPNG() : texture.EncodeToJPG();
            //    File.WriteAllBytes(imagePath, data);
            //    Debug.Log(data);
            //}

            //Texture2D Imagetexture = (Texture2D)Image.texture;
            //Debug.Log("File Exist : " + Imagetexture.EncodeToPNG());
        }

        /// <summary>
        /// Twitter投稿
        /// </summary>
        public void OnPostTwitter()
        {
			string message   = "message";
            string url       = "http://yedo-factory.co.jp/";
			string imagePath = Application.persistentDataPath + "/Temp" + ExtensionImage;
			SocialWorker.PostTwitter(message, url, imagePath, (p) => { Debug.Log(p); });
//			SocialWorker.PostTwitter(message, "", OnResult);
//			SocialWorker.PostTwitter("", imagePath, OnResult);
        }

        /// <summary>
        /// Facebook投稿。ただしFacebookは画像の投稿のみ許可しており、テキストの投稿は無視されることに注意。
        /// </summary>
        public void OnPostFacebook()
        {
			string imagePath = Application.persistentDataPath + "/image" + ExtensionImage;
            SampleSocialWorker.PostFacebook(imagePath, OnResult);
        }

        /// <summary>
        /// Line投稿。Lineはメッセージと画像の同時投稿は行えないことに注意。
        /// </summary>
        public void OnPostLine()
        {
			string message   = "message";
			string imagePath = Application.persistentDataPath + "/image" + ExtensionImage;
            SampleSocialWorker.PostLine(message, imagePath, OnResult);
//			SocialWorker.PostLine(message, "", OnResult);
//			SocialWorker.PostLine("", imagePath, OnResult);
        }

        /// <summary>
        /// Instagram投稿。Instagramは画像の投稿のみ行える。
        /// </summary>
        public void OnPostInstagram()
        {
			string imagePath = Application.persistentDataPath + "/image" + ExtensionImage;
            SampleSocialWorker.PostInstagram(imagePath, OnResult);
        }

        /// <summary>
        /// メール投稿
        /// </summary>
        public void OnPostMail()
        {
			string[] to      = new string[] { "to@hoge.com" };
			string[] cc      = new string[] { "cc@hoge.com" };
			string[] bcc     = new string[] { "bcc@hoge.com" };
			string subject   = "subject";
			string message   = "message";
			string imagePath = Application.persistentDataPath + "/image" + ExtensionImage;
            SampleSocialWorker.PostMail(to, cc, bcc, subject, message, imagePath, OnResult);
//			SocialWorker.PostMail(message, "", OnResult);
//			SocialWorker.PostMail("", imagePath, OnResult);
        }

        /// <summary>
        /// アプリ選択式での投稿
        /// </summary>
        public void OnCreateChooser()
        {
			string message   = "message";
			string imagePath = Application.persistentDataPath + "/image" + ExtensionImage;
            SampleSocialWorker.CreateChooser(message, imagePath, OnResult);
//			SocialWorker.CreateChooser(message, "", OnResult);
//			SocialWorker.CreateChooser("", imagePath, OnResult);
        }

        /// <summary>
        /// 結果コールバック
        /// </summary>
        /// <param name="res">結果値</param>
        public void OnResult(SocialWorkerResult res)
        {
    //        switch(res)
    //        {
				//case SocialWorkerResult.Success:
				//	Result.text = "Result : Success";
    //                break;
    //            case SocialWorkerResult.NotAvailable:
				//	Result.text = "Result : NotAvailable";
    //                break;
    //            case SocialWorkerResult.Error:
				//	Result.text = "Result : Error";
    //                break;
    //        }
        }

        string _fileName = "/Image.png";
        private IEnumerator WriteFileProcess()
        {
            //_fileName = "Screenshot" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            yield return CaptureScreenshotProcess();
            yield return MediaDirWriteFileProcess();
        }

        private IEnumerator CaptureScreenshotProcess()
        {
            Debug.Log("CaptureScreenshotProcess");
            yield return new WaitForEndOfFrame();
            string path = null;
#if UNITY_EDITOR
            path = _fileName;
#elif UNITY_ANDROID
        path = Application.persistentDataPath + "/" + _fileName;
#endif

            Debug.Log("BeginCaptureScreenshot:" + path);
            ScreenCapture.CaptureScreenshot(_fileName);
            Debug.Log("AfterCaptureScreenshot:" + path);

            while (File.Exists(path) == false)
            {
                Debug.Log("NoFile:" + path);
                yield return new WaitForEndOfFrame();
            }

            Debug.Log("CaptureOK:" + path);
            scanFile(path, null);//"image/png";
        }

        private IEnumerator MediaDirWriteFileProcess()
        {
            Debug.Log("MediaDirWriteFileProcess");
            if (Application.platform != RuntimePlatform.Android)
                yield return null;
#if UNITY_ANDROID
            var path = Application.persistentDataPath + "/" + _fileName;
            while (File.Exists(path) == false)
            {
                Debug.Log("NoFile:" + path);
                yield return new WaitForEndOfFrame();
            }
            // 保存パスを取得
            using (AndroidJavaClass jcEnvironment = new AndroidJavaClass("android.os.Environment"))
            using (AndroidJavaObject joPublicDir = jcEnvironment.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory", jcEnvironment.GetStatic<string>("DIRECTORY_PICTURES"/*"DIRECTORY_DCIM"*/ )))
            {
                var outputPath = joPublicDir.Call<string>("toString");
                Debug.Log("MediaDir:" + outputPath);
                //              outputPath += "/100ANDRO/" + _fileName;
                outputPath += "/Screenshots/" + _fileName;
                var pngBytes = File.ReadAllBytes(path);
                File.WriteAllBytes(outputPath, pngBytes);
                Debug.Log("afe WriteAllBytes");
                yield return new WaitForEndOfFrame();
                while (File.Exists(outputPath) == false)
                {
                    Debug.Log("NoFile:" + outputPath);
                    yield return new WaitForEndOfFrame();
                }
                Debug.Log("MediaDirWriteFileOK:" + outputPath);
                scanFile(outputPath, null);
            }
#endif
        }

        //インデックス情報にファイル名を登録する
        //これをしないとPC から内部ストレージを参照した時にファイルが見えない
        static void scanFile(string path, string mimeType)
        {
            if (Application.platform != RuntimePlatform.Android)
                return;
#if UNITY_ANDROID
            using (AndroidJavaClass jcUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (AndroidJavaObject joActivity = jcUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            using (AndroidJavaObject joContext = joActivity.Call<AndroidJavaObject>("getApplicationContext"))
            using (AndroidJavaClass jcMediaScannerConnection = new AndroidJavaClass("android.media.MediaScannerConnection"))
            using (AndroidJavaClass jcEnvironment = new AndroidJavaClass("android.os.Environment"))
            using (AndroidJavaObject joExDir = jcEnvironment.CallStatic<AndroidJavaObject>("getExternalStorageDirectory"))
            {
                Debug.Log("scanFile:" + path);
                var mimeTypes = (mimeType != null) ? new string[] { mimeType } : null;
                jcMediaScannerConnection.CallStatic("scanFile", joContext, new string[] { path }, mimeTypes, null);
            }
            Handheld.StopActivityIndicator();
#endif
        }

    }

}