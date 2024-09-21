using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoEnd : MonoBehaviour {

	private VideoPlayer m_VideoPlayer;

 

	private void OnEnable()
	{	m_VideoPlayer = GetComponent<VideoPlayer>();
		m_VideoPlayer.loopPointReached += OnMovieFinished;  
	}

	private void OnDisable()
	{
		m_VideoPlayer.loopPointReached -= OnMovieFinished;  
	}

	void OnMovieFinished(VideoPlayer player)
	{
		Debug.Log("Event for movie end called");
		player.Stop();
		SteamVR_LoadLevel.Begin("Main Menu");
	}
}
