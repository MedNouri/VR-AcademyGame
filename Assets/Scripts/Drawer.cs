//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Demonstrates how to create a simple interactable object
//
//=============================================================================

using UnityEngine;
using System.Collections;

 
	public class Drawer : VRControl
    {
        [Tooltip("An optional game object to which the drawer will be connected. If the game object moves the drawer will follow along.")]
        public GameObject connectedTo;

        [Tooltip("The axis on whiFDirech the drawer should open. All other axis will be frozen.")]
        public Direction direction;
        [Tooltip("The game object for the body.")]
        public GameObject body;
        [Tooltip("The game object for the handle.")]
        public GameObject handle;
        [Tooltip("The parent game object for the drawer content elements.")]
        public GameObject content;
        [Tooltip("Makes the content invisible while the drawer is closed.")]
        public bool hideContent = true;
        [Tooltip("If the extension of the drawer is below this percentage then the drawer will snap shut.")]
        [Range(0, 1)]
        public float minSnapClose = 1;
        [Tooltip("The maximum percentage of the drawer's total length that the drawer will open to.")]
        [Range(0f, 1f)]
        public float maxExtend = 1f;

        protected Rigidbody drawerRigidbody;
        protected Rigidbody handleRigidbody;
        protected FixedJoint handleFixedJoint;
        protected ConfigurableJoint drawerJoint;
 
        protected ConstantForce drawerSnapForce;
        protected Direction finalDirection;
        protected float subDirection = 1; // positive or negative can be determined automatically since handle dictates that
        protected float pullDistance = 0f;
        protected Vector3 initialPosition;
        protected bool drawerJointCreated = false;
        protected bool drawerSnapForceCreated = false;
 

        protected override void InitRequiredComponents()
        {
            initialPosition = transform.position;

            InitBody();
            InitHandle();

            
        }

        protected override bool DetectSetup()
        {
            finalDirection = direction;
            if (finalDirection == Direction.autodetect)
            {
                return false;
            }

            // determin sub-direction depending on handle
            Bounds handleBounds = SharedMethods.GetBounds(GetHandle().transform, transform);
            Bounds bodyBounds = SharedMethods.GetBounds(GetBody().transform, transform);

            switch (finalDirection)
            {
                case Direction.x:
                    subDirection = (handleBounds.center.x > bodyBounds.center.x) ? -1 : 1;
                    pullDistance = bodyBounds.extents.x;
                    break;
                case Direction.y:
                    subDirection = (handleBounds.center.y > bodyBounds.center.y) ? -1 : 1;
                    pullDistance = bodyBounds.extents.y;
                    break;
                case Direction.z:
                    subDirection = (handleBounds.center.z > bodyBounds.center.z) ? -1 : 1;
                    pullDistance = bodyBounds.extents.z;
                    break;
            }

            if (body & handle)
            {
                // handle should be outside body hierarchy, otherwise anchor-by-bounds calculation is off
                if (handle.transform.IsChildOf(body.transform))
                {
                    return false;
                }
            }

            if (drawerJointCreated)
            {
                drawerJoint.xMotion = ConfigurableJointMotion.Locked;
                drawerJoint.yMotion = ConfigurableJointMotion.Locked;
                drawerJoint.zMotion = ConfigurableJointMotion.Locked;

                switch (finalDirection)
                {
                    case Direction.x:
                        drawerJoint.axis = Vector3.right;
                        drawerJoint.xMotion = ConfigurableJointMotion.Limited;
                        break;
                    case Direction.y:
                        drawerJoint.axis = Vector3.up;
                        drawerJoint.yMotion = ConfigurableJointMotion.Limited;
                        break;
                    case Direction.z:
                        drawerJoint.axis = Vector3.forward;
                        drawerJoint.zMotion = ConfigurableJointMotion.Limited;
                        break;
                }
                drawerJoint.anchor = drawerJoint.axis * (-subDirection * pullDistance);
            }
            if (drawerJoint)
            {
                drawerJoint.angularXMotion = ConfigurableJointMotion.Locked;
                drawerJoint.angularYMotion = ConfigurableJointMotion.Locked;
                drawerJoint.angularZMotion = ConfigurableJointMotion.Locked;

                pullDistance *= (maxExtend * 1.8f); // don't let it pull out completely

                SoftJointLimit drawerJointLimit = drawerJoint.linearLimit;
                drawerJointLimit.limit = pullDistance;
                drawerJoint.linearLimit = drawerJointLimit;

                if (connectedTo)
                {
                    drawerJoint.connectedBody = connectedTo.GetComponent<Rigidbody>();
                }
            }
            if (drawerSnapForceCreated)
            {
                drawerSnapForce.force = GetThirdDirection(drawerJoint.axis, drawerJoint.secondaryAxis) * (subDirection * -50f);
            }

            return true;
        }

 

        protected  void HandleUpdate()
        {
            value = CalculateValue();
            bool withinSnapLimit = (Mathf.Abs(value) < minSnapClose * 100f);
            drawerSnapForce.enabled = withinSnapLimit;
           // if (autoTriggerVolume)
         //   {
               // autoTriggerVolume.isEnabled = !withinSnapLimit;
            //}
        }

        protected virtual void InitBody()
        {
            drawerRigidbody = GetComponent<Rigidbody>();
            if (drawerRigidbody == null)
            {
                drawerRigidbody = gameObject.AddComponent<Rigidbody>();
                drawerRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            }
            drawerRigidbody.isKinematic = false;

 

            if (connectedTo)
            {
                Rigidbody drawerConnectedToRigidbody = connectedTo.GetComponent<Rigidbody>();
                if (drawerConnectedToRigidbody == null)
                {
                    drawerConnectedToRigidbody = connectedTo.AddComponent<Rigidbody>();
                    drawerConnectedToRigidbody.useGravity = false;
                    drawerConnectedToRigidbody.isKinematic = true;
                }
            }

            drawerJoint = GetComponent<ConfigurableJoint>();
            if (drawerJoint == null)
            {
                drawerJoint = gameObject.AddComponent<ConfigurableJoint>();
                drawerJointCreated = true;
            }

            drawerSnapForce = GetComponent<ConstantForce>();
            if (drawerSnapForce == null)
            {
                drawerSnapForce = gameObject.AddComponent<ConstantForce>();
                drawerSnapForce.enabled = false;
                drawerSnapForceCreated = true;
            }
        }

        protected virtual void InitHandle()
        {
            handleRigidbody = GetHandle().GetComponent<Rigidbody>();
            if (handleRigidbody == null)
            {
                handleRigidbody = GetHandle().AddComponent<Rigidbody>();
            }
            handleRigidbody.isKinematic = false;
            handleRigidbody.useGravity = false;

            handleFixedJoint = GetHandle().GetComponent<FixedJoint>();
            if (handleFixedJoint == null)
            {
                handleFixedJoint = GetHandle().AddComponent<FixedJoint>();
                handleFixedJoint.connectedBody = drawerRigidbody;
            }
        }

  

        protected virtual float CalculateValue()
        {
            return (Mathf.Round((transform.position - initialPosition).magnitude / pullDistance * 100));
        }

        protected virtual GameObject GetBody()
        {
            return (body ? body : gameObject);
        }

        protected virtual GameObject GetHandle()
        {
            return (handle ? handle : gameObject);
        }
    }
 
