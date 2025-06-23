using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelGenerate : MonoBehaviour
{
    public GameObject deathMenu; // assign via inspector
    bool hasReset = false; // Add this at the top of the class
    int loopCount = 0; // Counter to track the number of times the player has reached the portal
    public static int levelBeat;
    internal int beatenScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        hasReset = false;
    }
    void Update()
    {
        if (IsDeathMenuOpen() && !hasReset)
        {
            loopCount = 0;
            hasReset = true; // Prevent multiple reloads
            Debug.Log("Loop count reset due to death menu (Update).");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Make sure player has "Player" tag
        {
            Debug.Log("Player reached portal!");

            if (loopCount == 0)
            {
                SceneManager.LoadScene("Level2Scene"); // Level 2 scene name
                loopCount++; // Increment the loop count

                Time.timeScale = 1;
            }
            else if (loopCount == 1)
            {
                int levelToLoad = GenerateRandomLevel(); // Get random level (1 or 2)

                if (levelToLoad == 1)
                    SceneManager.LoadScene("SampleScene"); // Level 1 scene name
                else if (levelToLoad == 2)
                    SceneManager.LoadScene("Level2Scene"); // Level 2 scene name

                Time.timeScale = 1;
            }
            levelBeat++; // Increment the level beat count
        }
    }
    public bool IsDeathMenuOpen()
    {
        return deathMenu != null && deathMenu.activeSelf;
    }
    public int GenerateRandomLevel()
    {
        int levelPlay = Random.Range(1, 3); // 1 or 2
        Debug.Log("Randomly selected level: " + levelPlay);
        return levelPlay;
    }
}
