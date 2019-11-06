using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{

    private string gameId = "3353258";
    Button myButton;
    public string myPlacementId = "video";
    GameController controller;

    public void Init()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        myButton = GetComponent<Button>();
 
        // Set interactivity to be dependent on the Placement’s status:
        myButton.interactable = Advertisement.IsReady(myPlacementId);

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, false);
    }

    // Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo()
    {
        if(Advertisement.IsReady("video"))
            Advertisement.Show("video");
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId)
        {
            myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            controller.Continue();
        }
        else if (showResult == ShowResult.Skipped)
        {
            controller.Continue();
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The add did not finish due to error");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    
    }
}