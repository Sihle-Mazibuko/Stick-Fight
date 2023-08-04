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
    public GameObject map;
    public Transform gunSpawnPoint;
    //public Transform playerSpawnPoint;

}
public class WaveSpawner : MonoBehaviour
{
    [SerializeField] Wave[] stages;
    [SerializeField] Health[] players;

    GameObject[] guns;
    Wave currentWave;
    int currentWaveNUmber;

    bool CanSpawn = true;
    float nextSpawnTime;

    [SerializeField] Animator animator;
    [SerializeField] TextMeshProUGUI waveName;

    bool canAnimate = false;

    private void LateUpdate()
    {
        var playerOneHealth = players[0].GetComponent<Health>().currentHealth;
        var playerTwoHealth = players[1].GetComponent<Health>().currentHealth;


        currentWave = stages[currentWaveNUmber];
        SpawnWave();

        guns = GameObject.FindGameObjectsWithTag("Weapon");
        GameObject[] gunsInstance = guns;



        if ((playerOneHealth == 0|| playerTwoHealth == 0) && canAnimate && currentWaveNUmber+1 != stages.Length)
        {

            playerOneHealth += 100;
            playerTwoHealth += 100;

            foreach (GameObject gun in gunsInstance)
            {
                gun.SetActive(false);
            }
            Debug.Log("a player died");
            waveName.text = stages[currentWaveNUmber + 1].stageName;
            animator.SetTrigger("WaveDone");
            canAnimate = false;

        }
    }

    void SpawnNextWave()
    {
        currentWaveNUmber++;
        CanSpawn = true;
        currentWave.map.SetActive(false);


        foreach (var player in players)
        {
            //player.currentHealth = 100;
        }
    }

    void SpawnWave()
    {
        currentWave.map.SetActive(true);
        if (CanSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomGun = currentWave.gunType[Random.Range(0, currentWave.gunType.Length)];
            Transform randomPoint = currentWave.gunSpawnPoint;
            Instantiate(randomGun, randomPoint.position, Quaternion.identity);
            currentWave.numOfGuns--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.numOfGuns == 0)
            {
                CanSpawn = false;
                canAnimate = true;
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
