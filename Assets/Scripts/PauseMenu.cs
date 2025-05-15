using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;//Pause menu game object

    public void PauseGame()
    {
        Time.timeScale = 0; // Time to stop game play
        Debug.Log("The game pauses");
        pauseMenu.SetActive(true);//This will make the pause menu appear
    }
    public void HomePage()
    {
        Time.timeScale = 1; // Time to normal
        SceneManager.LoadScene("SplashScreen");//Load the home page
    }
    public void ResumeGame()
    {
        Time.timeScale = 1; // Time to normal
        pauseMenu.SetActive(false);//Resume the game
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//Restart the game
        Time.timeScale = 1; // Time to normal
    
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
