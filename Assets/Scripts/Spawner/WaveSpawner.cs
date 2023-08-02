using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[System.Serializable]
public class Wave
{
    public string stageName;
    public int numOfGuns;
    public GameObject[] gunType;
    public float spawnInterval;
}
public class WaveSpawner : MonoBehaviour
{
    [SerializeField] Wave[] stages;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject roundFinished;
    [SerializeField] Health[] players;

    GameObject[] guns;
    Wave currentWave;
    int currentWaveNUmber;

    bool CanSpawn = true;
    float nextSpawnTime;


    

    private void Update()
    {
        var playerOneHealth = players[0].GetComponent<Health>().currentHealth;
        var playerTwoHealth = players[1].GetComponent<Health>().currentHealth;


        currentWave = stages[currentWaveNUmber];
        SpawnWave();

        guns = GameObject.FindGameObjectsWithTag("Weapon");
        GameObject[] gunsInstance = guns;



        if (playerOneHealth == 0|| playerTwoHealth == 0)
        {

            playerOneHealth = 100;
            playerTwoHealth = 100;
            Debug.Log("a player died");
            foreach (GameObject gun in gunsInstance)
            {
                Destroy(gun);
            }


        }
    }

    void SpawnNextWave()
    {
        currentWaveNUmber++;
        CanSpawn = true;

    }

    void SpawnWave()
    {
        if (CanSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomGun = currentWave.gunType[Random.Range(0, currentWave.gunType.Length)];
            Transform randomPoint = spawnPoint;
            Instantiate(randomGun, randomPoint.position, Quaternion.identity);
            currentWave.numOfGuns--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.numOfGuns == 0)
            {
                CanSpawn = false;
            }
        }
    }



    //void CompleteLevel()
    //{
    //    int currentLevel = SceneManager.GetActiveScene().buildIndex;

    //    if(currentLevel >= PlayerPrefs.GetInt("levelAt"))
    //    {
    //        PlayerPrefs.SetInt("levelAt", currentLevel);
    //    }
    //    Debug.Log("level " + PlayerPrefs.GetInt("levelAt") + " is unlocked");
    //}

    //IEnumerator EndOfLevelChoices()
    //{
    //    if (isLevelThree)
    //    {
    //        yield return new WaitForSeconds(1.8f);
    //        SceneManager.LoadScene("GAME COMPLETE");
    //    }
    //    if (!isLevelThree)
    //    {
    //        yield return new WaitForSeconds(1.5f);
    //        SceneManager.LoadScene(nextSceneLoad);
    //    }
    //}
}
