using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Radio : MonoBehaviour
{
	[Header( "Audio Clips" )]
	public AudioClip TranstionAudioClips;
	
	[Header( "Audio Clips" )]
	public AudioClip[] RadioSouceSource;
	
	[Header( "Audio Sources" )]
	private AudioSource _audio;

	
	
	public LinearMapping linearMapping;
	private float currentLinearMapping = float.NaN;

	public void OnPausePlay()
	{
		if (_audio.isPlaying)
		{
			_audio.Pause();
		}
		else
		{
			_audio.Play();
		}
	}


	private void Start()
	{
		if (_audio==null)
		{
			_audio = GetComponent<AudioSource>();
		}
	StartCoroutine(	NextSound());
	}


	public  IEnumerator NextSound()
	{
		_audio.Pause();
		_audio.clip = TranstionAudioClips;
		_audio.Play();
		Debug.Log("Sound call next ");

		yield return  new  WaitForSeconds(4f);
		_audio.clip = RadioSouceSource[Random.Range(0, RadioSouceSource.Length)];
		if (_audio.clip!=null)
		{
			_audio.Play();	
		}
	 
		yield return  new  WaitForSeconds(2f);
		isActionMade = false;
	}

 
	private bool isActionMade;
	void FixedUpdate()
	{
		if (currentLinearMapping != linearMapping.value)
		{

			currentLinearMapping = linearMapping.value;
			 
				if (!isActionMade)
				{
					Debug.Log("Audio has changed");
				 StartCoroutine(NextSound());
					isActionMade = true;
				}

				 
				{
					
				}
			{
				
			}
		}
	}


	public void pusePlayAudio()
	{

		if (_audio.isPlaying)
		{
			_audio.Pause();
		}
		else
		{
			_audio.Play();
		}
		
	}
 
}
