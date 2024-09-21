using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoEffect : MonoBehaviour
{


	public GameObject LightOff;
	public GameObject LightOn;
	private bool Enbale;
	
	// Use this for initialization
	void Start ()
	{
	 
		StartCoroutine(TurnOnOff());
	}



	private IEnumerator TurnOnOff()
	{

		LightOff.SetActive(false);
			LightOn.SetActive(true);
		yield return new  WaitForSeconds(1f);
			LightOff.SetActive(true);
			LightOn.SetActive(false);
		yield return new  WaitForSeconds(1f);
		StartCoroutine(TurnOnOff());



	}

  



}
