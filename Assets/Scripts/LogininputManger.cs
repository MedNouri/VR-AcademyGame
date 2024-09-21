using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogininputManger : MonoBehaviour
{
    
        public List<LoginEventRelay.EventMessageType> EventsHandled=
        new List<LoginEventRelay.EventMessageType>();
    
    public InputField NameField;
    public InputField AgeField;
    public GameObject AgeFieldGameObject; 
    public GameObject NameFieldGameObject;
    private InputField currentFiled;

    private bool NameIsDone;
    private bool AgeIsDone;
    private bool Nameinput;

    private void OnEnable()
    {
        LoginEventRelay.OnEventAction += HandleEvent;
	 
    }
 

    private void OnDisable()
    {
        LoginEventRelay.OnEventAction -= HandleEvent;
	 
    }

    string HandleEvent(LoginEventRelay.EventMessageType type)
    {

        if (EventsHandled.Contains(type))
        {
            Debug.Log("Dispaly HAndel Evtn revived ");
            switch (type)
            {
                case LoginEventRelay.EventMessageType.FillName:
                   //start name 
                    break;


            }

        }

        return "";
    }



   

  
}
