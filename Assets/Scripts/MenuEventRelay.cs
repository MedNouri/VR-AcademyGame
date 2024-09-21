using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEventRelay : MonoBehaviour
{

    public delegate string EventAction(EventMessageType type);
    
    public static event EventAction OnEventAction;
    
    public enum  EventMessageType
    {
        Lostfocus,
        CdSpace,
        Cd360,
        Cdchimical,
        CdOpen,
        Cdclose,
        Exit,
       
        LoadScene,
        NoCd,
    }

    public static string RelayEvent(EventMessageType eventType)
    {
      
        return OnEventAction(eventType);
    }


}
