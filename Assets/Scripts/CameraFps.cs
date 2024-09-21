using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFps : MonoBehaviour {
    
private Camera cam;
    
 
 void Start () {
     cam = GetComponent<Camera> ();
     StartCoroutine (DelayedRendering ());
 }
 
 public IEnumerator DelayedRendering(){
     while (true) {
         cam.Render();
         
         yield return new WaitForSeconds(10f);
 
     }
 }
}
