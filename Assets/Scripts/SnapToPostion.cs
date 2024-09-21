using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


namespace Valve.VR.InteractionSystem
{
    [RequireComponent(typeof(Interactable))]
    public class SnapToPostion : MonoBehaviour
    {
        public enum SnapTypes
        {
            UseJoint,
            UseParenting
        }


        public SnapTypes SnapyTypes;
        private Hand hand;
        protected String ObjectName;
        private bool isOnDropZone = false;
        protected bool StartDetection;
        public GameObject Parent;
        private bool isSnaped;

        
        /// <summary>
        /// Delegate of type void  .
        /// </summary>
        public delegate void SnapZoneEventAction();

        /// <summary>
        /// Static Event of type SnapZoneEventAction  .
        /// </summary>
        public static event SnapZoneEventAction ObjectEnteredSnapDropZone;

        /// <summary>
        /// Emitted when a valid interactable object exists the snap drop zone trigger collider.
        /// </summary>
        public static event SnapZoneEventAction ObjectExitedSnapDropZone;

        /// <summary>
        /// Emitted when an interactable object is successfully snapped into a drop zone.
        /// </summary>
        public static event SnapZoneEventAction ObjectSnappedToDropZone;

        /// <summary>
        /// Emitted when an interactable object is removed from a snapped drop zone.
        /// </summary>
        public static event SnapZoneEventAction ObjectUnsnappedFromDropZone;

        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        protected SnapTypes SnapType;
        private Renderer rend;


        
        
        
        
        
        [SerializeField] protected GameObject HighlightGameObject;


        [SerializeField] protected GameObject SnapPostion;


        private GameObject _enterdObject;
        private MeshRenderer _meshRenderer;

        [SerializeField] protected List<GameObject> ValidSnapObjects = new List<GameObject>();


        public virtual void OnObjectEnteredSnapDropZone()
        {
            if (ObjectEnteredSnapDropZone != null)
            {
                ObjectEnteredSnapDropZone();
            }

            Debug.Log("Snap event in action");

        }

        public void OnObjectExitedSnapDropZone()
        {
            if (ObjectExitedSnapDropZone != null)
            {
                Debug.Log("Object exsit Drop Zone");
                ObjectExitedSnapDropZone();
                RemovehighlightSnapPOstion();
            }
        }

        public  void OnObjectSnappedToDropZone()
        {
            if (ObjectSnappedToDropZone != null)
            {
                ObjectSnappedToDropZone();
                Debug.Log("Object Snaped Drop Zone");
            }
        }

        public  void OnObjectUnsnappedFromDropZone()
        {
            
            if (ObjectUnsnappedFromDropZone != null)
            {
                ObjectUnsnappedFromDropZone();
      
            }
        }


        private bool _isActionMade = false;





        private bool _Objectsanped = false;
        protected Collider m_Collider;
        private GameObject EnterdObject;

        private void OnTriggerEnter(Collider other)
        {

            if (StartDetection)
            {


                if (!_Objectsanped)
                {

                    EnterdObject = other.gameObject;
                    Debug.Log("name" + EnterdObject.name);
                    if (isAvalidObject(EnterdObject))
                    {
                        GenerateHighlightObject();
                        SnapToPos(EnterdObject);
                        OnObjectEnteredSnapDropZone();
                        _Objectsanped = true;
                    }
                }
            }
            else
            {
                Debug.Log("Cant detect Anything ");
            }
        }

 

        private void OnTriggerExit(Collider other)
        {
            GameObject entreGameObject = other.gameObject;
        
            if (isAvalidObject(entreGameObject))
            {   OnObjectExitedSnapDropZone();
              
                isOnDropZone = false;
                _Objectsanped = false;
                ObjectName = null;
             
                isSnaped = false;
                Debug.Log("Object is Out ");
              }

        }


        private void OnEnable()
        {
            ObjectEnteredSnapDropZone += OnObjectShouldSnap;
            ObjectExitedSnapDropZone += OnObjectExitedSnap;
        }

        private void OnObjectExitedSnap()
        {
            _isActionMade = false;
            Debug.Log("Object exite");
        }


        private void OnObjectShouldSnap()
        {
            isOnDropZone = true;
            Debug.Log("Object is On drop Zone ");
        }

        void Start()
        {
            m_Collider = GetComponent<Collider>();

            RemovehighlightSnapPOstion();
        }


        private bool isAvalidObject(GameObject EntredObject)
        {
            if (ValidSnapObjects.Contains(EntredObject))
            {

                return true;

            }

            return false;
        }

        protected void SnapToPos(GameObject enteredGameObject)
        {
            for (int i = 0; i < Player.instance.handCount; i++)
            {
                Hand hand = Player.instance.GetHand(i);
                if (isOnDropZone)
                {
                    
                    
                
                    if ( hand.GetStandardInteractionButtonDown() || ( ( hand.controller != null ) && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) ) )
                    {

                        if (isAvalidObject(hand.currentAttachedObject))
                        {
                            ObjectWithTHeHand = hand.currentAttachedObject;
                       
                            hand.DetachObject(ObjectWithTHeHand);
                            hand.HoverUnlock(ObjectWithTHeHand.GetComponent<Interactable>());

                            ObjectWithTHeHand.transform.SetParent(HighlightGameObject.transform);
                      
                            Debug.Log("cureent hand Postion " + ObjectWithTHeHand);
                            StartCoroutine(DisableCollider(ObjectWithTHeHand));
                        }

                    }
                }
            }


            ObjectName = enteredGameObject.name;
            OnObjectSnappedToDropZone();
            Debug.Log("Snaping");

            enteredGameObject.transform.position = HighlightGameObject.transform.position;
            enteredGameObject.transform.rotation = HighlightGameObject.transform.rotation;

            if (SnapyTypes == SnapTypes.UseParenting)
            {
                enteredGameObject.transform.SetParent(HighlightGameObject.transform);
                Debug.Log("Setting the Parent ");

            }

            if (SnapyTypes == SnapTypes.UseJoint)
            {
               // enteredGameObject.transform.SetParent(HighlightGameObject.transform);
       
                enteredGameObject.GetComponent<Collider>().enabled = false;
                        
     
                enteredGameObject.GetComponent<Interactable>().enabled = false;
            }

            if (enteredGameObject.transform.position == HighlightGameObject.transform.position)
            {
                RemovehighlightSnapPOstion();
            }

            isSnaped = true;
        }


        protected void RemovehighlightSnapPOstion()
        {
            HighlightGameObject.GetComponent<MeshRenderer>().enabled = false;
        }


        protected   void CopyObject(GameObject objectBlueprint)
        {
            Vector3 saveScale = transform.localScale;
            transform.localScale = Vector3.one;


            //default position of new highlight object
            objectBlueprint.transform.localPosition = Vector3.zero;
            objectBlueprint.transform.localRotation = Quaternion.identity;

            transform.localScale = saveScale;
        }


        protected bool IsObjectStillOnDropZone()
        {
            return _Objectsanped;

        }

        protected void GenerateHighlightObject()
        {
            HighlightGameObject.GetComponent<MeshRenderer>().enabled = true;
        }

        private GameObject ObjectWithTHeHand;

    

        private void OnTriggerStay(Collider other)
        {
             

            if (isSnaped)
            { 
                if (EnterdObject.transform.parent!=HighlightGameObject.transform)
                {
                    EnterdObject.transform.SetParent(HighlightGameObject.transform);
                }
            }
            if (StartDetection==false)
            {
                 
                if (isSnaped)
                {
               
                   OnObjectUnsnappedFromDropZone();
      
               }
        }
        }

        IEnumerator DisableCollider(GameObject other)
        {
           other.GetComponent<Collider>().enabled = false;
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Collider is Back");
           other.GetComponent<Collider>().enabled = true;
            
            
        }
    }
}