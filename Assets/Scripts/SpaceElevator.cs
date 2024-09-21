using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceElevator : MonoBehaviour
{


	public List<MoonEventRelay.EventMessageType> EventsHandeld =
		new List<MoonEventRelay.EventMessageType>();

	[Header("Audio Clips")] public AudioClip ElevatoAudioClipr;

	private AudioSource AudioSourceElevetor;
	public Transform FloorOnePostion;
	public Transform FloorZeroPostion;


	public GameObject Robot;
	public GameObject player;

	private void OnEnable()
	{
		MoonEventRelay.OnEventAction += HandleEvent;

	}


	private void OnDisable()
	{
		MoonEventRelay.OnEventAction -= HandleEvent;

	}


	private void Start()
	{
		AudioSourceElevetor = GetComponent<AudioSource>();

	}

	string HandleEvent(MoonEventRelay.EventMessageType type)
	{

		if (EventsHandeld.Contains(type))
		{

			switch (type)
			{
				case MoonEventRelay.EventMessageType.EnimesAttackEnd:

					StartCoroutine(GetUp());
					break;



			}

		}

		return "";
	}

	private IEnumerator GetUp()
	{

		yield return new WaitForSeconds(2f);
		AudioSourceElevetor.clip = ElevatoAudioClipr;
		AudioSourceElevetor.Play();
		AudioSourceElevetor.loop = true;

		Go.to(transform, 10f, new GoTweenConfig().position(FloorOnePostion.position).setEaseType(GoEaseType.CircInOut)
		).setOnCompleteHandler(c => GetInDestitionUp());
	}



	private void GetInDestitionUp()
	{
		Debug.Log("Elevatro is Up ");
		AudioSourceElevetor.Pause();
	}


	private IEnumerator GetDown()
	{
		yield return new WaitForSeconds(3f);
		AudioSourceElevetor.clip = ElevatoAudioClipr;
		AudioSourceElevetor.Play();
 
		Go.to(transform, 20f, new GoTweenConfig().position(FloorZeroPostion.position)
		).setOnCompleteHandler(c => GetInDestitionDown());
	
	}



	private void GetInDestitionDown()
	{
Debug.Log("Player is out");		
		Robot.transform.SetParent(null);
		player.transform.SetParent(null);
		MoonEventRelay.RelayEvent(MoonEventRelay.EventMessageType.GobackToSapceShip);
		gameObject.GetComponent<Collider>().enabled = false;

		AudioSourceElevetor.Pause();
	}

	bool isaActionDone;
	bool Robotishere;

	private void OnTriggerEnter(Collider other)
	{
		

 
			Robotishere = true;
			Debug.Log("Robot is in My ");
	 
		if (other.gameObject.transform.root.CompareTag("Player"))
		{

			if ((!isaActionDone)&&(Robotishere))
			{
				isaActionDone = true;
				Robot.transform.SetParent(gameObject.transform);
				player.transform.SetParent(gameObject.transform);
		    	StartCoroutine(	GetDown());
			}


		}


	}

}
