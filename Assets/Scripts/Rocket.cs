using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{


	public List<EventRelayFirstLevel.EventMessageType> EventsHandeld =
		new List<EventRelayFirstLevel.EventMessageType>();


	public ParticleSystem LaunchFire;
	private ParticleSystem _CachedSystem;

	public ParticleSystem  launchSmoke;
	public AudioClip LaunchSound;
 
	private bool firsttime;
	private AudioSource _audioSource;
	public TextMesh SpaceShipSpeed;
	public TextMesh SpaceShiptxtMesh;
	private bool isActionPerformed;
	private bool isActionPerformedstart;
	public GameObject Robot;
	public GameObject PlayerGameObject;
	private int numberOfAction = 3; 

	// Use this for initialization
	void Start()
	{
		//
		_audioSource = GetComponent<AudioSource>();
		isActionPerformed = false;
		isActionPerformedstart = false;
		launchSmoke.Pause();
		launchSmoke.Pause();
	 
	}
 

	void  StartEngin()
	{
		Fluy();

	}

	private bool isMoving;


	public void Fluy()
	{
		isMoving = true;
		transform.positionTo(60f, new Vector3(transform.position.x, 500f, transform.position.z)).setOnCompleteHandler(c=> loadLevel());
	}

	private void loadLevel()
	{
		Scene scene = SceneManager.GetActiveScene();
		Game.WriteLevelPos( scene.name);
		SteamVR_LoadLevel.Begin("Level1_M2");
		 
		
	}
	void closeDoor()
	{
	}

 

	private void OnEnable()
	{

		EventRelayFirstLevel.OnEventAction += HandleEvent;
	}



	private void OnDisable()
	{

		EventRelayFirstLevel.OnEventAction -= HandleEvent;
	}



	private IEnumerator LaunchAnimation()
	{
		Debug.Log("player rocket will lunched");
	
		yield return new WaitForSeconds(4f);
		_audioSource.clip = LaunchSound;
		_audioSource.Play();
		_audioSource.volume = 100f;
		yield return new WaitForSeconds(5f);
		launchSmoke.Play();
		yield return new WaitForSeconds(40f);
	   LaunchFire.Play();
	 
		yield return new WaitForSeconds(28f);
		Fluy();
	}
	
	
	
	string HandleEvent(EventRelayFirstLevel.EventMessageType type)

	{

		if (EventsHandeld.Contains(type))
		{
			Debug.Log("Event Is recived Robot leve1 " + type);

			switch (type)
			{
				 
				case EventRelayFirstLevel.EventMessageType.EnginStarted:
					numberOfAction--;
					if (numberOfAction==0)
					{
						
					
					StartCoroutine(LaunchAnimation());
						SpaceShiptxtMesh.text = "SPEED ";
					Robot.transform.SetParent(this.transform);
					PlayerGameObject.transform.SetParent(this.transform);
					}
						break;
				case EventRelayFirstLevel.EventMessageType.LaodLevelM2:
					StartEngin();
					
					break;
				case EventRelayFirstLevel.EventMessageType.KeyFound:
					SpaceShiptxtMesh.text = "Key is Found";
					numberOfAction--;
					break;
				case EventRelayFirstLevel.EventMessageType.DestinationSet:
					SpaceShiptxtMesh.text = "MOON -> ";
					numberOfAction--;
					break;
				case EventRelayFirstLevel.EventMessageType.SetDestination:
					SpaceShiptxtMesh.text = "EARTH -> ";
					
					break;
			}
		}

		return"";
	}

	private int speed=0;
	private void Update()
	{
		if (isMoving)
		{
			SpaceShipSpeed.text = (speed ++).ToString();

		}
		}
}

