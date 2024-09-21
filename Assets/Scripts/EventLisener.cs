using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class EventLisener : MonoBehaviour
{

	private bool showmessge = false;
	private void OnEnable()
	{
		ExceptionGenertor.OnEventAction += EventEespaonse;
	}

	private void OnDestroy()
	{
		ExceptionGenertor.OnEventAction -= EventEespaonse;
	}


	private void OnDisable()
	{
	
		ExceptionGenertor.OnEventAction -= EventEespaonse;
	}
	
	
	void EventEespaonse()
	{
		ShowGui();

	}

	void ShowGui()
	{

		StartCoroutine(TimedMessage());
	}


	IEnumerator TimedMessage()
	{
		showmessge = true;
		yield return new WaitForSeconds(10f);
		showmessge = false;


	}

	private  void OnGUI()
	{
		if (showmessge)
		{
			GUI.Label(new Rect(100,100,300,20),"HelloFrom Gui");
		}
	}
}
