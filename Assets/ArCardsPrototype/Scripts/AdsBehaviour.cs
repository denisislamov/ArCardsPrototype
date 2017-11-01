using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsBehaviour : MonoBehaviour
{
	[SerializeField] private string[] urls;
	public void ShowRewardedAd()
	{
		if (!Advertisement.IsReady("rewardedVideo"))
		{
			return;
		}
		
		var options = new ShowOptions { resultCallback = HandleShowResult };
		Advertisement.Show("rewardedVideo", options);
	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
			case ShowResult.Finished:
				Debug.Log("The ad was successfully shown.");
				
				Application.OpenURL(urls[UnityEngine.Random.Range(0, urls.Length - 1)]);
				break;
			case ShowResult.Skipped:
				Debug.Log("The ad was skipped before reaching the end.");
				break;
			case ShowResult.Failed:
				Debug.LogError("The ad failed to be shown.");
				break;
			default:
				throw new ArgumentOutOfRangeException("result", result, null);
		}
	}
}
