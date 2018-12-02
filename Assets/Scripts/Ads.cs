//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Advertisements;

//public class Ads : MonoBehaviour
//{
//    public int coinsAfterAd = 50;

//    public void PlayAd()
//    {
//        if (Advertisement.IsReady())
//        {
//            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
//        }
//        else
//        {
//            Popup.Instance.Show("Video Ad is not ready!");
//        }
//    }

//    private void HandleAdResult(ShowResult result)
//    {
//        switch (result)
//        {
//            case (ShowResult.Finished):
//                CoinController.IncreaseDecreaseCoins(coinsAfterAd);
//                break;
//            case (ShowResult.Skipped):
//                Popup.Instance.Show("Please complete the Ad to get " + coinsAfterAd + " coins");
//                break;
//            case (ShowResult.Failed):
//                Popup.Instance.Show("Video Ad is not ready!");
//                break;
//        }
//    }
//}
