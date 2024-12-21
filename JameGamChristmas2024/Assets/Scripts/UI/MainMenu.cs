using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    //make public so button can find on object 
    public void StartGame()
    {
        //when called load scene Level 1
        SceneManager.LoadSceneAsync("Level_01");

    }

    public void QuitGame()
    {
        //below code quits game
            Application.Quit();
    }
}
