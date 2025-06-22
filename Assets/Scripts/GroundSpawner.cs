using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    /*public GameObject segmentMap01;
    public GameObject segmentMap02;
    public GameObject segmentMap03;
    public GameObject segmentMap04;
    public GameObject segmentMap05;
    public GameObject segmentMap06;
    public GameObject segmentMap07;
    public GameObject segmentMap08;
    public GameObject segmentMap09;
    public GameObject segmentMap10;
    public GameObject segmentMap11;

    void Start()
    {
        StartCoroutine(SpawnTile());
    }*/
    /* public IEnumerator SpawnTile()
     {
         yield return new WaitForSeconds(1);
         segmentMap01.SetActive(true);
         yield return new WaitForSeconds(2);
         segmentMap02.SetActive(true);
         yield return new WaitForSeconds(3);
         segmentMap03.SetActive(true);
         yield return new WaitForSeconds(3);
         segmentMap04.SetActive(true);
         yield return new WaitForSeconds(4);
         segmentMap05.SetActive(true);
         yield return new WaitForSeconds(4);
         segmentMap06.SetActive(true);
         yield return new WaitForSeconds(5);
         segmentMap07.SetActive(true);
         yield return new WaitForSeconds(5);
         segmentMap08.SetActive(true);
         yield return new WaitForSeconds(6);
         segmentMap09.SetActive(true);
         Destroy(segmentMap01);
         Destroy(segmentMap02);
         Destroy(segmentMap03);
         yield return new WaitForSeconds(6);
         segmentMap10.SetActive(true);
         Destroy(segmentMap04);
         Destroy(segmentMap05);
         Destroy(segmentMap06);
         yield return new WaitForSeconds(7);
         segmentMap11.SetActive(true);
         Destroy(segmentMap07);
         Destroy(segmentMap08);
         Destroy(segmentMap09);


     }*/

    public static GroundSpawner Instance; // Singleton instance of GroundSpawner
    [SerializeField] GameObject groundTilePrefab; // Prefab for the ground tile
   // [SerializeField] GameObject spawnGameWorldPrefab1; // Prefab for the spawn game world
   // [SerializeField] GameObject spawnGameWorldPrefab2;
   // [SerializeField] GameObject spawnGameWorldPrefab3;
   // [SerializeField] GameObject repeatedSpawnGameWorldPrefab1;
   // [SerializeField] GameObject repeatedSpawnGameWorldPrefab2;
   // [SerializeField] GameObject repeatedSpawnGameWorldPrefab3;
    Vector3 nextSpawnPosition; // Position to spawn the next ground tile
    public int LevelChange = 0; // Variable to track the level change

    /*public void SpawnTile()
    {
        GameObject tempObj = Instantiate(groundTilePrefab, nextSpawnPosition, Quaternion.identity); // Instantiate the ground tile prefab
        nextSpawnPosition = tempObj.transform.GetChild(1).transform.position; // Update the next spawn position
    }*/
    void Awake()
    {
        Instance = this;// Assign the singleton instance to be used laterby other scripts
    }
    public void SpawnTile()
    {
        GameObject tempObj = Instantiate(groundTilePrefab, nextSpawnPosition, Quaternion.identity); // Instantiate the ground tile prefab
        nextSpawnPosition = tempObj.transform.GetChild(1).transform.position; // Update the next spawn position
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        for (int i = 0; i < 11; i++) // Spawn 10 ground tiles
        {
            i = i + LevelChange; // Increment the level change
            SpawnTile();// Spawn the ground first
        }
    }
    public int levelGenarator()
    {
        if (LevelChange == 11) // Check if the level change is less than 10
        {
            int levelPlay =  Random.Range(0, 3); // Generate a random number between 0 and 3
            Debug.Log("Level next to Play is: Level " + levelPlay); // Log the level change and generated level play
            return levelPlay; // Return the updated level change
        }
        Debug.Log("LevelGenerator was called but LevelChange is not 11");
        return -1;
    }

    //[SerializeField] GameObject groundTilePrefab; // Prefab for the ground tile
    // Vector3 nextSpawnPosition; // Position to spawn the next ground tile

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*void Start()
    {
        for (int i = 0; i < 5; i++) // Spawn 5 ground tiles
        {
            SpawnTile();
        }
    }*/



}
