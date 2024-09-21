using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RadioLever : MonoBehaviour {
	public LinearMapping linearMapping;
	private float currentLinearMapping = float.NaN;
	public GameObject ONText;
	public Material ONfMaterila;

	
	private bool isActionMade;
	private void SignalFixed()
	{
		MoonEventRelay.RelayEvent(MoonEventRelay.EventMessageType.SignalFixed);
		ONText.GetComponent<Renderer>().material = ONfMaterila;
	}

	void Update()
	{
		if (currentLinearMapping != linearMapping.value)
		{
			currentLinearMapping = linearMapping.value;
			if (currentLinearMapping ==1f)
			{
				if (!isActionMade)
				{
				SignalFixed();
					isActionMade = true;
				}
			}

			{
				
			}
		}
	}
}
