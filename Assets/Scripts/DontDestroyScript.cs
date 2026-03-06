using UnityEngine;

public class DontDestroyScript : MonoBehaviour
{
    public static DontDestroyScript Instance;

    public Canvas pauseUi;
    public Canvas settingsUi;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(pauseUi);
        DontDestroyOnLoad(settingsUi);
    }
}
