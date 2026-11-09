using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Optional UI")]
    public GameObject winPanel;      // a panel that says "Level complete! Press Space"
    public TMP_Text gemText;         // live counter in the corner
    public TMP_Text winGemText;      // gem result shown on the level complete panel

    [Header("Level flow")]
    public string nextSceneName = "";   // leave empty to just restart the level

    int gems, totalGems;
    bool fireAtDoor, waterAtDoor, won;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    void Start()
    {
        // Count every gem placed in the level (after all objects have woken up).
        totalGems = FindObjectsByType<Collectible>(FindObjectsSortMode.None).Length;
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) Restart();
        if (won && Input.GetKeyDown(KeyCode.Space)) NextLevel();
    }

    public void AddGem()
    {
        gems++;
        UpdateUI();
    }

    public void SetAtDoor(Element e, bool inside)
    {
        if (e == Element.Fire) fireAtDoor = inside;
        else waterAtDoor = inside;

        if (fireAtDoor && waterAtDoor && !won) Win();
    }

    public void PlayerDied() => Restart();

    void Win()
    {
        won = true;

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

    void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    void UpdateUI()
    {
        if (gemText != null) gemText.text = "Gems: " + gems + " / " + totalGems;
    }
}
