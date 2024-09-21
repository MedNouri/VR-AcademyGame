using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacshipKnob :MonoBehaviour  {
   
   private	bool isActionPerformed;
    private bool CanStart;
   public GameObject MoonText;
    public Material ValidMoon;
 
    public List<EventRelayFirstLevel.EventMessageType> EventHanded = new List<EventRelayFirstLevel.EventMessageType>();
    private void OnEnable()
    {

        EventRelayFirstLevel.OnEventAction += HandleEvent;
    }



    private void OnDisable()
    {

        EventRelayFirstLevel.OnEventAction -= HandleEvent;
    }


    private void Start()
    {
        GetComponent<Collider>().enabled = false;
    }

    string HandleEvent(EventRelayFirstLevel.EventMessageType type)

    {

        if (EventHanded.Contains(type))
        {
            switch (type)
            {
                case EventRelayFirstLevel.EventMessageType.SetDestination:
                    CanStart = true;
                    GetComponent<Collider>().enabled = true;

                    break;
		 
            }
        }

        return null;
    }


  public void  OnSetDestination()
   {
       if (CanStart)
       {

   
      if (!isActionPerformed)
      {
          EventRelayFirstLevel.RelayEvent(EventRelayFirstLevel.EventMessageType.DestinationSet);
         EventRelayFirstLevel.RelayEvent(EventRelayFirstLevel.EventMessageType.StartTheEngin);
  
         MoonText.GetComponent<Renderer>().material = ValidMoon;
         isActionPerformed=true;
      }
       }
 
   }


}
