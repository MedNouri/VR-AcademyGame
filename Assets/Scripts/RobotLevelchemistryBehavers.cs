using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RobotLevelchemistryBehavers :Robot
{
	public List<EventRelayLevel2.EventMessageType> EventsHandled=
		new List<EventRelayLevel2.EventMessageType>();
	public AudioClip WelcomeToLevel2;
	public AudioClip NeedPassword;
	public AudioClip PasswordEntred;
	public AudioClip TelltheplayerAboutSaifty;
	public AudioClip LetStart;
	public AudioClip YouCanStart;
 
	public AudioClip AskForMakeCarbon;
	public AudioClip Infonacl;
 
	public AudioClip AsktoDilutuon;
	public AudioClip AboutBoil;
	public AudioClip AskBoiling;
	public AudioClip GreatJob;
	public AudioClip Virus;

	public AudioClip End;

	public TeleportArea TeleportAreaLab;
	
	private GoTween _tween;
	void Start ()
	{

		StartCoroutine(waitForThePlayerTofocus());





	}




	IEnumerator waitForThePlayerTofocus()
	{
		yield return new WaitForSeconds(2f);
		
		PlaySoundWithCallback(WelcomeToLevel2, MovetoDoor);
	}

	private void MovetoDoor()
	{
		var path = new GoSpline( "Robot_Level2_Password" );
		GoTween moveBlock =	Go.to( transform, RoborSpeed, new GoTweenConfig().positionPath( path,true).setEaseType( GoEaseType.CircOut).setDelay(0.2f));  
		moveBlock.setOnCompleteHandler(c=>EndAnimationDoor()
		);
	}


	private void EndAnimationDoor()
	{
 

		TeleportAreaLab.SetLocked(false);


	}
	
	
	
	string HandleEvent(EventRelayLevel2.EventMessageType type)
	{

		if (EventsHandled.Contains(type))
		{
			Debug.Log("Level manger HAndel Evtn Recevide  ");
			switch (type)
			{
		   case EventRelayLevel2.EventMessageType.EnterPassword:
			  ValidTask();
              Invoke("EnterPassword",2f);
			   break;
			   ;
				case EventRelayLevel2.EventMessageType.DoorisOpen:
					ValidTask();
					Invoke("DoorIsOpenEnterTheLAb",2f);
					break;
					;
				case EventRelayLevel2.EventMessageType.PlayerisOnTheLab:
					ValidTask();
					Invoke("PlayerisInThelab",2f);
					break;
					;
				case EventRelayLevel2.EventMessageType.CarbonMissionEnd:
					ValidTask();
					Invoke("tellPlayerAboutInfoNacl",4f);
					break;
					;
		 
				 
				case EventRelayLevel2.EventMessageType.DilutionEnd:
					ValidTask();
					EventRelayLevel2.RelayEvent(EventRelayLevel2.EventMessageType.BoilStart);
					break;
					;
				
				case EventRelayLevel2.EventMessageType.DilutionStart:
					
					Invoke("AskPlayertoDilution",8f);
					break;
					
				case EventRelayLevel2.EventMessageType.BoilStart:
					PlaySoundWithCallback(AboutBoil,StartBoiling);
		 
					break;
					;
				case EventRelayLevel2.EventMessageType.BoilEnd:
					ValidTask();
					PlaySoundWithCallback(GreatJob,StartAttack);
					Invoke("HideFromVirus",5f);
					break;
					;	
				case EventRelayLevel2.EventMessageType.VirusEnd:
					ValidTask();
					PlaySoundWithCallback(End,EndLEvel);
					break;
					;	
			}

		}

		return "";
	}

	private void EndLEvel()
	{
	SteamVR_LoadLevel.Begin("Main_Menu",true);
	}

	private void StartAttack()
	{
		PlaySoundOnshoot(Virus);
			EventRelayLevel2.RelayEvent(EventRelayLevel2.EventMessageType.VirusStart);
	}

	private void StartBoiling()
	{
    	 
	 
	}


	private void tellPlayerAboutInfoNacl()
	{
		EventRelayLevel2.RelayEvent(EventRelayLevel2.EventMessageType.InFoNacl);
		Debug.Log("Info ");
		PlaySoundWithCallback(Infonacl,DilutuonTask);
	}

	private void DilutuonTask()
	{
    
		EventRelayLevel2.RelayEvent(EventRelayLevel2.EventMessageType.DilutionStart);
	
	}

	private void AskPlayertoDilution()
	{
		PlaySoundOnshoot(AsktoDilutuon);
		
	}

	private void EnterPassword()
	{
		
		PlaySoundOnshoot(NeedPassword);
	}
	
	
	private void OnEnable()
	{
		EventRelayLevel2.OnEventAction += HandleEvent;
	 
	}
 

	private void OnDisable()
	{
		EventRelayLevel2.OnEventAction -= HandleEvent;

	}

	private void DoorIsOpenEnterTheLAb()
	{
		Debug.Log("Enerinng The Lab");
		PlaySoundWithCallback(PasswordEntred,MoveinsideTheLab);
		
	}

	private void MoveinsideTheLab()
	{
		
		var path = new GoSpline( "Robot_Get_iside_lab" );
		GoTween moveBlock =	Go.to( transform, RoborSpeed, new GoTweenConfig().positionPath( path,true).setEaseType( GoEaseType.CircOut).setDelay(0.2f));  
		moveBlock.setOnCompleteHandler(RobotiSInsideTheLAb
		);
		Debug.Log("Move inside henLab");
		
	}



	private void PlayerisInThelab()
	{
		
		PlaySoundWithCallback(TelltheplayerAboutSaifty,AskTosatrt);
		EventRelayLevel2.RelayEvent(EventRelayLevel2.EventMessageType.BeStafe);
		
	}

	private void AskTosatrt()
	{
PlaySoundWithCallback(LetStart,MovetoWorkTable);
	}

	private void MovetoWorkTable()
	{
	 PlaySoundWithCallback(YouCanStart,firstCompose);
	}

	private void firstCompose()
	{
	    PlaySoundOnshoot(AskForMakeCarbon);
		EventRelayLevel2.RelayEvent(EventRelayLevel2.EventMessageType.CarbonMission);
	}

	
	
	
	
	
	
	

	
	
	private void HideFromVirus()
	{
		
		var path = new GoSpline( "RobotHide_From_Virus" );
		GoTween moveBlock =	Go.to( transform, RoborSpeed, new GoTweenConfig().positionPath( path,false).setEaseType( GoEaseType.CircOut).setDelay(0.1f));  
		moveBlock.setOnCompleteHandler(GivethePlayerThe
		);
 
	}

	private void GivethePlayerThe(AbstractGoTween abstractGoTween)
	{
		StayScared();
		Debug.Log("Give the palyer the steak ");
	}


	private void RobotiSInsideTheLAb(AbstractGoTween abstractGoTween)
	{
Debug.Log("Player is Inside the Lab ");
	}
}
