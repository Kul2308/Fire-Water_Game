using UnityEngine;

public class QuitButton : MonoBehaviour
{
    // Public so a UI Button can call it
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;   // stop Play mode in the editor
#else
        Application.Quit();
#endif
    }
}