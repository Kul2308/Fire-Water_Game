using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Optional UI")]
    public GameObject winPanel;
    public GameObject losePanel;
    public TMP_Text gemText;
    public TMP_Text winGemText;

    [Header("Level flow")]
    public string nextSceneName = "";

    [Header("Audio")]                       // NEW
    public AudioClip gemSound;              // NEW 
    public AudioClip winSound;              // NEW 
    public AudioClip loseSound;             // NEW 
    [Range(0f, 1f)] public float volume = 0.8f;   // NEW

    int gems, totalGems;
    bool fireAtDoor, waterAtDoor, won, dead;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    void Start()
    {
        totalGems = FindObjectsByType<Collectible>(FindObjectsSortMode.None).Length;
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) Restart();
        if (won && Input.GetKeyDown(KeyCode.Space)) NextLevel();
        if (dead && Input.GetKeyDown(KeyCode.Space)) Restart();
    }

    // NEW: Sound
    void PlaySound(AudioClip clip)
    {
        if (clip != null && Camera.main != null)
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
    }

    public void AddGem()
    {
        gems++;
        UpdateUI();
        PlaySound(gemSound);                // NEW
    }

    public void SetAtDoor(Element e, bool inside)
    {
        if (e == Element.Fire) fireAtDoor = inside;
        else waterAtDoor = inside;

        if (fireAtDoor && waterAtDoor && !won && !dead) Win();
    }

    public void PlayerDied()
    {
        if (dead || won) return;
        dead = true;

        PlaySound(loseSound);               // NEW

        if (losePanel != null)
        {
            losePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Restart();
        }
    }

    void Win()
    {
        won = true;
        PlaySound(winSound);                // NEW

        if (winGemText != null)
        {
            winGemText.text = "Gems: " + gems + " / " + totalGems;
            if (totalGems > 0 && gems == totalGems) winGemText.text += "\nAll gems collected!";
        }

        if (winPanel != null) winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void NextLevel()
    {
        if (string.IsNullOrEmpty(nextSceneName)) Restart();
        else SceneManager.LoadScene(nextSceneName);
    }

    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    void UpdateUI()
    {
        if (gemText != null) gemText.text = "Gems: " + gems + " / " + totalGems;
    }
}