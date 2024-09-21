using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ScreenSaver : MonoBehaviour {

	public List<MenuEventRelay.EventMessageType> evntsHnded=
		new List<MenuEventRelay.EventMessageType>();

	private VideoPlayer _videoPlayer;
	private MeshRenderer _meshRenderer;
	private bool _lostFocus;

	private void Start()
	{
	 
		_videoPlayer.Play();
		_meshRenderer.enabled= true;
	}


	private void OnEnable()
	{
		MenuEventRelay.OnEventAction += HandelEvent;
	   	_videoPlayer = GetComponent<VideoPlayer>();
		_meshRenderer = GetComponent<MeshRenderer>();
	}

	private void OnDisable()
	{
	 
		MenuEventRelay.OnEventAction -= HandelEvent;
	}




	string HandelEvent(MenuEventRelay.EventMessageType type)
	{
	 
		if (evntsHnded.Contains(type))

		{
			if (type==MenuEventRelay.EventMessageType.Lostfocus)
			{
		 
			  StartCoroutine(	ScreenSaverPlay());
				_lostFocus = true;
			}
			
			
		}

		return null;
	}
	
	
	
	private IEnumerator ScreenSaverPlay()
	{
		 
		_videoPlayer.Stop();
		_meshRenderer.enabled= false;
		 
		yield return  new WaitForSeconds(20f);
		if (!_lostFocus)
		{

			_videoPlayer.Play();
			_meshRenderer.enabled= true;
			
		}

		 
	}
	
 

}
