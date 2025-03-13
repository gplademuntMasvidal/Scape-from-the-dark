using Steerings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackboardEnemies : MonoBehaviour
{
    public GameObject m_target;


    //KeepPositionP1 variables
    public float m_closeToPlayer = 70;
    public float m_formationRadius = 70f;
    public int m_formationIndex = 0;
    public int m_totalEnemies = 30;


    public float m_originalSpeed;
    public float m_originalRepulsionThreshold;
    //
    /*public float maxSpeed;
    public float maxAcceleration;
    public float cohesionThreshold;
    public float repulsionThreshold;
    public float coneOfVisionAngle;
    public float cohesionWeight;
    public float repulsionWeight;
    public float alignmentWeight;
    public float seekWeight;*/

    void Start()
    {
        m_target = GameObject.FindGameObjectWithTag("Target");
        m_originalSpeed = GetComponent<SteeringContext>().maxSpeed;
        m_originalRepulsionThreshold = GetComponent<SteeringContext>().repulsionThreshold;
    }


}
