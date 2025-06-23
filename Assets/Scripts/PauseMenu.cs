using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;//Pause menu game object
    [SerializeField] private GameObject controlMenu;
    [SerializeField] private GameObject levelMenu;

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
    public void restartlevelTwo()
    {
        SceneManager.LoadScene("Level2Scene");//Restart level two
        Time.timeScale = 1; // Time to normal
    }
    public void playleveltwo()
    {
        SceneManager.LoadScene("Level2Scene");//Load level two
        Time.timeScale = 1; // Time to normal
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//Restart the game
        Time.timeScale = 1; // Time to normal
    
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
    public void ControlPanel()
    {
        Time.timeScale = 0; // Time to stop
        controlMenu.SetActive(true);//Load the control panel
        pauseMenu.SetActive(false);//Hide the pause menu
    }
    public void CloseControlPanel()
    {
        Time.timeScale = 0; // Time to normal
        controlMenu.SetActive(false);//Hide the control panel
        pauseMenu.SetActive(true);//Show the pause menu again
    }
    public void LevelMenu()
    {
        Time.timeScale = 0; // Time to stop
        levelMenu.SetActive(true);//Load the level menu
        pauseMenu.SetActive(false);//Hide the pause menu
    }
    public void RestartLevelMenu() 
    {
        Time.timeScale = 0; // Time to normal
        levelMenu.SetActive(false);//Hide the level menu
        pauseMenu.SetActive(true);//Show the pause menu again
    }
    /*public void playerStats()
    {
        Time.timeScale = 0; // Time to stop
        playerStats.SetActive(true);//Load the player stats
        pauseMenu.SetActive(false);//Hide the pause menu
    }*/
    public void levelOne()
    {
        levelMenu.SetActive(false);
        SceneManager.LoadScene("SampleScene");//Load level one
        Time.timeScale = 1; // Time to normal
    }
    public void levelTwo()
    {
        levelMenu.SetActive(false);
        SceneManager.LoadScene("Level2Scene");//Load level two
        Time.timeScale = 1; // Time to normal
    }
}
