using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataManger : MonoBehaviour
{

 public TextMesh PlayerName;
 


 private void Awake()
 {
  PlayerName.text = PlayerData.DisplayPlayerName();
 }
}
