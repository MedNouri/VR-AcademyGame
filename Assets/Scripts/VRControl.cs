using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;


public abstract  class VRControl: MonoBehaviour {
 

    
 

        public enum Direction
        {
            autodetect,
            x,
            y,
            z
        }

    
         private bool interactWithoutGrab = false;

      

        abstract protected void InitRequiredComponents();
        abstract protected bool DetectSetup();
     

 
        protected bool setupSuccessful = true;
        

        protected float value;
 

 
 
    void Awake()
        {
            if (Application.isPlaying)
            {
                InitRequiredComponents();
         
            }

            setupSuccessful = DetectSetup();

         
        }

    void Update()
        {
            if (!Application.isPlaying)
            {
                setupSuccessful = DetectSetup();
            }
            else if (setupSuccessful)
            {
                float oldValue = value;
 

           
            }
        }


     

        protected Vector3 GetThirdDirection(Vector3 axis1, Vector3 axis2)
        {
            bool xTaken = axis1.x != 0 || axis2.x != 0;
            bool yTaken = axis1.y != 0 || axis2.y != 0;
            bool zTaken = axis1.z != 0 || axis2.z != 0;

            if (xTaken && yTaken)
            {
                return Vector3.forward;
            }
            else if (xTaken && zTaken)
            {
                return Vector3.up;
            }
            else
            {
                return Vector3.right;
            }
        }

   
    }
     

