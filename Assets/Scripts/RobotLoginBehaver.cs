using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

//======= Copyright (c) VRACADEMY 2018 , All rights reserved. ===============

[RequireComponent(typeof(AudioSource))]
public class RobotLoginBehaver : Robot{
 
	
	//Liste Of the handled Events by the Robot 
	public List<LoginEventRelay.EventMessageType> EventsHandled=
		new List<LoginEventRelay.EventMessageType>();
	
	
	
	private void OnEnable()
	{
		//Subscribe To An Event OnEnable
		LoginEventRelay.OnEventAction += HandleEvent;
	 
	}
 

	private void OnDisable()
	{
		LoginEventRelay.OnEventAction -= HandleEvent;
	 
	}

	
	public AudioClip WelcomeToVrAcademy;
	public AudioClip AboutVrAcademy;
	public AudioClip AboutTheRobotAskForTheName;
	public AudioClip UseTheKeyBoard;
	private AudioClip FillTheForm;
	public AudioClip LooksGood;
	public AudioClip HowToExitTheGameAudio;

 
	
	void Start () {
	
	 
	 
		Invoke("MovetoPlayer",2f);
 
		 
	}



	private void MovetoPlayer()
	{
		var path = new GoSpline( "RoboT_login" );
		GoTween moveBlock =	Go.to( transform, RoborSpeed, new GoTweenConfig().positionPath( path,true).setEaseType( GoEaseType.CircOut));  
		moveBlock.setOnCompleteHandler(c=>EndAnimation()
		);
	}


	void AudioWelcomeFinished()
	{
		
		PlaySoundWithCallback(AboutVrAcademy,UseKeyBoard);

	
	 

 
	}

	void ShowKeyboard()
	{
		PlaySoundOnshoot(UseTheKeyBoard);
		LoginEventRelay.RelayEvent(LoginEventRelay.EventMessageType.FillTheForm);
	}

	void UseKeyBoard()
	{
		PlaySoundWithCallback(AboutTheRobotAskForTheName,ShowKeyboard);
		
		
	}



	void EndAnimation()
	{
		// Robot Is At Traget Pos Sen Event 
		LoginEventRelay.RelayEvent(LoginEventRelay.EventMessageType.RobotFinishTalking);
		
		// Talk About VR ACADEMY 
		PlaySoundWithCallback(WelcomeToVrAcademy, AudioWelcomeFinished);
	
      

	}


 

	//Handel  Events Function  
	string HandleEvent(LoginEventRelay.EventMessageType type)
	{

		if (EventsHandled.Contains(type))
		{
		 
			switch (type)
			{
				case LoginEventRelay.EventMessageType.FormCompleted:
					Happy();
					validFeedBack();
					break;
    
				case LoginEventRelay.EventMessageType.Exit:
					Happy();
					HelphowToExitTheGame();
					break;


			}

		}

		return "Robot Login Behavers";
	}
	
	
	
	
	

	private void validFeedBack()
	{
		
	 	PlaySoundWithCallback(LooksGood,HelphowToExitTheGame);
		Invoke("LoadMenuLevel",25);

	}


	private void HelphowToExitTheGame()
	{

		StartCoroutine(HelphowToExitTheGamePlay());
		Debug.Log("Hho to Exit");
	}

	private IEnumerator HelphowToExitTheGamePlay()
	{
		
		yield return  new WaitForSeconds(2f);
		PlaySoundOnshoot(HowToExitTheGameAudio);
		
	}
	void LoadMenuLevel()
	{
		Debug.Log("Load ELVEl is Ready ");
		SteamVR_LoadLevel.Begin("Main Menu",true,0.5f);
	}
	
	
}
