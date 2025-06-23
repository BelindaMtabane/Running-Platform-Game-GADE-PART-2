using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu; // Assign in Inspector
    [SerializeField] private GameObject ScorePanel;
    [SerializeField] private GameObject loginPanel;
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);//This will trsnform the screen into the game scene
    }
    public void MainScreen()
    {
        mainMenu.SetActive(true); // Show the main menu
        ScorePanel.SetActive(false); // Hide the Score Panel
        loginPanel.SetActive(false); // Hide the login panel
    }
    public void LeaderBoard()
    {
        mainMenu.SetActive(false); // Hide the main menu
        ScorePanel.SetActive(true); // Show the Score Panel
    }
    public void closeLeaderBoard()
    {
        mainMenu.SetActive(true); // Show the main menu
        ScorePanel.SetActive(false); // Hide the Score Panel
        loginPanel.SetActive(false); // Hide the login panel
    }
    public void Login()
    {
        loginPanel.SetActive(true); // Show the login panel
        ScorePanel.SetActive(false);
        mainMenu.SetActive(false); // Hide the main menu
    }
    public void ExitGame()
    {
        Application.Quit();//This will allow the Plaer to exit the screen
    }
}
