using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[System.Serializable]
public class Wave
{
    public string stageName;
    public int numOfGuns;
    public int mapOptions;
    public GameObject[] gunType;
    //public GameObject map;
    public float spawnInterval;
}
public class WaveSpawner : MonoBehaviour
{
    [SerializeField] Wave[] stages;
    [SerializeField] Transform spawnPoint;
    [SerializeField] TextMeshProUGUI stageName;
    [SerializeField] GameObject roundFinished;
    [SerializeField] Health[] players;

    Wave currentWave;
    int currentWaveNUmber;

    bool CanSpawn = true;
    bool canAnimate = false;
    //[SerializeField]bool isLevelThree = false;
    float nextSpawnTime;

    //[SerializeField] int nextSceneLoad;

    [SerializeField] Animator animtor;
    //private void Start()
    //{
    //    nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    //}
    private void Update()
    {

        currentWave = stages[currentWaveNUmber];
        SpawnWave();

        GameObject[] guns = GameObject.FindGameObjectsWithTag("Weapon");
        if (!CanSpawn)
        {
            foreach (GameObject gun in guns)
            {
                Destroy(gun);
            }

            if (currentWaveNUmber + 1 != stages.Length)
            {
                stageName.text = stages[currentWaveNUmber + 1].stageName;

                if (canAnimate)
                {
                    stageName.text = stages[currentWaveNUmber + 1].stageName;
                    animtor.SetTrigger("WaveComplete");
                    canAnimate = false;
                }
            }
            else
            {
                //CompleteLevel();
                StartCoroutine(NextLevel());
                //StartCoroutine(EndOfLevelChoices());
                Debug.Log("level finished");


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
            if(currentWave.numOfGuns == 0)
            {
                CanSpawn = false;
                canAnimate = true;
            }
        }
    }

    IEnumerator NextLevel()
    {
        roundFinished.SetActive(true);
        yield return new WaitForSeconds(2);
        roundFinished.SetActive(false);
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
