using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;

//======= Copyright (c) VRACADEMY 2018 , All rights reserved. ===============

public class EnimeLevel2 : MonoBehaviour {
	private NavMeshAgent agent;

	public bool test;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	private void NewDestination()
	{
		Vector3 newDest =Random.insideUnitSphere *    100f +Player.instance.hmdTransform.position;
		NavMeshHit hit;
		bool hasDestination = NavMesh.SamplePosition(newDest, out hit, 100f, 1);
		
		if (hasDestination)
		{
			agent.SetDestination(hit.position);
		}
	}

	void Update() {
		if (test)
		{
			Death();
		}
 
		 
		 
 

		if(!agent.pathPending){

			if (agent.remainingDistance <= agent.stoppingDistance)
			{
				if (!agent.hasPath ||agent.velocity.sqrMagnitude== 0)
				{
					
					NewDestination();
				
				}

				
			}
		}	
	}


	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name=="Weapen")
		{
			Debug.Log("i will be dead soon");
			//Death();
		}
	}

	private void Death()
	{
		gameObject.GetComponent<NavMeshAgent>().enabled = false;
 
		gameObject.GetComponent<Rigidbody>().useGravity=true;
		agent.enabled = false;
		transform.RotateAround(Vector3.zero, Vector3.up, 80 * Time.deltaTime);
		Destroy(gameObject,5f);
		EventRelayLevel2.RelayEvent(EventRelayLevel2.EventMessageType.VirusDead);


	}
}
