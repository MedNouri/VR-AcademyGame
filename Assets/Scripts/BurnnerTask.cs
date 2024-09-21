using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Valve.VR.InteractionSystem;

public class BurnnerTask : MonoBehaviour
{



	private bool isWorking;
	public ParticleSystem FirePartucal;
	public ParticleSystem  SomkeEffct;
	private GameObject Water;

	public DilutionFlask DilutionFlask;


	private void Start()
	{
		FirePartucal.Stop();
		SomkeEffct.Stop();

	}


	public void Openburnner()
	{
	 
		if (!isWorking)
		{
			Debug.Log("Open ");
		
		StartCoroutine(StartFire());
		}		else
		{
			Debug.Log("its Not Working Soory Open ");
		}
	}

	public void Closeburnner()
	{
		
		if (isWorking)
		{
			
			Debug.Log("Close ");
			StartCoroutine(EndFire());
		}
		else
		{
			Debug.Log("its Not Working Soory ");
		}
	}


	
	

	private IEnumerator StartFire()
	{
	 
		isWorking = true;
		yield return new WaitForSeconds(1f);
       FirePartucal.Play();
 
	}
	
	
	private IEnumerator EndFire()
	{
		isWorking = false;
	
		yield return new WaitForSeconds(1f);
		FirePartucal.Pause();
		FirePartucal.Clear();
 
	}
	private void OnCollisionExit(Collision other)
	{ 
	 
	}

	private void OnCollisionEnter(Collision other)
	{

	 
		
	}

	private bool isActionMade;
	private void OnCollisionStay(Collision other)
	{
		if (other.gameObject.name=="Flask")
		{
			print("i should start ");
		  if((isWorking)&&(!isActionMade))
		  {
			  print("i should start  22");
			  isActionMade = true;
			DilutionFlask.Evaporation();
			}
		}	 
	}
}
