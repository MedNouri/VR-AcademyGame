using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FramesPerSecondViewer : MonoBehaviour {

        [Tooltip("Toggles whether the FPS text is visible.")]
        public bool displayFPS = true;
        [Tooltip("The frames per second deemed acceptable that is used as the benchmark to change the FPS text colour.")]
        public int targetFPS = 90;
        [Tooltip("The size of the font the FPS is displayed in.")]
        public int fontSize = 32;
        [Tooltip("The position of the FPS text within the headset view.")]
        public Vector3 position = Vector3.zero;
        [Tooltip("The colour of the FPS text when the frames per second are within reasonable limits of the Target FPS.")]
        public Color goodColor = Color.green;
        [Tooltip("The colour of the FPS text when the frames per second are falling short of reasonable limits of the Target FPS.")]
        public Color warnColor = Color.yellow;
        [Tooltip("The colour of the FPS text when the frames per second are at an unreasonable level of the Target FPS.")]
        public Color badColor = Color.red;

        protected const float updateInterval = 0.5f;
        protected int framesCount;
        protected float framesTime;
   
        
 
        protected virtual void Updatse()
        {
            framesCount++;
            framesTime += Time.unscaledDeltaTime;

            if (framesTime > updateInterval)
            {
            
                    if (displayFPS)
                    {
                        float fps = framesCount / framesTime;
                        
                        print("fps"+fps);
                        //text.text = string.Format("{0:F2} FPS", fps);
                      //  text.color = (fps > (targetFPS - 5) ? goodColor :
                        //             (fps > (targetFPS - 30) ? warnColor :
                          //            badColor));
                    }
                    else
                    {
                         
                    }
             
                framesCount = 0;
                framesTime = 0;
            }
        }

 

        

         
 
} 
