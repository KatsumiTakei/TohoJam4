using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAds : MonoBehaviour
{
    BannerView bannerView = null;

    void Start()
    {
        string testId = "ca-app-pub-3940256099942544~3347511713";
        string appId = "ca-app-pub-6577425048094658~1886457194";

        MobileAds.Initialize(appId);

#if UNITY_ANDROID
        string adTestId = "ca-app-pub-3940256099942544/6300978111";
        string adUnitId = "ca-app-pub-6577425048094658/5839866840";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif

        AdRequest request = new AdRequest.Builder()/*.AddTestDevice("116F7024F72ABAFCE18652750E480004")*/.Build();
        foreach (var device in request.TestDevices)
            Debug.Log(device);

        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        bannerView.LoadAd(request);
        bannerView.Show();

        Debug.Log("GoogleAds Initialized !!");

    }

}