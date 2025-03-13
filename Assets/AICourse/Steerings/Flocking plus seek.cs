
using UnityEngine;

namespace Steerings
{
    public class Flockingplusseek : SteeringBehaviour
    {

        private GameObject m_Target;
        public float distance;

        private void Start()
        {
            m_Target = GameObject.FindGameObjectWithTag("Target");

        }
        public override Vector3 GetLinearAcceleration()
        {
            return Flockingplusseek.GetLinearAcceleration(Context, m_Target, distance);
        }

        public static Vector3 GetLinearAcceleration(SteeringContext me, GameObject m_Target, float distance)
        {
            Vector3 seekAcc = Seek.GetLinearAcceleration(me, m_Target);

            if (SensingUtils.DistanceToTarget(me.gameObject, m_Target) >= distance)
                return Flocking.GetLinearAcceleration(me);
            else
                return seekAcc;
        }
    }
}
