using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyholder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		collider = GetComponent<Collider>();
		isActionPerformed = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private bool isActionPerformed;

	private Collider collider;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Key")
		{
			if (isActionPerformed)
			{
				DestroyImmediate(collider);
				EventRelayFirstLevel.RelayEvent(EventRelayFirstLevel.EventMessageType.KeyFound);
				isActionPerformed = false;

			}
			else
			{
				
				Debug.Log("Event is Already sent :p ");
			}


		}
	}
}
