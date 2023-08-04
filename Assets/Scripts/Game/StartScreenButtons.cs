using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenButtons : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LevelOne");
    }

<<<<<<< Updated upstream
    public void Quit()
=======
    public void HomeScreen()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void LeaveGame()
>>>>>>> Stashed changes
    {
        Application.Quit();
    }

<<<<<<< Updated upstream
    public void Reset()
    {
        SceneManager.LoadScene("StartScene");
    }
=======
>>>>>>> Stashed changes
}
