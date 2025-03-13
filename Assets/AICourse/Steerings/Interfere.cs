using UnityEngine;

namespace Steerings
{

    public class Interfere : SteeringBehaviour
    {

        
        // remove comments for steerings that must be provided with a target 
        // remove whole block if no explicit target required
        // (if FT or FTI policies make sense, then this method must be present)
        public GameObject target;
        public float requiredDistance;


        public override GameObject GetTarget()
        {
            return target;
        }
        
        
        public override Vector3 GetLinearAcceleration()
        {
            return Interfere.GetLinearAcceleration(Context, target, requiredDistance /* add extra parameters (target?) if required */);
        }

        
        public static Vector3 GetLinearAcceleration (SteeringContext me, GameObject target, float requiredDistance/* add extra parameters (target?) if required */)
        {
            /* COMPLETE this method. It must return the linear acceleration (Vector3) */


            Vector3 targetVelocity = target.GetComponent<SteeringContext>().velocity;
            Vector3 finalPosition = targetVelocity.normalized * requiredDistance + target.transform.position;
            SURROGATE_TARGET.transform.position = finalPosition;

            return Arrive.GetLinearAcceleration(me, SURROGATE_TARGET);
        }

    }
}