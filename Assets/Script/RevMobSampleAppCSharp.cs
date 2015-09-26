using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevMobSampleAppCSharp : MonoBehaviour, IRevMobListener
{
		/*
	private static readonly int BUTTON_WIDTH = 200;
	private static readonly int BUTTON_HEIGHT = 50;
	private static readonly int PADDING = 20;

	private static readonly int X_POSITION_REFERENCE = 10 + PADDING;
	private static readonly int Y_POSITION_REFERENCE = BUTTON_HEIGHT + PADDING;

	private int i = 0;
	private Vector2 scrollPosition = Vector2.zero;
	delegate void OnButtonClicked();
    */
		private static readonly Dictionary<String, String> appIds = new Dictionary<String, String> () {
		{ "Android", "54cf3b56aa7a6bf20972fdd6"}, // Find your RevMob App IDs in revmob.com
		{ "IOS", "54cf3b45aa7a6bf20972fdcc" }
	};

		public RevMob revmob;
		public RevMobFullscreen fullscreen;
		private RevMobPopup popup;
		private RevMobLink adLink;
		private RevMobBanner banner;
		private RevMobBanner loadedBanner;

		void OnApplicationFocus ()
		{
		}

		void Update ()
		{
		}

		public void StartRevMob ()
		{
				revmob = RevMob.Start (appIds, this.gameObject.name.ToString ());
		}
		/*
	void CreateButton(String label, OnButtonClicked onClicked) {
		i++;
        onClicked();
	}
    
	void CreateEnvButtons() {
		CreateButton("Exit", () => { Application.Quit(); });
		CreateButton("Testing Mode With Ads", () => { revmob.SetTestingMode(RevMob.Test.WITH_ADS); });
		CreateButton("Testing Mode Without Ads", () => { revmob.SetTestingMode(RevMob.Test.WITHOUT_ADS); });
		CreateButton("Disable Testing Mode", () => { revmob.SetTestingMode(RevMob.Test.DISABLED); });
		CreateButton("Print Env Information", () => { revmob.PrintEnvironmentInformation(); });
		CreateButton("Set timeout to 5s", () => { revmob.SetTimeoutInSeconds(5); });
	}
    */
		public void CreateFullscreen ()
		{
				fullscreen = revmob.CreateFullscreen ();
		}

		public void ShowFullScreen ()
		{
				fullscreen.Show ();
		}

		public void HideFullScreen ()
		{
				fullscreen.Hide ();
		}

		public void CreatePopUp ()
		{
				popup = revmob.CreatePopup ();
		}

		public void ShowPopUp ()
		{
				popup.Show ();
		}

		public void CreateAdLink ()
		{
				adLink = revmob.CreateAdLink ();
		}

		public void OpenAdLink ()
		{
				adLink.Open ();
		}



	#region IRevMobListener implementation
		public void SessionIsStarted ()
		{
				Debug.Log (">>> Session Started");
		}

		public void SessionNotStarted (string message)
		{
				Debug.Log (">>> Session not started");
		}

		public void AdDidReceive (string revMobAdType)
		{
				Debug.Log (">>> AdDidReceive: " + revMobAdType);
		}

		public void AdDidFail (string revMobAdType)
		{
				Debug.Log (">>> AdDidFail: " + revMobAdType);
		}

		public void AdDisplayed (string revMobAdType)
		{
				Debug.Log (">>> AdDisplayed: " + revMobAdType);
		}

		public void UserClickedInTheAd (string revMobAdType)
		{
				Debug.Log (">>> AdClicked: " + revMobAdType);
		}

		public void UserClosedTheAd (string revMobAdType)
		{
				Debug.Log (">>> AdClosed: " + revMobAdType);
		}

		public void InstallDidReceive (string message)
		{
		}

		public void InstallDidFail (string message)
		{
		}
  
		public void EulaIsShown ()
		{
				Debug.Log (">>> EulaShown");
		}
    
		public void EulaAccepted ()
		{
				Debug.Log (">>> EulaAccepted");
		}
      
		public void EulaRejected ()
		{
				Debug.Log (">>> EulaRejected");
		}

	#endregion
}