using UnityEngine;

public class MonsterBlackboard : MonoBehaviour, IRestartGameElement
{
    public float m_playerDetectedRadius = 3;
    public GameObject m_thePlayer;
    public GameObject m_leavingPoint;
    public Vector3 m_initialPosition;

    private void Start()
    {
        GameManager.m_instanceGameManager.AddRestartGameElement(this);
        m_initialPosition = transform.position;
    }

    public void RestartGame()
    {
        gameObject.SetActive(true);
        transform.position = m_initialPosition;
    }
}
