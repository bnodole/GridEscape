using UnityEngine;

public class DontDestroyScript : MonoBehaviour
{
    public static DontDestroyScript Instance;

    public Canvas pauseUi;
    public Canvas settingsUi;
    public GameObject gameManager;
    public GameObject canvaEvent;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(pauseUi);
        DontDestroyOnLoad(settingsUi);
        DontDestroyOnLoad(gameManager);
        DontDestroyOnLoad(canvaEvent);
    }
}
