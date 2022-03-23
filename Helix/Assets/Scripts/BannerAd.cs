using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAd : MonoBehaviour
{
    string BannerAd_ID = "ca-app-pub-4798405761479497/5779464777";

    private BannerView bannerView;
    private void Update()
    {
/*        if (GameManager.GMState.Equals(GameManager.GameState.GamePlay))
        {
            if (GameObject.Find("FULL_BANNER(Clone)") != null)
            {
                GameObject.Find("FULL_BANNER(Clone)").SetActive(false);
            }
        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        RequestBanner();
    }

    public void RequestBanner()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
        AdSize adSize = new AdSize(468, 60);
        bannerView = new BannerView(BannerAd_ID, adSize, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
        bannerView.Show();
    }
    public void DestroyBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Hide();
            bannerView.Destroy();
            bannerView = null;
        }
    }
}