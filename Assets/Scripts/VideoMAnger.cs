using System;
using System.Collections;
using System.Collections.Generic;
 
using UnityEngine;
 
using UnityEngine.UI;
using UnityEngine.Video;


public class VideoMAnger : MonoBehaviour
{

	public List<Sprite> VideosSprites = new List<Sprite>();
 
	public Image MImage;

	private int n;



	private void Start()
	{
		MImage = MImage.GetComponent<Image>();

		MImage.sprite = VideosSprites[0];

	}

	public void SlidRight()
	{

		MenuEventRelay.RelayEvent(
			MenuEventRelay.EventMessageType.Lostfocus);
		// VideosSprites.Count
		if (n <2)
		{


			MImage.sprite = null;
			MImage.sprite = VideosSprites[n + 1];
			n++;
		}


	}

	public void SlideLeft()
	{
		MenuEventRelay.RelayEvent(
			MenuEventRelay.EventMessageType.Lostfocus);
		if (n > 0)
		{


			MImage.sprite = null;
			MImage.sprite = VideosSprites[n - 1];
			n--;
		}

	}





	private GameObject videoPlayer;

	public void LoadVideo()
	{

 

		 
		if (MImage.sprite == VideosSprites[0])
		{
			Debug.Log("Firste video");
			SteamVR_LoadLevel.Begin("360_Video_PLAYER");
		}


		if (MImage.sprite == VideosSprites[1])
		{
			Debug.Log("2 video");
			SteamVR_LoadLevel.Begin("360_Video_Player_Video2");
		}
		

		if (MImage.sprite == VideosSprites[2])
		{
			SteamVR_LoadLevel.Begin("360_Video_Video3");
		}


	}
 

 
}

 
 