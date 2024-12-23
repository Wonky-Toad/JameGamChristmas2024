using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings_Menu : MonoBehaviour
{

    public Dropdown dd_resolutionDropDown;


    // create resolution array
    Resolution[] resolutions;

    void Start()
    {
        // store resolutions possible in array
        resolutions = Screen.resolutions;
        // clear pre set opptions
        dd_resolutionDropDown.ClearOptions();

        // create new list of strings with options
        List<string> options = new List<string>();

        // for loop will cycle through all elements to make every options 
        for(int i = 0; i < resolutions.Length; i++)
        {
            //create new string for each resolution with width X length 
            string option = resolutions[i].width + " X " + resolutions[i].height ;
            options.Add(option);
        }

        // add new options as string list
        dd_resolutionDropDown.AddOptions(options);
    }


    public void SetFullscreen(bool isFullscreen) 
    { 
        Screen.fullScreen = isFullscreen;  
    
    }
}
