using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.Collections;

 

	public class Door : VRControl
	{

	[Tooltip("The axis on which the door should open.")]
        public Direction direction = Direction.autodetect;
        [Tooltip("The game object for the door. Can also be an empty parent or left empty if the script is put onto the actual door mesh. If no colliders exist yet a collider will tried to be automatically attached to all children that expose renderers.")]
        public GameObject door;
        [Tooltip("The game object for the handles. Can also be an empty parent or left empty. If empty the door can only be moved using the rigidbody mode of the controller. If no collider exists yet a compound collider made up of all children will try to be calculated but this will fail if the door is rotated. In that case a manual collider will need to be assigned.")]
        public GameObject handles;
        [Tooltip("The game object for the frame to which the door is attached. Should only be set if the frame will move as well to ensure that the door moves along with the frame.")]
        public GameObject frame;
    
      
        [Tooltip("The maximum opening angle of the door.")]
        public float maxAngle = 120f;
        [Tooltip("Can the door be pulled to open.")]
        public bool openInward = false;
        [Tooltip("Can the door be pushed to open.")]
        public bool openOutward = true;
        [Tooltip("The range at which the door must be to being closed before it snaps shut. Only works if either inward or outward is selected, not both.")]
        [Range(0, 1)]
        public float minSnapClose = 1;
        [Tooltip("The amount of friction the door will have whilst swinging when it is not grabbed.")]
        public float releasedFriction = 10f;
        [Tooltip("The amount of friction the door will have whilst swinging when it is grabbed.")]
        public float grabbedFriction = 100f;
      

        protected float stepSize = 1f;
        protected Rigidbody doorRigidbody;
        protected HingeJoint doorHinge;
        protected ConstantForce doorSnapForce;
        protected Rigidbody frameRigidbody;
        protected Direction finalDirection;
        protected float subDirection = 1; // positive or negative can be determined automatically since handles dictate that
        protected Vector3 secondaryDirection;
        protected bool doorHingeCreated = false;
        protected bool doorSnapForceCreated = false;


	 
        protected override void InitRequiredComponents()
        {
            InitFrame();
            InitDoor();
            InitHandle();

    
        }

        protected override bool DetectSetup()
        {
            // detect axis
            doorHinge = GetDoor().GetComponent<HingeJoint>();
            if (doorHinge && !doorHingeCreated)
            {
                direction = Direction.autodetect;
            }
            finalDirection = (direction == Direction.autodetect) ? DetectDirection() : direction;
            if (finalDirection == Direction.autodetect)
            {
                return false;
            }
            if (doorHinge && !doorHingeCreated)
            {
                // if there is a hinge joint already it overrides axis selection
                direction = finalDirection;
            }

            // detect opening direction
            Bounds doorBounds = SharedMethods.GetBounds(GetDoor().transform, transform);
            if (doorHinge == null || doorHingeCreated)
            {
                if (handles)
                {
                    // determin sub-direction depending on handle location
                    Bounds handleBounds = SharedMethods.GetBounds(handles.transform, transform);
                    switch (finalDirection)
                    {
                        case Direction.x:
                            if ((handleBounds.center.z + handleBounds.extents.z) > (doorBounds.center.z + doorBounds.extents.z) || (handleBounds.center.z - handleBounds.extents.z) < (doorBounds.center.z - doorBounds.extents.z))
                            {
                                subDirection = (handleBounds.center.y > doorBounds.center.y) ? -1 : 1;
                                secondaryDirection = Vector3.up;
                            }
                            else
                            {
                                subDirection = (handleBounds.center.z > doorBounds.center.z) ? -1 : 1;
                                secondaryDirection = Vector3.forward;
                            }
                            break;
                        case Direction.y:
                            if ((handleBounds.center.z + handleBounds.extents.z) > (doorBounds.center.z + doorBounds.extents.z) || (handleBounds.center.z - handleBounds.extents.z) < (doorBounds.center.z - doorBounds.extents.z))
                            {
                                subDirection = (handleBounds.center.x > doorBounds.center.x) ? -1 : 1;
                                secondaryDirection = Vector3.right;
                            }
                            else
                            {
                                subDirection = (handleBounds.center.z > doorBounds.center.z) ? -1 : 1;
                                secondaryDirection = Vector3.forward;
                            }
                            break;
                        case Direction.z:
                            if ((handleBounds.center.x + handleBounds.extents.x) > (doorBounds.center.x + doorBounds.extents.x) || (handleBounds.center.x - handleBounds.extents.x) < (doorBounds.center.x - doorBounds.extents.x))
                            {
                                subDirection = (handleBounds.center.y > doorBounds.center.y) ? -1 : 1;
                                secondaryDirection = Vector3.up;
                            }
                            else
                            {
                                subDirection = (handleBounds.center.x > doorBounds.center.x) ? -1 : 1;
                                secondaryDirection = Vector3.right;
                            }
                            break;
                    }
                }
                else
                {
                    switch (finalDirection)
                    {
                        case Direction.x:
                            secondaryDirection = (doorBounds.extents.y > doorBounds.extents.z) ? Vector3.up : Vector3.forward;
                            break;
                        case Direction.y:
                            secondaryDirection = (doorBounds.extents.x > doorBounds.extents.z) ? Vector3.right : Vector3.forward;
                            break;
                        case Direction.z:
                            secondaryDirection = (doorBounds.extents.y > doorBounds.extents.x) ? Vector3.up : Vector3.right;
                            break;
                    }
                    // TODO: derive how to detect -1
                    subDirection = 1;
                }
            }
            else
            {
                // calculate directions from existing anchor
                Vector3 existingAnchorDirection = doorBounds.center - doorHinge.connectedAnchor;
                if (existingAnchorDirection.x != 0)
                {
                    secondaryDirection = Vector3.right;
                    subDirection = existingAnchorDirection.x <= 0 ? 1 : -1;
                }
                else if (existingAnchorDirection.y != 0)
                {
                    secondaryDirection = Vector3.up;
                    subDirection = existingAnchorDirection.y <= 0 ? 1 : -1;
                }
                else if (existingAnchorDirection.z != 0)
                {
                    secondaryDirection = Vector3.forward;
                    subDirection = existingAnchorDirection.z <= 0 ? 1 : -1;
                }
            }

            if (doorHingeCreated)
            {
                float extents = 0;
                if (secondaryDirection == Vector3.right)
                {
                    extents = doorBounds.extents.x / GetDoor().transform.lossyScale.x;
                }
                else if (secondaryDirection == Vector3.up)
                {
                    extents = doorBounds.extents.y / GetDoor().transform.lossyScale.y;
                }
                else
                {
                    extents = doorBounds.extents.z / GetDoor().transform.lossyScale.z;
                }

                doorHinge.anchor = secondaryDirection * subDirection * extents;
                doorHinge.axis = Direction2Axis(finalDirection);
            }
            if (doorHinge)
            {
                doorHinge.useLimits = true;
                doorHinge.enableCollision = true;

                JointLimits limits = doorHinge.limits;
                limits.min = openInward ? -maxAngle : 0;
                limits.max = openOutward ? maxAngle : 0;
                limits.bounciness = 0;
                doorHinge.limits = limits;
            }
            if (doorSnapForceCreated)
            {
                float forceToApply = (-5f * GetDirectionFromJoint());
                doorSnapForce.relativeForce = GetThirdDirection(doorHinge.axis, secondaryDirection) * (subDirection * forceToApply);
            }

            return true;
        }

 
     

        protected virtual float GetDirectionFromJoint()
        {
            return (doorHinge.limits.min < 0f ? -1f : 1f);
        }

        protected virtual Vector3 Direction2Axis(Direction givenDirection)
        {
            Vector3 returnAxis = Vector3.zero;

            switch (givenDirection)
            {
                case Direction.x:
                    returnAxis = new Vector3(1, 0, 0);
                    break;
                case Direction.y:
                    returnAxis = new Vector3(0, 1, 0);
                    break;
                case Direction.z:
                    returnAxis = new Vector3(0, 0, 1);
                    break;
            }

            return returnAxis;
        }

        protected virtual Direction DetectDirection()
        {
            Direction returnDirection = Direction.autodetect;

            if (doorHinge && !doorHingeCreated)
            {
                // use direction of hinge joint
                if (doorHinge.axis == Vector3.right)
                {
                    returnDirection = Direction.x;
                }
                else if (doorHinge.axis == Vector3.up)
                {
                    returnDirection = Direction.y;
                }
                else if (doorHinge.axis == Vector3.forward)
                {
                    returnDirection = Direction.z;
                }
            }
            else
            {
                if (handles)
                {
                    Bounds handleBounds = SharedMethods.GetBounds(handles.transform, transform);
                    Bounds doorBounds = SharedMethods.GetBounds(GetDoor().transform, transform, handles.transform);

                    // handles determine direction, there are actually two directions possible depending on handle position, we'll just detect one of them for now, preference is y
                    if ((handleBounds.center.y + handleBounds.extents.y) > (doorBounds.center.y + doorBounds.extents.y) || (handleBounds.center.y - handleBounds.extents.y) < (doorBounds.center.y - doorBounds.extents.y))
                    {
                        returnDirection = Direction.x;
                    }
                    else
                    {
                        returnDirection = Direction.y;
                    }
                }
            }

            return returnDirection;
        }

        protected virtual void InitFrame()
        {
            if (frame == null)
            {
                return;
            }

            frameRigidbody = frame.GetComponent<Rigidbody>();
            if (frameRigidbody == null)
            {
                frameRigidbody = frame.AddComponent<Rigidbody>();
                frameRigidbody.isKinematic = true; // otherwise frame moves/falls over when door is moved or fully open
                frameRigidbody.angularDrag = releasedFriction; // in case this is a nested door
            }
        }

        protected virtual void InitDoor()
        {
            GameObject actualDoor = GetDoor();
            SharedMethods.CreateColliders(actualDoor);

            doorRigidbody = actualDoor.GetComponent<Rigidbody>();
            if (doorRigidbody == null)
            {
                doorRigidbody = actualDoor.AddComponent<Rigidbody>();
                doorRigidbody.angularDrag = releasedFriction;
            }
            doorRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // otherwise door will not react to fast moving controller
            doorRigidbody.isKinematic = false; // in case nested door as already created this

            doorHinge = actualDoor.GetComponent<HingeJoint>();
            if (doorHinge == null)
            {
                doorHinge = actualDoor.AddComponent<HingeJoint>();
                doorHingeCreated = true;
            }
            doorHinge.connectedBody = frameRigidbody;

            doorSnapForce = actualDoor.GetComponent<ConstantForce>();
            if (doorSnapForce == null)
            {
                doorSnapForce = actualDoor.AddComponent<ConstantForce>();
                doorSnapForce.enabled = false;
                doorSnapForceCreated = true;
            }

      
        }

        protected virtual void InitHandle()
        {
            if (handles == null)
            {
                return;
            }

            if (handles.GetComponentInChildren<Collider>() == null)
            {
                SharedMethods.CreateColliders(handles);
            }

            Rigidbody handleRigidbody = handles.GetComponent<Rigidbody>();
            if (handleRigidbody == null)
            {
                handleRigidbody = handles.AddComponent<Rigidbody>();
            }
            handleRigidbody.isKinematic = false;
            handleRigidbody.useGravity = false;

            FixedJoint handleFixedJoint = handles.GetComponent<FixedJoint>();
            if (handleFixedJoint == null)
            {
                handleFixedJoint = handles.AddComponent<FixedJoint>();
                handleFixedJoint.connectedBody = doorRigidbody;
            }

        
        }
  
        protected virtual GameObject GetDoor()
        {
            return (door ? door : gameObject);
        }

	}
 