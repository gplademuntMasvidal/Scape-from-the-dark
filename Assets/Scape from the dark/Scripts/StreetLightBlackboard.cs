using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLightBlackboard : MonoBehaviour
{
    public float m_playerDetectedRadius = 3;
    public GameObject m_thePlayer;
    public GameObject m_theLight;
    public float m_timer = 0;
    public float m_timeToTurnOff = 5;
    public float m_timeToTurnOn = 10;
    public float m_inLightDistance = 10;

    public bool m_streetLightOn;

    private void Start()
    {
        m_streetLightOn = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.gameObject.layer == LayerMask.NameToLayer("LIGHT"))
        {
            Debug.Log("L'objecte LIGHT ha entrat al trigger!");
        }
        else
        {
            Debug.Log("Un altre objecte ha intentat entrar.");
        }
    }


}
