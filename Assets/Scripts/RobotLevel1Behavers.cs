using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RobotLevel1Behavers : Robot
{
    private RobotNextMession _robotNextMession;
    private GoTween _tween;
 


    public List<EventRelayFirstLevel.EventMessageType> EventsHandeld =
        new List<EventRelayFirstLevel.EventMessageType>();
    [Header("Audio Clips ")]
 


   
    public AudioClip HelloIntoTheMission;
    public AudioClip GetIntoTheEevator;
    public AudioClip PressTheButtonAudio;
    public AudioClip TheFirstPersonAudioClip;
    public AudioClip GetoutOfSpaceShip;
    public TeleportArea ElevatorArea;
    
    public AudioClip FindTheKey;


    public AudioClip Setdestionation;

    public AudioClip PressToGo;
    public AudioClip CountDown;
    
    private void OnEnable()
    {
        EventRelayFirstLevel.OnEventAction += HandleEvent;
    }


    private void OnDisable()
    {
        EventRelayFirstLevel.OnEventAction -= HandleEvent;
    }


    private string HandleEvent(EventRelayFirstLevel.EventMessageType type)

    {
        if (EventsHandeld.Contains(type))
        {
            Debug.Log("Event Is recived Robot leve1 " + type);

            switch (type)
            {
                case EventRelayFirstLevel.EventMessageType.GetintoElevator:
                    GetIntoElevtorMethode();
                    break;
                case EventRelayFirstLevel.EventMessageType.GetOutOfElvator:
                    StartCoroutine(GetoutTheElevtor());
                    break;
                case EventRelayFirstLevel.EventMessageType.LookForKey:
                    LookFortheKey();
                    break;
                case EventRelayFirstLevel.EventMessageType.KeyFound:
                    ValidTask();
                    Invoke("SetDistination", 5f);
                    break;
                case EventRelayFirstLevel.EventMessageType.DestinationSet:
                    ValidTask();
                    Invoke("PressTheButton", 2f);
                    break;
                case EventRelayFirstLevel.EventMessageType.StartTheEngin:
                    ValidTask();
                    Invoke("PressTheButton", 2f);
                    break;
                case EventRelayFirstLevel.EventMessageType.EnginStarted
                    :
                    ValidTask();
                    Invoke("CountDownStart", 5f);
                    break;
                case EventRelayFirstLevel.EventMessageType.Talkinfo1:


                    PlaySoundWithCallback(PressTheButtonAudio, TalkInfoOne);
                    break;
                case EventRelayFirstLevel.EventMessageType.Talkinfo2
                    :
                    PlaySoundOnshoot(TheFirstPersonAudioClip);

                    break;

                case EventRelayFirstLevel.EventMessageType.ValidTask:
                    ValidTask();
                    break;
            }
        }

        return " ";
    }

    private void SetDistination()
    {
        PlaySoundWithCallback(Setdestionation, WaitingforavalidDestination);
    }


    private void CountDownStart()
    {
        PlaySoundWithCallback(CountDown, loadnextLevel);
    }

    private void TalkInfoOne()
    {
        Debug.Log("Info one");
        //	PlaySoundOnshoot(InfoAboutThemoon);
    }


    private void PressTheButton()
    {
        Debug.Log("Mission ending 1 ");
        PlaySoundWithCallback(PressToGo, Waitingforavalidbutton);
    }


    private void loadnextLevel()
    {
       Debug.Log("Mission ending  2 ");
        //EventRelayFirstLevel.RelayEvent(EventRelayFirstLevel.EventMessageType.LaodLevelM2);
    }


    private void WaitingforavalidDestination()
    {
        EventRelayFirstLevel.RelayEvent(EventRelayFirstLevel.EventMessageType.SetDestination);
    }

    private void Waitingforavalidbutton()
    {
        Debug.Log("wwaiting for the button");
    }


    private void Start()
    {
        Debug.Log("Hello M1_");
        PlaySoundWithCallback(HelloIntoTheMission, GetIntoElevtorMethode);
    }


    private void openelevator()
    {
        EventRelayFirstLevel.RelayEvent(EventRelayFirstLevel.EventMessageType.OpenElevator);
       
    }


    private void GetIntoElevtorMethode()
    {
      


        // or the target can be set to look at another transform
        var path = new GoSpline("Robot_Go_To_Elevator");
        _tween = Go.to(transform, RoborSpeed, new GoTweenConfig()
            .positionPath(path, true)
            .setIterations(1).onComplete(tween => AllowTeleport()));


        PlaySoundOnshoot(GetIntoTheEevator);
        Invoke("openelevator", 3f);

    }

    private void AllowTeleport( )
    {
        ElevatorArea.SetLocked(false);
    }
 

    private IEnumerator GetoutTheElevtor()
    {
        Debug.Log("player is here");
        EventRelayFirstLevel.RelayEvent(EventRelayFirstLevel.EventMessageType.ValidTask);

        yield return new WaitForSeconds(4f);
        PlaySoundOnshoot(GetoutOfSpaceShip);
        yield return new WaitForSeconds(2f);
        Debug.Log("Robot_should Move ");
        transform.SetParent(null);
        Target.transform.SetParent(null);
        var path = new GoSpline("Robot_outof_Elevator");
        _tween = Go.to(transform, RoborSpeed, new GoTweenConfig()
            .positionPath(path, true)
            .setIterations(1).onComplete(tween => waitForThePlayerTogetIn()));

        yield return new WaitForSeconds(2f);

        EventRelayFirstLevel.RelayEvent(EventRelayFirstLevel.EventMessageType.OpenSpaceship);
    }

    private void LookFortheKey()
    {
        PlaySoundWithCallback(FindTheKey, waitingForthePlayertoFinedthekKey);
    }


    private void SetThdetation()
    {
    }


    private void waitForThePlayerTogetIn()
    {
        Debug.Log("Waiting For the Player to get here");
    }


    private void waitingForthePlayertoFinedthekKey()
    {
        Debug.Log("Waiting For the Player to findthekey");
    }

    private void TalkAboutTheSpace()
    {
    }

    private void talkAboutTheSpaceship()
    {
    }

    private delegate void RobotNextMession();
}