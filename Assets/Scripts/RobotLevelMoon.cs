using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Policy;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RobotLevelMoon : Robot{
	public List<MoonEventRelay.EventMessageType> EventsHandeld=
		new List<MoonEventRelay.EventMessageType>();

public AudioClip StartM2;
public AudioClip TheAvregTemp;
public AudioClip GetoutofTheSapceShip ;
public AudioClip infoMoonFirstPerson ;
public AudioClip SignalInfo ;
public AudioClip SingalproblemAudio ;
public AudioClip UsthisLadderAudioClip ;
	
	
public AudioClip PutThePlug ;
public AudioClip InfoGravity;
public AudioClip taketheGun;
public AudioClip GetDown;
 
public AudioClip GetBack;
	
	
	
	private GoTween _tween =null;
	public GameObject Gun;
	public TeleportPoint TeleportPointExitSpaceShip;
	public TeleportArea TeleportGoToSignal;
 
 

	private void OnEnable()
	{
		MoonEventRelay.OnEventAction += HandleEvent;
	 
	}
 

	private void OnDisable()
	{
		MoonEventRelay.OnEventAction -= HandleEvent;
	 
	}
	
	

	string HandleEvent(MoonEventRelay.EventMessageType type)
	{

		if (EventsHandeld.Contains(type))
		{
			Debug.Log("Robot  Event Recevide  ");
			switch (type)
			{
				
				
				case MoonEventRelay.EventMessageType.Landing:
			       Invoke("LandingTalk",1f);
					break;
				case MoonEventRelay.EventMessageType.AskForStoppingTheEngin:
					Invoke("OnAskForStoppingTheEngin",1f);
					break;
				case MoonEventRelay.EventMessageType.EnginShutDown:
					ValidTask();
					break;
				 	 
				case MoonEventRelay.EventMessageType.FixSignal:
					
					Invoke("AskForSignal",2f);
					break;
	 
				case MoonEventRelay.EventMessageType.PlayerIsOut:
					ValidTask();
					Invoke("TellTheUserAboutSignalInfo",3f);
					break;
				
				case MoonEventRelay.EventMessageType.RobotGetUp:
					ValidTask();
					Invoke("RobotGetUpToFixSignal",2f);
					break;
				case MoonEventRelay.EventMessageType.SignalFixed:
				 ValidTask();
					
					Invoke("EnemiesAttack",5f);
					break;
		    case MoonEventRelay.EventMessageType.ValidTask:
				 ValidTask();
		   break;
				case MoonEventRelay.EventMessageType.DoorIsOpen:
					ValidTask();
					Invoke("GetOutOfLunner",1f);
					break;
				
				case MoonEventRelay.EventMessageType.ClambEnd:
					ValidTask();
					Invoke("PlayerisUp",2f);
					break;
				
	 		
				case MoonEventRelay.EventMessageType.EnimesAttackEnd:
					ValidTask();
					Invoke("EndEnemiesTask",2f);
					break;
				
				
				case MoonEventRelay.EventMessageType.GobackToSapceShip:
					ValidTask();
					Invoke("GoToSpaceShip",2f);
					break;
			}

		}

		return "";
	}


	private void TellTheUserAboutSignalInfo()
	{
		PlaySoundWithCallback(SignalInfo,SignlProblemEvent);
	
		Debug.Log("Signal info ");
	}

	private void SignlProblemEvent()
	{
		MoonEventRelay.RelayEvent(MoonEventRelay.EventMessageType.FixSignal);
	}


	private void TehAvregTemp()
	{
 PlaySoundOnshoot(TheAvregTemp);
	}

	private void LandingTalk()
	{
		Debug.Log("Hello into Mission Two ");
		PlaySoundWithCallback(StartM2,TehAvregTemp);

		Debug.Log("Landing");
		
	}

	private void OnAskForStoppingTheEngin()
	{
		PlaySoundWithCallback(GetoutofTheSapceShip,WaitForThPLayerToStopEnginOpenDoor);
	}


	private void WaitForThPLayerToStopEnginOpenDoor()
	{
		 
		Debug.Log("Waiting For The Player To get Out ");
	 
	}


	private void GoToFixSignal()
	{TeleportGoToSignal.SetLocked(false);
		var path = new GoSpline("RobotGoToFixSignal");
		_tween = Go.to(transform, 12, new GoTweenConfig()
			.positionPath(path, false)
			.setIterations(1).onComplete(tween =>ClampMission()));
			
	}

	private void ClampMission()
	{
		MoonEventRelay.RelayEvent(MoonEventRelay.EventMessageType.PlayerShouldStartClamb);
		
		
	}
	
	
	private void GetOutOfLunner()
	{
		PlaySoundWithCallback(infoMoonFirstPerson,cangetout);
		var path = new GoSpline("RobotGetOutOflunner");
		_tween = Go.to(transform, RoborSpeed, new GoTweenConfig()
			.positionPath(path, false)
			.setIterations(1).onComplete(tween =>WaitForThPLayerToGEtOut()));

	}

	private void cangetout()
	{
		TeleportPointExitSpaceShip.SetLocked(false);
	}


	private void WaitForThPLayerToGEtOut()
	{

		Debug.Log("Waiting For The Player To get Out ");
	 
	}



	private void AskForSignal()
	{
PlaySoundWithCallback(SingalproblemAudio,GoToFixSignal);
	}

	private void info()
	{
		
		Debug.Log("Waiting For The Player To get Out ");
	}

	private void RobotGetUpToFixSignal()
	{
		
		PlaySoundWithCallback(UsthisLadderAudioClip,AnimationGetUpRobot);
	
	}

	private void AnimationGetUpRobot()
	{
		var path = new GoSpline("RobotGetUpToFixSignal");
		_tween = Go.to(transform, RoborSpeed, new GoTweenConfig()
			.positionPath(path, false)
			.setIterations(1).onComplete(tween =>WaitForThPLayerToGEtOut()));
	}


	private	void EnemiesAttack()
	{
	PlaySoundWithCallback(InfoGravity,EndInfoOfgravityStartEnimes);
	}

	private void EndInfoOfgravityStartEnimes()
	{	Scared();
Invoke("HideFromAliens",2f);
	}


	private void HideFromAliens()
	{
		
		HideUntilEndOfAttack();
     	
		MoonEventRelay.RelayEvent(MoonEventRelay.EventMessageType.EnemiesAttackStart);	
		
	}
	
	private void HideUntilEndOfAttack()
	{
 PlaySoundWithCallback(taketheGun,ShowGunToPalyer);
		var path = new GoSpline("Robot_Hide_from_Alien");
		_tween = Go.to(transform, RoborSpeed, new GoTweenConfig()
			.positionPath(path, false)
			.setIterations(1));
		Scared();

	}



	private void  GetoutOfhidePlace()
	{
		
		var path = new GoSpline("Robot_Get_ToSpace_home1");
	
		 
		_tween = Go.to(transform, RoborSpeed, new GoTweenConfig()
			.positionPath(path, false)
			.setIterations(1));
	}

	
	
	private void  GoToSpaceShip()
	{
	
		PlaySoundWithCallback(GetBack,PlayerShouldgetoutOfElevator);
		Invoke("BackToMenu",10f);
	
	}

	private void BackToMenu()
	{
		// SteamVR_LoadLevel.Begin("Main Menu",true,2f,0f,1f,2f,1f);
		// 
		}


	void PlayerShouldgetoutOfElevator()
	{
			
		var path = new GoSpline("Robot_Get_ToSpace_home2");

		_tween = Go.to(transform, RoborSpeed, new GoTweenConfig()
			.positionPath(path,false)
			.setIterations(1));
	}
	
	private void ShowGunToPalyer()
	{
		Gun.SetActive(true);
		StayScared();
	}


	private void PlayerisUp()
	{
		
		Debug.Log("The Player is Up ");
		PlaySoundOnshoot(PutThePlug);
		
	}


	private void EndEnemiesTask()
	{
		
		PlaySoundWithCallback(GetDown,GetToElevator);
		Gun.active = false;
		Destroy(Gun);
	}

	private void GetToElevator()
	{

		GetoutOfhidePlace();
	}
}
