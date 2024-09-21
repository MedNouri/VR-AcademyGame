using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour {


	
	public const string Menu = "Main Menu";
	public const string Val2 = "MyVal2";
	public const string Val3 = "MyVal3";



	
public   static  void LoadLevel( string NAme)
	{
		SteamVR_LoadLevel.Begin(NAme,true,2f,1f,1f,1f,1f);
		
 Debug.Log("Load level ");
			
	}
	
	
}
