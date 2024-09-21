using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExceptionGenertor : MonoBehaviour
{

     public delegate void EvenCdPlayed();

     public static event EvenCdPlayed OnEventAction;
     

     private void OnEnable()
     {

          OnEventAction();

     }
}