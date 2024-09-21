using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AlienAttack : MonoBehaviour {

	public List<MoonEventRelay.EventMessageType> EventsHandeld=
		new List<MoonEventRelay.EventMessageType>();

	[SerializeField]
	public  int NumberOfAlienS;
	private int NumberOfDead;
	public GameObject AlienAgent;
	
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
		 
	 
	}

	

  private 	IEnumerator StartAttack()
	{
		
		for (int i = 0; i < NumberOfAlienS; i++)
		{
			GameObject aliGameObject = Instantiate(AlienAgent, transform.position, transform.rotation) as GameObject;
			yield return new WaitForSeconds(1f);
		}

	}

	string HandleEvent(MoonEventRelay.EventMessageType type)
	{

		if (EventsHandeld.Contains(type))
		{
	 
			switch (type)
			{
				case MoonEventRelay.EventMessageType.EnemiesAttackStart:

					StartCoroutine(StartAttack());
					break;
				case MoonEventRelay.EventMessageType.EnemiGotKilled:
					NumberOfAlienS--;
					Debug.Log("IgoT killed"+NumberOfAlienS);
					if (NumberOfAlienS<=0)
					{
						Debug.Log("End Mission"+NumberOfAlienS);
						MoonEventRelay.RelayEvent (MoonEventRelay.EventMessageType.EnimesAttackEnd);
					}
		 
					break;
	 

			}

		}

		return "";
	}
}
