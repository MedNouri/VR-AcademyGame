using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScreen : MonoBehaviour
{

	public List<LoginEventRelay.EventMessageType> EventsHandled=
		new List<LoginEventRelay.EventMessageType>();

        
	public Texture ValidInputTexture ;
	public Texture InvalidInputTexture ;
	public Texture WhatisVrAcademyTexture;
	public Texture YourNameTexture;
	public Texture FilltheFormTexture ;
	public Texture LooksGoodTexture ;
 
	public GameObject TheBigDisplay;
 
	public Texture BigDispalyYourNameInput;
	public Texture BigDispalyAboutVracademy;
	
 
	private Renderer _mRenderer;
	private Renderer D_Renderer;


	
	
	
	
	
	
	
	
	
	
	
	
	private void OnEnable()
	{
		LoginEventRelay.OnEventAction += HandleEvent;
	 
	}
 

	private void OnDisable()
	{
		LoginEventRelay.OnEventAction -= HandleEvent;
	 
	}

	void Start()
	{
		//Fetch the Renderer from the GameObject
		_mRenderer = GetComponent<Renderer>();
	  	D_Renderer = TheBigDisplay.GetComponent<Renderer>();

	}


	string HandleEvent(LoginEventRelay.EventMessageType type)
	{

		if (EventsHandled.Contains(type))
		{
			Debug.Log("Dispaly HAndel Evtn revived ");
			switch (type)
			{
				case LoginEventRelay.EventMessageType.ErrorInput:
					invalidinput();
					break;


				case LoginEventRelay.EventMessageType.Validinput:
				  validinput();
					break;

				case LoginEventRelay.EventMessageType.FillTheForm:
					PleasFillThForm();
				 
					break;
				
				case LoginEventRelay.EventMessageType.RobotFinishTalking:
				
					WhatisVrAcademy();
					break;
				
				case LoginEventRelay.EventMessageType.FormCompleted:
				
				Invoke("FormCompleted",2f);
					break;


				
			}

		}

		return "";
	}


	private void FormCompleted()
	{
		
		// save The Name In the DATA BAse
		
		_mRenderer.material.SetTexture("_MainTex",LooksGoodTexture);
		D_Renderer.material.SetTexture("_MainTex",LooksGoodTexture);
		
	}

	void WhatisVrAcademy()
	{
		
		Splach();
		_mRenderer.material.SetTexture("_MainTex",WhatisVrAcademyTexture);
		D_Renderer.material.SetTexture("_MainTex",BigDispalyAboutVracademy);
		
	}

	
	
	private void Splach()
	{
	}

	void PleasFillThForm()
	{
		Splach();
		Debug.Log("Event Recived Filll the Form bitch ");
		D_Renderer.material.SetTexture("_MainTex",BigDispalyYourNameInput);
		_mRenderer.material.SetTexture("_MainTex",FilltheFormTexture);
	}



  
 


	void invalidinput()
	{

		//Set the Texture
		_mRenderer.material.SetTexture("_MainTex", InvalidInputTexture);
		Debug.Log(" invalid input ");
	}


 

	void validinput()
	{

		//Set the Texture
		_mRenderer.material.SetTexture("_MainTex", ValidInputTexture);
		Debug.Log(" valid input ");
	}

}
