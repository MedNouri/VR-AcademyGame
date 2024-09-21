using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class LunarModule : MonoBehaviour
{
	
	public List<MoonEventRelay.EventMessageType> EventsHandeld=
		new List<MoonEventRelay.EventMessageType>();
	public AudioClip EnginSound;
	private AudioSource _audioSource;
	public ParticleSystem FireSystem;
	public GameObject Off;
	public GameObject OpenDoorText;
	public Material OffMaterila;
	public GameObject Player;
	public GameObject Robot;
	public TextMesh TextMeshDispaly;

	public TeleportArea TeleportAreaLunar;

	// Use this for initialization
	void Start()
	{

		TextMeshDispaly.GetComponent<MeshRenderer>().enabled = false;
		TeleportAreaLunar.SetLocked(true);
		if (_audioSource == null)
		{
			_audioSource = GetComponent<AudioSource>();
		}

		Landing();
	}

	private bool _canStartEngin;

 
	public void ShutDownEngin()
	{
		MoonEventRelay.RelayEvent(MoonEventRelay.EventMessageType.ValidTask);

		_audioSource.Stop();
	}

	public void StartEngin()
	{
	}


	private void Landing()
	{
		var path = new GoSpline("Landing_Animation");
		MoonEventRelay.RelayEvent(MoonEventRelay.EventMessageType.Landing);
		GoTween moveBlock = Go.to(transform, 50f,
			new GoTweenConfig().positionPath(path,false).setEaseType(GoEaseType.CircOut));
		moveBlock.setOnCompleteHandler(c => EndLanding()
		);
	}


	private void EndLanding()
	{
		MoonEventRelay.RelayEvent(MoonEventRelay.EventMessageType.AskForStoppingTheEngin);
		Player.transform.SetParent(null);
		Robot.transform.SetParent(null);
		_canStartEngin = true;
		TeleportAreaLunar.SetLocked(false);
		Debug.Log("Landing");
		TextMeshDispaly.GetComponent<MeshRenderer>().enabled = true;

	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	private void OnEnable()
	{
		// Save PLayer PosTion 
		Scene scene = SceneManager.GetActiveScene();
		Debug.Log("Scne is "+scene.name);
		Game.WriteLevelPos( scene.name);
 
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
			Debug.Log("Level 2 M1 Event Recevide  ");
			switch (type)
			{
				case MoonEventRelay.EventMessageType.EnginShutDown:
					_audioSource.Stop();
					Off.GetComponent<Renderer>().material = OffMaterila;
					break;
				case MoonEventRelay.EventMessageType.DoorOpen:
				 
				OpenDoorText.GetComponent<Renderer>().material = OffMaterila;
					break;


			}
		}

		return "";



	}
}