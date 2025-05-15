using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu; // Assign in Inspector
    [SerializeField] private GameObject optionsPanel;
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);//This will trsnform the screen into the game scene
    }

    public void ExitGame()
    {
        Application.Quit();//This will allow the Plaer to exit the screen
    }
}
