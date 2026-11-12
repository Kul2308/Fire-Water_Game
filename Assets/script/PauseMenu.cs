using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject pausePanel;      // panel with the buttons (starts disabled)

    [Header("Don't allow pause while these are showing")]
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Scenes")]
    public string homeSceneName = "MainMenu";

    [Header("Audio")]
    public bool pauseSoundsToo = false;   // true = also silence music while paused

    bool paused;

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        // ignore Esc on the win / lose screens
        if ((winPanel != null && winPanel.activeInHierarchy) ||
            (losePanel != null && losePanel.activeInHierarchy)) return;

        if (paused) Resume();
        else Pause();
    }

    public void Pause()
    {
        paused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        if (pauseSoundsToo) AudioListener.pause = true;
    }

    // Public so UI Buttons can call them
    public void Resume()
    {
        paused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(homeSceneName);
    }
}