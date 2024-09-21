// Knob|Controls3D|100060

using Valve.VR.InteractionSystem;
  using UnityEngine;

 
 
    public class VrKnob : VRControl
    {
        public enum KnobDirection
        {
            x,
            y,
            z
        }

        [Tooltip("An optional game object to which the knob will be connected. If the game object moves the knob will follow along.")]
        public GameObject connectedTo;
        [Tooltip("The axis on which the knob should rotate. All other axis will be frozen.")]
        public KnobDirection direction = KnobDirection.x;
        [Tooltip("The minimum value of the knob.")]
        public float min = 0f;
        [Tooltip("The maximum value of the knob.")]
        public float max = 100f;
        [Tooltip("The increments in which knob values can change.")]
        public float stepSize = 1f;

        protected const float MAX_AUTODETECT_KNOB_WIDTH = 3; // multiple of the knob width
        protected KnobDirection finalDirection;
        protected KnobDirection subDirection;
        protected bool subDirectionFound = false;
        protected Quaternion initialRotation;
        protected Vector3 initialLocalRotation;
        protected ConfigurableJoint knobJoint;
        protected bool knobJointCreated = false;

        protected override void InitRequiredComponents()
        {
            initialRotation = transform.rotation;
            initialLocalRotation = transform.localRotation.eulerAngles;
            InitKnob();
        }

        protected override bool DetectSetup()
        {
            finalDirection = direction;

            if (knobJointCreated)
            {
                knobJoint.angularXMotion = ConfigurableJointMotion.Locked;
                knobJoint.angularYMotion = ConfigurableJointMotion.Locked;
                knobJoint.angularZMotion = ConfigurableJointMotion.Locked;

                switch (finalDirection)
                {
                    case KnobDirection.x:
                        knobJoint.angularXMotion = ConfigurableJointMotion.Free;
                        break;
                    case KnobDirection.y:
                        knobJoint.angularYMotion = ConfigurableJointMotion.Free;
                        break;
                    case KnobDirection.z:
                        knobJoint.angularZMotion = ConfigurableJointMotion.Free;
                        break;
                }
            }

            if (knobJoint)
            {
                knobJoint.xMotion = ConfigurableJointMotion.Locked;
                knobJoint.yMotion = ConfigurableJointMotion.Locked;
                knobJoint.zMotion = ConfigurableJointMotion.Locked;

                if (connectedTo)
                {
                    knobJoint.connectedBody = connectedTo.GetComponent<Rigidbody>();
                }
            }

            return true;
        }

 

        protected virtual void InitKnob()
        {
            Rigidbody knobRigidbody = GetComponent<Rigidbody>();
            if (knobRigidbody == null)
            {
                knobRigidbody = gameObject.AddComponent<Rigidbody>();
                knobRigidbody.angularDrag = 10; // otherwise knob will continue to move too far on its own
            }
            knobRigidbody.isKinematic = false;
            knobRigidbody.useGravity = false;

           Interactable knobInteractableObject = GetComponent<Interactable>();
            if (knobInteractableObject == null)
            {
                 
            }
         //   knobInteractableObject.isGrabbable = true;
        //    knobInteractableObject.grabAttachMechanicScript = gameObject.AddComponent<GrabAttachMechanics.VRTK_TrackObjectGrabAttach>();
          //  knobInteractableObject.grabAttachMechanicScript.precisionGrab = true;
      //      knobInteractableObject.secondaryGrabActionScript = gameObject.AddComponent<SecondaryControllerGrabActions.VRTK_SwapControllerGrabAction>();
         //   knobInteractableObject.stayGrabbedOnTeleport = false;

            knobJoint = GetComponent<ConfigurableJoint>();
            if (knobJoint == null)
            {
                knobJoint = gameObject.AddComponent<ConfigurableJoint>();
                knobJoint.configuredInWorldSpace = false;
                knobJointCreated = true;
            }

            if (connectedTo)
            {
                Rigidbody knobConnectedToRigidbody = connectedTo.GetComponent<Rigidbody>();
                if (knobConnectedToRigidbody == null)
                {
                    knobConnectedToRigidbody = connectedTo.AddComponent<Rigidbody>();
                    knobConnectedToRigidbody.useGravity = false;
                    knobConnectedToRigidbody.isKinematic = true;
                }
            }
        }
        
        
        

 

        protected virtual float CalculateValue()
        {
            if (!subDirectionFound)
            {
                float angleX = Mathf.Abs(transform.localRotation.eulerAngles.x - initialLocalRotation.x) % 90;
                float angleY = Mathf.Abs(transform.localRotation.eulerAngles.y - initialLocalRotation.y) % 90;
                float angleZ = Mathf.Abs(transform.localRotation.eulerAngles.z - initialLocalRotation.z) % 90;
                angleX = (Mathf.RoundToInt(angleX) >= 89) ? 0 : angleX;
                angleY = (Mathf.RoundToInt(angleY) >= 89) ? 0 : angleY;
                angleZ = (Mathf.RoundToInt(angleZ) >= 89) ? 0 : angleZ;

                if (Mathf.RoundToInt(angleX) != 0 || Mathf.RoundToInt(angleY) != 0 || Mathf.RoundToInt(angleZ) != 0)
                {
                    subDirection = angleX < angleY ? (angleY < angleZ ? KnobDirection.z : KnobDirection.y) : (angleX < angleZ ? KnobDirection.z : KnobDirection.x);
                    subDirectionFound = true;
                }
     
            }

            float angle = 0;
            switch (subDirection)
            {
                case KnobDirection.x:
                    angle = transform.localRotation.eulerAngles.x - initialLocalRotation.x;
                    
                    break;
                case KnobDirection.y:
                    angle = transform.localRotation.eulerAngles.y - initialLocalRotation.y;
                    break;
                case KnobDirection.z:
                    angle = transform.localRotation.eulerAngles.z - initialLocalRotation.z;
                    break;
            }
            angle = Mathf.Round(angle * 1000f) / 1000f; // not rounding will produce slight offsets in 4th digit that mess up initial value
        
            // Quaternion.angle will calculate shortest route and only go to 180
            float calculatedValue = 0;
            if (angle > 0 && angle <= 180)
            {
                calculatedValue = 360 - Quaternion.Angle(initialRotation, transform.rotation);
            }
            else
            {
                calculatedValue = Quaternion.Angle(initialRotation, transform.rotation);
            }

            // adjust to value scale
            calculatedValue = Mathf.Round((min + Mathf.Clamp01(calculatedValue / 360f) * (max - min)) / stepSize) * stepSize;
            if (min > max && angle != 0)
            {
                calculatedValue = (max + min) - calculatedValue;
            }
         
            return calculatedValue;
        }




        protected void FreezTransformation()
        {
            
            
        }

    }
