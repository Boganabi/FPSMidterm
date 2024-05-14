using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public TextMeshProUGUI survivalTimeText; // TMP text for displaying survival time
    public TextMeshProUGUI enemiesKilledText; // TMP text for displaying enemies killed
    public Button continueButton; // Button to go back to the main menu

    void Start()
    {
        // Retrieve and display stored values
        survivalTimeText.text = PlayerPrefs.GetFloat("SurvivalTime").ToString("F2") + " seconds";
        enemiesKilledText.text = PlayerPrefs.GetInt("EnemiesKilled");

        // Setup button listener
        continueButton.onClick.AddListener(GoToMainMenu);
    }

    // Method to load the Main Menu scene
    void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
