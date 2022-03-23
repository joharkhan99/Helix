
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class FreeCoinAd : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private string adUnitId = "ca-app-pub-4798405761479497/9105865064";
    public Button NoInternetBanner;
    IEnumerator RemoveAfterSeconds(int seconds, string text)
    {
        NoInternetBanner.GetComponentInChildren<Text>().text = text;
        NoInternetBanner.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        NoInternetBanner.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += RewardedAd_OnAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += RewardedAd_OnAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += RewardedAd_OnAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += RewardedAd_OnAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += RewardedAd_OnUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += RewardedAd_OnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    private void RewardedAd_OnAdClosed(object sender, System.EventArgs e)
    {
        this.CreateAndLoadRewardedAd();
        Debug.Log(e.ToString());
    }
    public void CreateAndLoadRewardedAd()
    {
        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.OnAdLoaded += RewardedAd_OnAdLoaded;
        this.rewardedAd.OnUserEarnedReward += RewardedAd_OnUserEarnedReward;
        this.rewardedAd.OnAdClosed += RewardedAd_OnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }


    private void RewardedAd_OnUserEarnedReward(object sender, Reward args)
    {
        int gold = Random.Range(100, 300);
        StartCoroutine(RemoveAfterSeconds(2, "You Got "+gold+" Coins"));
        GameObject.Find("ScoreManager").GetComponent<ScoreManager>().UpdateGold(gold);
    }

    private void RewardedAd_OnAdFailedToShow(object sender, AdErrorEventArgs e)
    {
    }

    private void RewardedAd_OnAdOpening(object sender, System.EventArgs e)
    {
    }

    private void RewardedAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
    }

    private void RewardedAd_OnAdLoaded(object sender, System.EventArgs e)
    {
    }

    public void ShowAdOnClick()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (this.rewardedAd.IsLoaded())
            {
                this.rewardedAd.Show();
            }
        }
        else
        {
            StartCoroutine(RemoveAfterSeconds(2, "There is no Internet Connection"));
        }
    }
}