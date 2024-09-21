using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Valve.VR;

public class PCDispaly : MonoBehaviour
{
	public AudioClip leve1;
	public  AudioClip leve2;
	public Sprite m_LevelTexture;
	public Sprite m_LevelTexture_StartTheGame;
	public Sprite m_Leve2Texture;
	public Sprite m_Leve2Texture__StartTheGame;
	public Sprite m_360VideosTexture;
	public Sprite DesktopTexture;
  
	 
 
	private Image ImageView;
 
	public  GameObject level1Ui;
	public  GameObject level2Ui;
	 
	public GameObject video360ui;
	private bool exit;

	private bool CdReaderclose;
 
	public Image m_Image;
	public List<MenuEventRelay.EventMessageType> evntsHnded=
		new List<MenuEventRelay.EventMessageType>();

	
 

	void Start()

	{
		m_Image = m_Image.GetComponent<Image>();
		
Desktop();
	}

	private void OnEnable()
	{
	  		MenuEventRelay.OnEventAction += HandelEvent;
	 
	}

	private void OnDisable()
	{
	 
		MenuEventRelay.OnEventAction -= HandelEvent;
	}

 

	string HandelEvent(MenuEventRelay.EventMessageType type)
	{
		if (evntsHnded.Contains(type))
	
		{	 
			 
			if (type==MenuEventRelay.EventMessageType.Cd360)
			{
				exit = false;
				 
				Debug.Log("360 menu");
			
				Load360Stage();
			
			}else if (type == MenuEventRelay.EventMessageType.Cdchimical)
			{
				exit = false;
	 
				 
				Debug.Log("chimical menu ");
				Loadstage2();
			
			}
			else if (type == MenuEventRelay.EventMessageType.CdSpace)
			{
				exit = false;
				Loadstage1();
				 
				Debug.Log("space menu ");
			}
			else if (type == MenuEventRelay.EventMessageType.CdOpen)
			{
				exit = true;
				Desktop();	
			}else if (type == MenuEventRelay.EventMessageType.Cdclose)
			{
				exit = true;
				Desktop();	
			}
			else if (type == MenuEventRelay.EventMessageType.NoCd)
			{
				exit = true;
			 
				Desktop();	
 
			}
		}
	 
		return "Event was called ";
	}
	
	
	
 

	private void Desktop()
	{
 
		Reset();
		SetSprite(DesktopTexture);
		
	}



	private void Load360Stage()
	{
		StartCoroutine(FakeLoading(m_360VideosTexture,m_360VideosTexture));
		Invoke("Load360ui",9);
		
	}

	private  void Load360ui()
	{
		if (!exit)
		{

			video360ui.SetActive(true);
		}
	}



	private void Loadstage1()
	{
	 

		StartCoroutine(FakeLoading(m_LevelTexture,m_LevelTexture_StartTheGame));
	 
	Invoke("LoadLevel1ui",9);
	}

	
	private void Loadstage2()
	{
		 
	 
		StartCoroutine(FakeLoading(m_Leve2Texture,m_Leve2Texture__StartTheGame));
		Invoke("LoadLevel2ui",9);
	}

	
	
 

	IEnumerator FakeLoading(Sprite spriteLaoding ,Sprite spritMenu)
	{		if (!exit)
		{

		
		Desktop();

			yield return new WaitForSeconds(3f);
			//	_BlinkText.SetActive(true);
		}

		if (!exit)
		{
			
	
         SetSprite(spriteLaoding);
	
		yield return  new WaitForSeconds(6f);
		}
	//	_BlinkText.SetActive(false); 
		if (!exit)
		{
			SetSprite(spritMenu);
			yield return new WaitForSeconds(1f);
		}

		Debug.Log("done");
		
	}

	private void LoadLevel1ui()
	{
		if (!exit)
		{
			level1Ui.SetActive(true);
		}
	}
	

	private void LoadLevel2ui()
	{
		if (!exit)
		{
			level2Ui.SetActive(true);
		}
	}

	private void Reset()
	{
    	m_Image.sprite = null;
    	level1Ui.SetActive(false);
		level2Ui.SetActive(false);
		video360ui.SetActive(false);
		level1Ui.SetActive(false);
	}

	void SetSprite(Sprite m_Sprite)
	{
	 	m_Image.sprite = null;
 
			m_Image.sprite = m_Sprite;
 
 
 
		
	}



	public void LoadGameLevelOne()
	{
		Debug.Log("Level is "+Game.ReadPos());
		if (Game.ReadPos()!=null)
		{
			SteamVR_LoadLevel.Begin(Game.ReadPos(),true,0.5f,0.1f,0.5f,0.2f,1f);
		}
		else
		{
			Debug.Log("Level is "+Game.ReadPos());
		}
		
	}

	public void NewGameLevelOne()
	{
		LoadLevelAction("Level1_M1");
	}
	public void LoadGameLeveltwo()
	{
	}

	public void NewGameLeveltwo()
	{
		LoadLevelAction("Level2_M1");
	}
	
	
	
		
	void LoadLevelAction(String levelName)
	{
		
		SteamVR_LoadLevel.Begin(levelName);
		
	}

	
	void LoadLevelAction(String levelName, String leveltask)
	{
		
		SteamVR_LoadLevel.Begin(levelName,true,0.5f,0.1f,0.5f,0.2f,1f);
		
		
	}


	 }
