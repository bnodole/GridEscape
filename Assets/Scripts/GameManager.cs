using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    int currentLevel;
    int nextLevel;
    bool isPaused;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        if(SceneManager.GetActiveScene().name != "Home")
        {
            PlayerScript.Instance.isGameStarted = true;
        }
        /*if(PlayerPrefs.GetInt("PlayerOngoingLevel") >= currentLevel + 1)
        {
            nextLevel = PlayerPrefs.GetInt("PlayerOngoingLevel");
        }
        else
        {
            nextLevel = currentLevel + 1;
        }*/
    }

    private void Update()
    {
        PlayPause();
    }

    public void LoadLevel()
    {
        if (SceneManager.GetActiveScene().name == "Home")
        {
            nextLevel = PlayerPrefs.GetInt("PlayerOngoingLevel");
            if(nextLevel < 1)
            {
                nextLevel = 1;
            }
        }
        else
        {
            PlayerPrefs.SetInt("PlayerOngoingLevel", SceneManager.GetActiveScene().buildIndex +1);
            nextLevel = PlayerPrefs.GetInt("PlayerOngoingLevel");
        }
        SceneManager.LoadScene(nextLevel);
    }

    public void PlayPause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused && PlayerScript.Instance.isGameStarted)
            {
                Time.timeScale = 0f;
                isPaused = true;
                DontDestroyScript.Instance.pauseUi.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1f;
                isPaused = false;
                DontDestroyScript.Instance.pauseUi.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void PlayButton()
    {
        Time.timeScale = 1f;
        PlayerScript.Instance.isGameStarted = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SettingsButton()
    {
        DontDestroyScript.Instance.settingsUi.gameObject.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HomeButton()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
