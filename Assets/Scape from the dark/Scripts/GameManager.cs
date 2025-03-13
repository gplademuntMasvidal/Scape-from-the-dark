using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public interface IRestartGameElement
{
    void RestartGame();
}
public class GameManager : MonoBehaviour
{
    public static GameManager m_instanceGameManager;
    public MoveToClick m_moveToClick;
    public GameObject m_oneTorch;

    //UI panels
    public GameObject m_startMenuUI;
    public GameObject m_pauseMenuUI;
    public GameObject m_gameOverUI;
    public GameObject m_victoryUI;
    public TextMeshProUGUI m_gameCounter;

    private float m_counter;
    private bool m_revised;
    public LayerMask m_streetLightLayer;

    //public bool m_streetLightOn;

    List<IRestartGameElement> m_RestartGameElements = new List<IRestartGameElement>();


    private void Awake()
    {
        if (m_instanceGameManager == null)
        {
            m_instanceGameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameSettings();
        OpenStartMenu();
        m_counter = 0;
        m_revised = false;
        m_gameCounter.gameObject.SetActive(false);
        //m_streetLightOn = false;

    }

    private void Update()
    {
        m_counter += Time.deltaTime;

        int l_minutes = Mathf.FloorToInt(m_counter / 60);
        int l_seconds = Mathf.FloorToInt(m_counter % 60);

        m_gameCounter.text = string.Format("{0:00}:{1:00}", l_minutes, l_seconds);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (l_minutes == 1 && m_revised == false) { Victory(); }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, m_streetLightLayer);

            if (hit.collider != null && hit.collider.CompareTag("streetLight"))
            {
                StreetLightBlackboard l_SL = hit.collider.GetComponent<StreetLightBlackboard>();
                if (l_SL != null && m_oneTorch == null)
                {
                    l_SL.m_streetLightOn = true;
                    m_oneTorch = l_SL.gameObject;
                    m_moveToClick.MoveTarget();
                }
            }
            if (hit.collider != null && hit.collider.CompareTag("door"))
            {
                Door door = hit.collider.GetComponent<Door>();
                if (door != null)
                {
                    door.ToggleDoor();
                }

            }

        }

    }



    public void OpenStartMenu()
    {
        Time.timeScale = 0.0f;
        m_victoryUI.SetActive(false);
        m_gameCounter.gameObject.SetActive(false);
        m_gameOverUI.SetActive(false);
        m_pauseMenuUI.SetActive(false);
        m_startMenuUI.SetActive(true);
    }



    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        m_victoryUI.SetActive(false);
        m_gameOverUI.SetActive(false);
        m_startMenuUI.SetActive(false);
        m_pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        m_gameCounter.gameObject.SetActive(true);
        m_victoryUI.SetActive(false);
        m_gameOverUI.SetActive(false);
        m_startMenuUI.SetActive(false);
        m_pauseMenuUI.SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        m_victoryUI.SetActive(false);
        m_pauseMenuUI.SetActive(false);
        m_startMenuUI.SetActive(false);
        m_gameOverUI.SetActive(true);
    }

    public void Victory()
    {
        // Mostrem el menú de victòria
        m_revised = true;
        Time.timeScale = 0.0f;
        m_pauseMenuUI.SetActive(false);
        m_startMenuUI.SetActive(false);
        m_gameOverUI.SetActive(false);
        m_victoryUI.SetActive(true);

        StartCoroutine(VictoryCoroutine());
    }

    private IEnumerator VictoryCoroutine()
    {
        yield return new WaitForSecondsRealtime(5f);

        m_victoryUI.SetActive(false);

        OpenStartMenu();
    }
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    private void GameSettings()
    {
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }




    public void AddRestartGameElement(IRestartGameElement RestartGameElement)
    {
        m_RestartGameElements.Add(RestartGameElement);
    }

    public void RestartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        m_revised = false;
        m_victoryUI.SetActive(false);
        m_gameOverUI.SetActive(false);
        m_startMenuUI.SetActive(false);
        m_pauseMenuUI.SetActive(false);
        m_counter = 0;
        m_gameCounter.gameObject.SetActive(true);
        Time.timeScale = 1.0f;
        m_oneTorch = null;

        foreach (IRestartGameElement l_RestartGameElement in m_RestartGameElements)
        {
            l_RestartGameElement.RestartGame();
        }


    }
}
