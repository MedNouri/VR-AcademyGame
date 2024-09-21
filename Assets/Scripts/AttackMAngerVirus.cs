using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMAngerVirus : MonoBehaviour {

    public  int NumberOFVirus;
    private int NumberOfDead;
    public GameObject VirusAgent;
    
    public List<EventRelayLevel2.EventMessageType> EventsHandeld=
        new List<EventRelayLevel2.EventMessageType>();
 
 
    private void OnEnable()
    {
	
        EventRelayLevel2.OnEventAction += HandleEvent;
	 
    }
 

    private void OnDisable()
    {
        EventRelayLevel2.OnEventAction -= HandleEvent;
	 
    }
 
 
    string HandleEvent(EventRelayLevel2.EventMessageType type)
    {
        if (EventsHandeld.Contains(type))
        {
           
            switch (type)
            {
                case EventRelayLevel2.EventMessageType.VirusStart:
                    StartCoroutine(StartAttack());
                    break;
                    ;
                case EventRelayLevel2.EventMessageType.VirusDead:
                   NumberOFVirus--;
                    Debug.Log("IgoT killed"+NumberOFVirus);
                    if (NumberOFVirus<=0)
                    {
                        Debug.Log("End Mission"+NumberOFVirus);
     
                    }

                    
                    break;
                    ;
                    
            }
        }

        return null;
    }
    
    
    private 	IEnumerator StartAttack()
    {
		
        for (int i = 0; i < NumberOFVirus; i++)
        {
            GameObject aliGameObject = Instantiate(VirusAgent, transform.position, transform.rotation) as GameObject;
            yield return new WaitForSeconds(15f);
        }

    }


    private void Start()
    {
    StartCoroutine(StartAttack());
    }
}
