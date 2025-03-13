using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector2 m_ClosePosition;
    [SerializeField] private Transform m_OpenPosition;
    private bool isOpen = false;

    private void Start()
    {
        m_ClosePosition = transform.position;
    }

    private void Update()
    {
        if (isOpen)
        {
            transform.position = Vector2.Lerp(transform.position, m_OpenPosition.position, 1 * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.Lerp(transform.position, m_ClosePosition, 1 * Time.deltaTime);
        }
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
    }
}
