using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SummaryManager : MonoBehaviour
{
    public TextMeshProUGUI survivalTimeText; // TMP text for displaying survival time
    public TextMeshProUGUI enemiesKilledText; // TMP text for displaying enemies killed
    public Button continueButton; // Button to go back to the main menu
    public PlayerCharacter playerCharacter;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        // Retrieve and display stored values
        survivalTimeText.text = SessionData.ElapsedTime.ToString("F2") + " seconds";
        enemiesKilledText.text = SessionData.EnemiesKilled.ToString();

        // Setup button listener
        continueButton.onClick.AddListener(GoToMainMenu);
    }

    // Method to load the Main Menu scene
    void GoToMainMenu()
    {
        SessionData.Reset();
        SceneManager.LoadScene("Main Menu");
    }
}
