using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Valve.VR.InteractionSystem
{
 
    [RequireComponent(typeof(Interactable))]
    public class Climb : MonoBehaviour
    {
 
 
 
 
	    public Material HoverMaterial;
	    private Material oldMaterial;
	    private bool _used;
        
	    
	    
        private void OnHandHoverBegin(Hand hand)
        {
	  
	        oldMaterial =GetComponent<MeshRenderer>().material;
	             GetComponent<MeshRenderer>().material=HoverMaterial;
	        
		 
		 
        }


      
        private void OnHandHoverEnd(Hand hand)
        {
	        GetComponent<MeshRenderer>().material=oldMaterial;
			hand.HoverUnlock(GetComponent<Interactable>());

        }

		private void HandHoverUpdate(Hand hand)
		{


			// 	left = Player.instance.leftHand;
		 if(hand!=null){
			 if ( hand.GetStandardInteractionButtonDown() || ( ( hand.controller != null ) && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) ) )
			 {
				if (!_used)
				{
				hand.HoverLock (GetComponent<Interactable> ());
		
					Player.instance.transform.positionTo(0.5f,new Vector3(0, 0.5f, 0),true);
			
				_used = true;
				}
				 
			} else  if ( hand.GetStandardInteractionButtonUp() || ( ( hand.controller != null ) && hand.controller.GetPressUp( Valve.VR.EVRButtonId.k_EButton_Grip ) ) ) {
			
				hand.HoverUnlock (GetComponent<Interactable> ());
			
			}
		 }
		 
 

		}

    

    }

}