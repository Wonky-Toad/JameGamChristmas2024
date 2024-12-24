using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public PC_Movement sc_pc_movement;
    public GameObject go_canvas; 
    public GameObject go_endlevel_panel;
    public GameObject go_EndText;

    public int in_level_index;

    public float fl_victory_time = 0;
    public float fl_end_time = 0;

    public float fl_current_time = 0;

    public string st_vic_time_msg = "Time to beat: ";
    public UnityEngine.UI.Text T_vicTime;
    public UnityEngine.UI.Text T_Time_end;


    // Start is called before the first frame update
    void Start()
    {
        // if nothing attached to script, find the game object with PC tag and get PC_move script , so that we can use the time variables 
        if (sc_pc_movement == null) { sc_pc_movement = GameObject.FindWithTag("PC").GetComponent<PC_Movement>(); }

        // get scene index to be used to set vic time
        in_level_index = SceneManager.GetActiveScene().buildIndex;
        
        // if nothing attached
        if(go_canvas == null) 
        { // find gameobject with canvas as needed as main parent 
            go_canvas = GameObject.Find("Canvas"); 
        }
        // if no end game pnel ref, make one using child find of canvas , .gameobject 
        if (go_endlevel_panel == null) { go_endlevel_panel = go_canvas.transform.Find("End_Level_Panel").gameObject;  }


        // set victory time based of level index 
        if(in_level_index == 1) { fl_victory_time = 25.00F;  }
        else if (in_level_index == 2) { fl_victory_time = 17.50F; }
        else if (in_level_index == 3) { fl_victory_time = 40.00F; }

        T_vicTime.text = (st_vic_time_msg+fl_victory_time.ToString("F2"));

    }

    // Update is called once per frame
    void Update()
    {
        // take the time from pc move
        fl_current_time = sc_pc_movement.fl_lvl_time; 
        
        // round the current time to 2dp like string on ui, only gives a 0.01/0.02 margin or error between this float and time on screen 
        fl_current_time = Mathf.Round(fl_current_time* 100f) / 100f;

        if(sc_pc_movement.bl_LevelEnded == true)
        {
            EndPanel();
        }

        T_Time_end.text = fl_current_time.ToString("F2");
    }

    public void RestartLevel() 
    {
        //load the current level index to reset scene for retart 

        SceneManager.LoadScene(in_level_index);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(in_level_index+1);
    }

    // 0 is main menu
    public void BackToMenu() { SceneManager.LoadScene(0); }

    public void EndPanel()
    {
        // make panel active to see
        go_endlevel_panel.SetActive(true);
        // decide which msg shown based off victory time 
        if (fl_current_time > fl_victory_time)
        {
            go_EndText = go_endlevel_panel.transform.Find("Try_Again_Text").gameObject; ;
        }
        else if (fl_current_time <= fl_victory_time)
        {
            go_EndText = go_endlevel_panel.transform.Find("Well_Done_Text").gameObject;
        }
        go_EndText.SetActive(true);
    }
}
