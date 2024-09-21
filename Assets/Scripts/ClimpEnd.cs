using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimpEnd : MonoBehaviour {
	bool isaActionDone;

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Robot is Uo");

		if (other.gameObject.transform.root.CompareTag("Player"))
		{

			if (!isaActionDone)
			{
				isaActionDone = true;

				Debug.Log("Robot Get Up");
				MoonEventRelay.RelayEvent(MoonEventRelay.EventMessageType.ClambEnd);
				gameObject.GetComponent<Collider>().enabled = false;
				Destroy(gameObject);

			}
		}

	}

}
