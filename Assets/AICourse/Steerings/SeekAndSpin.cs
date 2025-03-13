using UnityEngine;

namespace Steerings
{

    public class SeekAndSpin : SteeringBehaviour
    {

        
        // remove comments for steerings that must be provided with a target 
        // remove whole block if no explicit target required
        // (if FT or FTI policies make sense, then this method must be present)
        public GameObject target;

        public override GameObject GetTarget()
        {
            return target;
        }
        
        
        public override Vector3 GetLinearAcceleration()
        {
            return SeekAndSpin.GetLinearAcceleration(Context, target /* add extra parameters (target?) if required */);
        }

        
        public static Vector3 GetLinearAcceleration (SteeringContext me, GameObject target /* add extra parameters (target?) if required */)
        {
            /* COMPLETE this method. It must return the linear acceleration (Vector3) */
          

            return Seek.GetLinearAcceleration(me, target);
        }

        public override float GetAngularAcceleration()
        {
            return SeekAndSpin.GetAngularAcceleration(Context);
        }

        public static float GetAngularAcceleration(SteeringContext me)
        {
            float result = me.maxAngularSpeed;
            
            return result;
        }

    }
}