using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(BoxCollider))]
public class MouseCursor : MonoBehaviour
{
 
    Collider m_ObjectCollider;
    public Vector3 displacement;
    public LinearMapping linearMapping;

    private Vector3 initialPosition;
 
   
    public  void  ClickDown()
    {
        m_ObjectCollider.isTrigger =true;
 
 Invoke("ClickUp",0f);
    }
    public void  ClickUp()
    {
        m_ObjectCollider.isTrigger =false;
  

     
    }

    void Start()
    {
        initialPosition = transform.localPosition;
        m_ObjectCollider = GetComponent<Collider>();
     
    }


    //-------------------------------------------------
    void Update()
    {
       
        if ( linearMapping )
        {
            transform.localPosition = initialPosition + linearMapping.value * displacement;
        }
    }
 
}
