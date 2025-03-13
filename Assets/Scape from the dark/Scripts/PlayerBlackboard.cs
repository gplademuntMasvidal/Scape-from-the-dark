using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackboard : MonoBehaviour, IRestartGameElement
{
    public float m_lightDetectedRadius = 3;
    public GameObject m_theLight;
    public GameObject m_theMonster;

    public bool m_playerIsSafe;

    public Vector3 m_initialPosition;

    private void Start()
    {
        GameManager.m_instanceGameManager.AddRestartGameElement(this);
        m_initialPosition = transform.position;
    }

    public void Die()
    {
        gameObject.SetActive(false);
        GameManager.m_instanceGameManager.GameOver();
    }

    public void RestartGame()
    {
        gameObject.SetActive(true);
        transform.position = m_initialPosition;
    }
}
