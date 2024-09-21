using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour {


	private bool isWorking;
	public ParticleSystem WaterPartucal;
	public AudioSource SoundOfWater;
	private GameObject Water;


	private void Start()
	{
	WaterPartucal.Stop();
		SoundOfWater.Stop();
	}


	public void StartOpenCouldWater()
	{
	 
		if (!isWorking)
		{
			Debug.Log("Open ");
		SoundOfWater.Play();
			StartCoroutine(StartWater());
		}		else
		{
			SoundOfWater.Stop();
            StartCoroutine( EndWater());
		}
	}

	private bool isHot;
	public void StartOpenHotWater()
	{
		
		if (!isHot)
		{
			
			
 
			ParticleSystem.MainModule main =WaterPartucal.GetComponent<ParticleSystem>().main;
			main.startColor = new Color(1f, 0.37f, 0.25f);
			isHot = true;
		}
		else
		{
			isHot = false;
			ParticleSystem.MainModule main =WaterPartucal.GetComponent<ParticleSystem>().main;
			main.startColor = new Color(0.94f, 1f, 0.99f); 
		}
	}


	
	

	private IEnumerator StartWater()
	{
	 
		isWorking = true;
		yield return new WaitForSeconds(0.5f);
		WaterPartucal.Play();
		yield return new WaitForSeconds(6f);
		StartCoroutine(EndWater());
	}
	
	
	private IEnumerator EndWater()
	{
		isWorking = false;
	
		yield return new WaitForSeconds(1f);
		WaterPartucal.Stop();
 
	}
	
	
	
	
	
	private void OnCollisionExit(Collision other)
	{
		
	}

	private void OnCollisionEnter(Collision other)
	{
		
	}
	
}
