 
using System.Collections.Generic;
 
using UnityEngine;
 
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
 

//======= Copyright (c) VRACADEMY 2018 , All rights reserved. ===============

/// <summary>
/// // A Login MAnger Scene that Mange The game Play Flow .
/// </summary>
public class LoginManger : MonoBehaviour
{
    /// <summary>
    /// The current hinge.
    /// </summary>
    /// GameObject ExitCanves
    public GameObject ExitCanves;

    public GameObject Keyboard;
    public Canvas InputFiledCanvas;
    public InputField NameInputField;

    public List<LoginEventRelay.EventMessageType> EventsHandled =
        new List<LoginEventRelay.EventMessageType>();

 
    public PlayerData PlayerData;

    private bool isActionMAdeForm;


    void OnLevelWasLoaded(int level) {
      
        
    }
    void Awake()
    {
        if (PlayerData.CheckData())
        {
         
      //   Utility.LoadLevel(Utility.Menu);
        }

    }
   
    private void Start()
    {
  
	 
        if (ExitCanves.activeSelf)
        {
            ExitCanves.SetActive(false);
        }

      Keyboard.SetActive(false);
    }


    private void OnEnable()
    {
        LoginEventRelay.OnEventAction += HandleEvent;
    }


    private void OnDisable()
    {
        LoginEventRelay.OnEventAction -= HandleEvent;
    }


    /// <summary>
    ///Function called by the button to show hide the Cnaves
    /// </summary>
    public void ExitGameCanves()
    {
        Debug.Log("Exit canves should hide or show");

        // check if the Exit canves is visible 
        if (ExitCanves.activeSelf)
        {
            ExitCanves.SetActive(false);
        }
        else
        {
            ExitCanves.SetActive(true);
        }
    }


    /// <summary>
    /// Function called by the button to Exit The Game 
    /// </summary>
    public void ExitGame()
    {
        Debug.Log("Application quit");
        Application.Quit();
    }


    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
    }


    string HandleEvent(LoginEventRelay.EventMessageType type)
    {
        if (EventsHandled.Contains(type))
        {
     
            switch (type)
            {
                case LoginEventRelay.EventMessageType.FillTheForm:
                 ShowKEyboard();
                    break;
            }
        }

        return "";
    }


    private void ShowKEyboard()
    {
        Debug.Log("show keyboard");

        Keyboard.SetActive(true);

        InputFiledCanvas.enabled = true;
      

        //NameInputField.Select();
        NameInputField.ActivateInputField();
        starttyping = true;
    }

    private void HideKeyboard()
    {
        Debug.Log("hide Kyboard");

        Keyboard.SetActive(false);

        InputFiledCanvas.enabled = false;
    }


    bool starttyping;

    
    
    
    


    
    

    private static string textoutput;


    TextField currentFiled;

    public void InputFiledGeter(string mytext)
    {
        if (mytext != null)
        {
            if ((mytext != "↵") && (mytext != "") &&
                (mytext != "←"))
            {
				NameInputField.text += mytext;
            }
            else if ((NameInputField.text.Length > 0) && (mytext == "←"))
            {
				NameInputField.text = NameInputField.text.Substring(0, NameInputField.text.Length - 1);
              
            }
            else if (mytext == "↵")
            {
                if (CheckName(NameInputField.text))
                {
                    Invoke("HideKeyboard", 1f);
                    if (!isActionMAdeForm)
                    {
                        isActionMAdeForm = true;
                        PlayerData.WritePlayerName(NameInputField.text);
                        Debug.Log("form complete is end ");
                        FormComplited();
                    }
                }
            }
        }
        else
        {
            Debug.Log("Textt is null ");
        }
    }


    private bool CheckName(string mytext)
    {
        if ((mytext.Length > 0) && (IsAllAlphabetic(mytext) && (mytext.Length < 15)))
        {
            Debug.Log("validInput");

            LoginEventRelay.RelayEvent(LoginEventRelay.EventMessageType.Validinput);
            return true;
        }
        else
        {
            Debug.Log("invalidInput");
            LoginEventRelay.RelayEvent(LoginEventRelay.EventMessageType.ErrorInput);
        }

        return false;
    }


    bool IsAllAlphabetic(string value)
    {
        foreach (char c in value)
        {
            if (!char.IsLetter(c))
                return false;
        }

        return true;
    }
    
    
    
    
    
    
    
    private  void FormComplited()
    {
        //Send this Event ot the Robot 
        LoginEventRelay.RelayEvent(LoginEventRelay.EventMessageType.FormCompleted);
    }



    
    
    
    
}