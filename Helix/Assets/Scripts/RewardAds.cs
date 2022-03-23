using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class RewardAds : MonoBehaviour
{
    private InterstitialAd interstitialAd;
    private string adUnitId = "ca-app-pub-4798405761479497/8652291693";
    private void Update()
    {
        if (interstitialAd == null && GameManager.GMState.Equals(GameManager.GameState.GameOver))
        {
            RequestAndLoadInterstitialAd();
            GameOver();
        }
    }

    public void RequestAndLoadInterstitialAd()
    {
        // Clean up interstitial before using it
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
        interstitialAd = new InterstitialAd(adUnitId);

        // Add Event Handlers
        this.interstitialAd.OnAdLoaded += InterstitialAd_OnAdLoaded;
        this.interstitialAd.OnAdFailedToLoad += InterstitialAd_OnAdFailedToLoad;
        this.interstitialAd.OnAdOpening += InterstitialAd_OnAdOpening;
        this.interstitialAd.OnAdClosed += InterstitialAd_OnAdClosed;

        // Load an interstitial ad
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitialAd.LoadAd(request);
    }

    private void InterstitialAd_OnAdClosed(object sender, System.EventArgs e)
    {
    }

    private void InterstitialAd_OnAdOpening(object sender, System.EventArgs e)
    {
    }

    private void InterstitialAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
    }

    private void InterstitialAd_OnAdLoaded(object sender, System.EventArgs e)
    {
    }

    public void GameOver()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (interstitialAd.IsLoaded())
            {
                interstitialAd.Show();
            }
        }
    }

    public void DestroyInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
    }
}