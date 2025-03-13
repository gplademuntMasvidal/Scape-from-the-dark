
using UnityEngine;

namespace Steerings
{
    public class VelocityMatching : SteeringBehaviour
    {

        public GameObject target;

        public override GameObject GetTarget()
        {
            return target;
        }

        public override Vector3 GetLinearAcceleration()
        {
            return VelocityMatching.GetLinearAcceleration(Context, target);
        }

        public static Vector3 GetLinearAcceleration(SteeringContext me, GameObject target)
        {
            // velocity of target required. Let's get if from its Steering context
            SteeringContext targetContext = target.GetComponent<SteeringContext>();
            if (targetContext == null)
            {
                Debug.LogWarning("Velocity Matching invoked with a target " +
                                  "that has no context attached. Zero acceleration returned");
                return Vector3.zero;
                
            }

            Vector3 requiredAcceleration = (targetContext.velocity - me.velocity) 
                                           / me.timeToDesiredSpeed; // el resultat d'això és l'acc que necessita l'objecte per aconseguir la velocitat del target en el temps desitjat
            
            if (requiredAcceleration.magnitude > me.maxAcceleration)
                requiredAcceleration = requiredAcceleration.normalized * me.maxAcceleration;

            return requiredAcceleration;
        }
    }
}
