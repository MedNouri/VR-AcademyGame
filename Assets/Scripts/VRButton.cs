using System;

using UnityEngine;
using UnityEngine.Events;

 
namespace Valve.VR.InteractionSystem
{
   
    [RequireComponent( typeof( Interactable ) )]
 

 


    public class VRButton : VRControl
    {
      
        public UnityEvent  ButtonListeners;

        public enum ButtonDirection
        {

            x,
            y,
            z,
      
        }

        [Tooltip(
            "game object to which the button will be connected. If the game object moves the button will follow along.")]
        public GameObject connectedTo;

        [Tooltip("The axis on which the button should move. All other axis will be frozen.")]
        public ButtonDirection direction;

        [Tooltip("The local distance the button needs to be pushed until a push event is triggered.")]
        public float activationDistance = 0.0461472f;

        [Tooltip(
            "The amount of force needed to push the button down as well as the speed with which it will go back into its original position.")]
        public float buttonStrength = 1.0f;

        [Tooltip(
            "change button color when hover on the button ")]
        private Material m_Material;

        public Material Pressed;

        private AudioSource _audioSource;
        private AudioClip _audioClip;
     private Rigidbody rb;
        protected ButtonDirection finalDirection;
        protected Vector3 restingPosition;
        protected Vector3 activationDir;
        protected Rigidbody buttonRigidbody;

        protected ConfigurableJoint buttonJoint;

        //  protected ConstantForce buttonForce;
        protected int forceCount = 0;
        protected ConstantForce buttonForce;
        private bool HandOnZone;
        
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            m_Material = GetComponent<Renderer>().material;
   
        }

        
        private void HandHoverUpdate( Hand hand )
        {
			 
            if ( hand.GetStandardInteractionButtonDown() || ( ( hand.controller != null ) && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) ) )
            {
                if (hand.currentAttachedObject != gameObject)
                {
                  PressButton();
                }
            }
        }



        private void  PressButton()
        {
            ButtonListeners.Invoke();
 
            if (direction==ButtonDirection.y)
            {

          //   transform.localPositionTo(0.1f, new Vector3(0, -7f, 0f), true);
                     
                   transform.Translate(0,-0.561472f,0f);    
               // transform.Translate(0,-5f,0);       
            }
            if (direction==ButtonDirection.x)
            {
                
            
                transform.Translate(-0.0461472f,0,0);       
            }
            if (direction==ButtonDirection.z)
            {
                
            
                transform.Translate(0,0,-0.0461472f);       
            }

        }
        

        //-------------------------------------------------
        private void OnHandHoverBegin()
        {
             
            GetComponent<MeshRenderer>().material = Pressed;
            HandOnZone = true;
 

        }

        //-------------------------------------------------
        private void OnHandHoverEnd()
        {
            GetComponent<MeshRenderer>().material = m_Material;
            HandOnZone = false;
        }



        protected override void InitRequiredComponents()
        {
            restingPosition = transform.position;
            
     
            
            
            if (!GetComponent<Collider>())
            {
                gameObject.AddComponent<BoxCollider>();
            }

            buttonRigidbody = GetComponent<Rigidbody>();
            if (buttonRigidbody == null)
            {
                buttonRigidbody = gameObject.AddComponent<Rigidbody>();
            }

            buttonRigidbody.isKinematic = false;
            buttonRigidbody.useGravity = false;

            buttonRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            buttonForce = GetComponent<ConstantForce>();
            if (buttonForce == null)
            {
                buttonForce = gameObject.AddComponent<ConstantForce>();
            }


            if (connectedTo)
            {
                Rigidbody connectedToRigidbody = connectedTo.GetComponent<Rigidbody>();
                if (connectedToRigidbody == null)
                {
                    connectedToRigidbody = connectedTo.AddComponent<Rigidbody>();
                }

                connectedToRigidbody.useGravity = false;
            }
        }







        protected override bool DetectSetup()
        {
            finalDirection = direction;

            if (buttonForce)
            {
                switch (finalDirection)
                {
                        case ButtonDirection.x:       buttonForce.force = new Vector3(5   , 0, 0);
                            break;
                        case ButtonDirection.y:       buttonForce.force = new Vector3(0, 5, 0);
                    break;
                    case ButtonDirection.z:       buttonForce.force = new Vector3(0, 0,5);
                        break;
                }
         
            }

            if (Application.isPlaying)
            {
                buttonJoint = GetComponent<ConfigurableJoint>();

                bool recreate = false;
                Rigidbody oldBody = null;
                Vector3 oldAnchor = Vector3.zero;
                Vector3 oldAxis = Vector3.zero;

                if (buttonJoint)
                {
                    // save old values, needs to be recreated
                    oldBody = buttonJoint.connectedBody;
                    oldAnchor = buttonJoint.anchor;
                    oldAxis = buttonJoint.axis;
                    DestroyImmediate(buttonJoint);
                    recreate = true;
                }

                // since limit applies to both directions object needs to be moved halfway to activation before adding joint
                transform.position = transform.position + ((activationDir.normalized * activationDistance) * 0.2f);
                buttonJoint = gameObject.AddComponent<ConfigurableJoint>();

                if (recreate)
                {
                    buttonJoint.connectedBody = oldBody;
                    buttonJoint.anchor = oldAnchor;
                    buttonJoint.axis = oldAxis;
                }

                if (connectedTo)
                {
                    buttonJoint.connectedBody = connectedTo.GetComponent<Rigidbody>();
                }

                SoftJointLimit buttonJointLimits = new SoftJointLimit();
                buttonJointLimits.limit =
                    activationDistance *
                    0.501f; // set limit to half (since it applies to both directions) and a tiny bit larger since otherwise activation distance might be missed
                buttonJoint.linearLimit = buttonJointLimits;

                buttonJoint.angularXMotion = ConfigurableJointMotion.Locked;
                buttonJoint.angularYMotion = ConfigurableJointMotion.Locked;
                buttonJoint.angularZMotion = ConfigurableJointMotion.Locked;
                buttonJoint.xMotion = ConfigurableJointMotion.Locked;
               buttonJoint.yMotion = ConfigurableJointMotion.Locked;
               buttonJoint.zMotion = ConfigurableJointMotion.Locked;
                 //buttonRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                switch (finalDirection)
                {
                    case ButtonDirection.x:
                  
           //       buttonRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;
                        if (Mathf.RoundToInt(Mathf.Abs(transform.right.x)) == 1)
                        {
                            buttonJoint.xMotion = ConfigurableJointMotion.Limited;
                        }
                        else if (Mathf.RoundToInt(Mathf.Abs(transform.up.x)) == 1)
                        {
                            buttonJoint.yMotion = ConfigurableJointMotion.Limited;
                        }
                        else if (Mathf.RoundToInt(Mathf.Abs(transform.forward.x)) == 1)
                        {
                            buttonJoint.zMotion = ConfigurableJointMotion.Limited;
                        }

                        break;
                    case ButtonDirection.y:
                     
                     buttonRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
                        if (Mathf.RoundToInt(Mathf.Abs(transform.right.y)) == 1)
                        {
                            buttonJoint.xMotion = ConfigurableJointMotion.Limited;
                        }
                        else if (Mathf.RoundToInt(Mathf.Abs(transform.up.y)) == 1)
                        {
                            buttonJoint.yMotion = ConfigurableJointMotion.Limited;
                        }
                        else if (Mathf.RoundToInt(Mathf.Abs(transform.forward.y)) == 1)
                        {
                            buttonJoint.zMotion = ConfigurableJointMotion.Limited;
                        }

                        break;
                    case ButtonDirection.z:
           
               //  buttonRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
                        if (Mathf.RoundToInt(Mathf.Abs(transform.right.z)) == 1)
                        {
                            buttonJoint.xMotion = ConfigurableJointMotion.Limited;
                        }
                        else if (Mathf.RoundToInt(Mathf.Abs(transform.up.z)) == 1)
                        {
                            buttonJoint.yMotion = ConfigurableJointMotion.Limited;
                        }
                        else if (Mathf.RoundToInt(Mathf.Abs(transform.forward.z)) == 1)
                        {
                            buttonJoint.zMotion = ConfigurableJointMotion.Limited;
                        }

                        break;
                }
            }

            return true;
        }



        void FixedUpdates()
        { 


            if (Vector3.Distance(transform.position, restingPosition) >= activationDistance)
            {



                if (HandOnZone)
                {

                  //  ButtonListeners.Invoke();

                }
                else
                {
                    Debug.Log("hand is not hoviring");
                }



            }

     
            if (forceCount == 0 && buttonJoint.connectedBody)
            {
                restingPosition = transform.position;
            }
        }
 

        public void Testbutton()
        {
    PressButton();

        }
 
    }
}