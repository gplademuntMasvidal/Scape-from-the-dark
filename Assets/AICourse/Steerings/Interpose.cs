using UnityEngine;

namespace Steerings
{

    public class Interpose : SteeringBehaviour
    {

        
        // remove comments for steerings that must be provided with a target 
        // remove whole block if no explicit target required
        // (if FT or FTI policies make sense, then this method must be present)
        public GameObject target;
        public GameObject secondTarget;

        public override GameObject GetTarget()
        {
            return target;
        }
        
        
        public override Vector3 GetLinearAcceleration()
        {
            return Interpose.GetLinearAcceleration(Context, target, secondTarget/* add extra parameters (target?) if required */);
        }

        
        public static Vector3 GetLinearAcceleration (SteeringContext me , GameObject target, GameObject secondTarget/* add extra parameters (target?) if required */)
        {
            /* COMPLETE this method. It must return the linear acceleration (Vector3) */

            Vector3 distanceBetweenVectors = target.transform.position + secondTarget.transform.position;
            Vector3 middlepoint = distanceBetweenVectors / 2;   

            SURROGATE_TARGET.transform.position = middlepoint;


            return Arrive.GetLinearAcceleration(me, SURROGATE_TARGET);
        }

    }
}