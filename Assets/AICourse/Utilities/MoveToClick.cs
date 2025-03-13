using UnityEngine;

public class MoveToClick : MonoBehaviour, IRestartGameElement
{

	public bool rightClick = false;
    private int buttonNumber;
    private Vector2 m_initialPosition;

    void Start ()
    {
        if (rightClick) buttonNumber = 1;
        else buttonNumber = 0;
        GameManager.m_instanceGameManager.AddRestartGameElement(this);
        m_initialPosition = transform.position;
    }

    public void MoveTarget()
    {
        Vector3 click = Input.mousePosition;
        Vector3 wantedPosition = Camera.main.ScreenToWorldPoint(new Vector3(click.x, click.y, 1f));
        wantedPosition.z = transform.position.z;
        transform.position = wantedPosition;
    }

    public void RestartGame()
    {
        transform.position = m_initialPosition;
    }
}
