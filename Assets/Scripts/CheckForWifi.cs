using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CheckForWifi : MonoBehaviour
{
    [Tooltip("The GameObject used to display a WiFi error")]
	public GameObject WifiNotification;

	void Start()
	{
		//DontDestroyOnLoad(this);
		WifiNotification.SetActive(false);
		InvokeRepeating("StartInternetChecking",3.0f,3.0f);
	}
	
	void StartInternetChecking()
	{
		StartCoroutine(CheckInternet());
	}
	
	IEnumerator CheckInternet(){
		if (WifiNotification == null)
			WifiNotification = GameObject.Find("WifiCanvas");
		
		UnityWebRequest www = UnityWebRequest.Get("http://google.com");
		www.timeout = 1;
		
		yield return www.SendWebRequest();
		
		if (www.isNetworkError || www.isHttpError)
		{
			Debug.LogError("Error. Check internet connection!");
			WifiNotification.SetActive(true);
		}
		else{
			WifiNotification.SetActive(false);
		}
	}
}
