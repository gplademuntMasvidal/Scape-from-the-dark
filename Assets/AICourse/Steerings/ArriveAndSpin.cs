using UnityEngine;

namespace Steerings
{

    public class ArriveAndSpin : SteeringBehaviour
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
            return ArriveAndSpin.GetLinearAcceleration(Context, target /* add extra parameters (target?) if required */);
        }


        public static Vector3 GetLinearAcceleration(SteeringContext me, GameObject target /* add extra parameters (target?) if required */)
        {
            /* COMPLETE this method. It must return the linear acceleration (Vector3) */
            return Arrive.GetLinearAcceleration(me, target);
        }

        public override float GetAngularAcceleration()
        {
            return ArriveAndSpin.GetAngularAcceleration(Context, target);
        }

        public static float GetAngularAcceleration(SteeringContext me, GameObject target)
        {
            Vector3 l_DirectionToTarget = target.transform.position - me.transform.position;
            float l_DistanceToTarget = l_DirectionToTarget.magnitude;

            if (l_DistanceToTarget <= me.closeEnoughRadius)
            {
                return 0;
            }

            else if (l_DistanceToTarget >= me.slowdownRadius)
            {
                return me.maxAngularSpeed;

            }

            float l_DesiredRotationSpeed = me.maxAngularSpeed * (l_DistanceToTarget / me.slowdownRadius);
            Debug.Log($"Angular Speed: {l_DesiredRotationSpeed}, Distance: {l_DistanceToTarget}");

            if (l_DesiredRotationSpeed > me.maxAngularSpeed) { l_DesiredRotationSpeed = me.maxAngularSpeed; }

            return l_DesiredRotationSpeed; // Retorna la velocitat angular ajustada

        }

    }
}