using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBordManger : MonoBehaviour
{

    public GameObject PlayerData;
    public GameObject SettingsSData;
     
     
     
    
    
    
    
    private void OnEnable()
    {
        PlayerData.SetActive(true);
        SettingsSData.SetActive(true);
    }
}
