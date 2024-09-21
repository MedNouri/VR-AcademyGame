using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Menu_Trash : MonoBehaviour {
public GameObject  EntredGameObject;
    public List<GameObject> RGameObjects =new List<GameObject>();
    public TextMesh TextMesh;

    private bool _isMAde=false;


    private void Start()
    {
        TextMesh.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other){
 

    EntredGameObject = other.gameObject;
    if (EntredGameObject!=null)
    {
        if ((RGameObjects.Contains(EntredGameObject))&&(_isMAde==false))
        {

            _isMAde = true;
         
            StartCoroutine(CourotinGame(EntredGameObject));
   
        }
    }

}


    IEnumerator CourotinGame(GameObject trash)
    {
        TextMesh.gameObject.SetActive(true);
        Debug.Log("we have A Apper trash ");
      trash.transform.position =new Vector3(0.8426064f,0.69f,-2.538f);
        yield return new WaitForSeconds(10f);
       
        TextMesh.gameObject.SetActive(false);
     
    }


}
