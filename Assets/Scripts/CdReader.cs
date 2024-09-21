using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PostProcessing;
using Valve.VR.InteractionSystem;

public class CdReader : SnapToPostion

	
{
	private bool _open;
	private bool _isMoving;
	private Vector3 _closingPostion;
	private Vector3 _curentPostion;
	public  Vector3 _openingPostion;
 
	private bool _hasEntered;


	private AudioSource _playerSound;


	private void OnDisable()
	{
	 ObjectSnappedToDropZone-= SnapToPostionOnObjectSnappedToDropZone;
	 ObjectExitedSnapDropZone-= SnapToPostionOnObjectExitedSnapDropZone;
	 ObjectUnsnappedFromDropZone-= SnapToPostionOnObjectUnsnappedFromDropZone;

	}


	private void Start()
	{
		_playerSound = GetComponent<AudioSource>();
	}


	private void OnEnable()
	{
		_closingPostion = transform.localPosition;
	 ObjectSnappedToDropZone+= SnapToPostionOnObjectSnappedToDropZone;
	 ObjectExitedSnapDropZone+= SnapToPostionOnObjectExitedSnapDropZone;
 ObjectUnsnappedFromDropZone+= SnapToPostionOnObjectUnsnappedFromDropZone;
	 
		_open = false;
		_isMoving= false;
	}

	private bool CdisInTheReader;
	private void SnapToPostionOnObjectSnappedToDropZone()
	{
		CdisInTheReader = true;
	}

	private void SnapToPostionOnObjectUnsnappedFromDropZone()
	{
		if (CdisInTheReader)
		{
			Debug.Log("Cd Event Recived");

			CdisInTheReader = false;
			if (ObjectName!=null)
			{
				
			
				CdEvnetSender(ObjectName);
			}}
	
	}

	
	private void SnapToPostionOnObjectExitedSnapDropZone()
	{
		 
		MenuEventRelay.RelayEvent(
			MenuEventRelay.EventMessageType.NoCd);
	}
 
	
	
	private static void CdEvnetSender(string cdName)
	{
		
		if (cdName=="CD_SPACE")
		{
			 
	     		MenuEventRelay.RelayEvent(
				MenuEventRelay.EventMessageType.CdSpace);
			    Debug.Log("Cd Event Sent cdCD_SPACE");
			
		}else if (cdName == "CD_360")
		{
			    MenuEventRelay.RelayEvent(
				MenuEventRelay.EventMessageType.Cd360);
			    Debug.Log("Cd Event Sent  Sent 360");
		}
		else if (cdName == "CD_CHIMICAL")
		{
			MenuEventRelay.RelayEvent(
				MenuEventRelay.EventMessageType.Cdchimical);
			Debug.Log("Cd Event Sent CHIMICAL");
		}
		
	}


	IEnumerator CloseCd()
	{		 
		_playerSound.Play();
		Go.to(transform, 0.4f, new GoTweenConfig().localPosition(_closingPostion));
	    yield return new WaitForSeconds(0.4f);
	 
		MenuEventRelay.RelayEvent(
	   MenuEventRelay.EventMessageType.Cdclose);
		ChechForCd();

	}

	private void ChechForCd()
	{
		if ( IsObjectStillOnDropZone())
		{
					if (ObjectName!=null)
            			{
            				
            			
            				CdEvnetSender(ObjectName);
            			}
		}
	}

	IEnumerator OpenCd()
	{	_playerSound.Play();
		CdisInTheReader = false;

		Go.to(transform, 0.4f, new GoTweenConfig().localPosition(_openingPostion));
		yield return new WaitForSeconds(0.4f);
	 
		MenuEventRelay.RelayEvent(
	    MenuEventRelay.EventMessageType.CdOpen);

	}

	
	  private void OnHandHoverEnd(Hand hand)
        {

			hand.HoverUnlock(GetComponent<Interactable>());

        }
	
	
	public void OpenClose()
	{
 
		MenuEventRelay.RelayEvent(
			MenuEventRelay.EventMessageType.Lostfocus);

	 	if (_open)
			{

		  
				StartCoroutine(CloseCd());
			}
			else
		 {


			 StartCoroutine(OpenCd());

		 }

		 
	}


	private void FixedUpdate()
	{

		if (transform.localPosition == _openingPostion)
		{
			_open = true;
			StartDetection = true;
		}
		else if (transform.localPosition==_closingPostion)
		
		{

			_open = false;
			StartDetection = false;
		}


 
	}

 

}
