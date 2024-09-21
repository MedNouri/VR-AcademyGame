using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(AudioSource))]

public class BulletAlien : MonoBehaviour
{

	public GameObject Explosion;
	public  AudioClip AudioClipExplosion;
	private AudioSource _audioSource;
	void Start()
	{

		
		Destroy(gameObject,2f);
		_audioSource = GetComponent<AudioSource>();
	
		if (_audioSource==null)
		{
			_audioSource = gameObject.AddComponent<AudioSource>();
		}
		_audioSource.clip = AudioClipExplosion;
		_audioSource.Play();
		_audioSource.loop = false;

	}
	
 
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name=="moon_Surface")
		{
			DoExplosion();
		}

 
	}

	private void DoExplosion()
	{		
		GameObject explosionGameObject= Instantiate(Explosion, transform.position, transform.rotation);
	
	
//		_audioSource.Play();
		Destroy( explosionGameObject,2f);
	}
}
 


