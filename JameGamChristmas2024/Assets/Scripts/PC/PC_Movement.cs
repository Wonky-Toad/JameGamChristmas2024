using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Movement : MonoBehaviour
{
    public float fl_speed = 5;
    public float fl_speed_multiplier = 1;
    float fl_speed_multiplier_history = 0;
    float fl_gravity = 2;
    public float fl_lvl_time;

    public bool bl_startTimer = false;
    public bool bl_crashed = false;
    public bool bl_accelboostCoroutine_isRunning = false;
    public bool bl_LevelEnded = false; 

    public bool bl_LevelStarted = false;

    public Vector3 v3_move_direction = Vector3.zero;
    public CharacterController cc_PC;
    public GameObject go_canvas;

    public UnityEngine.UI.Text T_levelTime;
    public UnityEngine.UI.Text T_AccelerationBoostNumber;

    public int in_acceleration_boost_number = 0;

    public string st_current_time_msg = "Current Time: ";

    public GameObject go_ControlPanel;

    public GameObject ca_main;
    public GameObject ca_end;

    // Start is called before the first frame update
    void Start()
    {
        cc_PC = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!bl_LevelStarted)
        {
            StartCoroutine(GameStart());
        }
        if(bl_LevelStarted) 
        {
            MovePC();
            //if coroutine is not already running (bl will be false), and boost number is less than 10, call it, otherwise do not ,so it can have its delay or because the boost is full
            // 
            if (!bl_accelboostCoroutine_isRunning && in_acceleration_boost_number < 10) { StartCoroutine(IncreaseAccelNum()); }
        }

        // if bl_startTimer is true then start timer
        if (bl_startTimer)
        {
            // count the frames per second, so summing them gives each second
            fl_lvl_time = fl_lvl_time + Time.deltaTime;
            // log the seconds to UI text and converts to string ( with 2 decimals ) so it can be used and shown
            T_levelTime.text = st_current_time_msg + fl_lvl_time.ToString("F2");
          
        }


        // log the accel boost num  to UI text and converts to string // hopefully no decimals as int
        T_AccelerationBoostNumber.text = in_acceleration_boost_number.ToString();

        // if up cursor or W key pressed call accel boost func
        if(Input.GetKeyDown(KeyCode.W ) || Input.GetKeyDown(KeyCode.UpArrow)){ UseAccelerationBoost(); }
    }

    private void MovePC()
    {
        // Add X  movement to the direction vector based input axes (A,D or L/R Cursor) 
        v3_move_direction = new Vector3(Input.GetAxis("Horizontal"), 0, 1);


        // Convert world coordinates to local for the PC and multiply by speed
        v3_move_direction = fl_speed * fl_speed_multiplier * transform.TransformDirection(v3_move_direction);

        // Add fl_gravity to the direction vector - has been taken out for now
        // v3_move_direction.y -= fl_gravity * Time.deltaTime * fl_speed_multiplier;

        // Move the character controller with the direction vector
        cc_PC.Move(v3_move_direction * Time.deltaTime);

    }

    private IEnumerator GameStart() 
    {
        
        
        yield return new WaitForSeconds(2);
        if (Input.GetMouseButtonDown(0)) 
        {
            go_ControlPanel.SetActive(false);
            bl_LevelStarted = true;
        }



    }

    // IEmuerator allows the function to be called at a delay 
    private IEnumerator Crash()
    {
        //set multipler to 0 to bring to a halt 
        SetSpeedMulti(0);
        //set crashed to true so that accel num is not called if not moving 
        bl_crashed = true;
        // now delay for x secs
        yield return new WaitForSeconds(3);
        // now if multi if less than or equal to 2 then reset multi
        if (fl_speed_multiplier_history <= 2) { SetSpeedMulti(fl_speed_multiplier_history); }
        //otherwise half the speed multi history and reset
        else { SetSpeedMulti(fl_speed_multiplier_history / 2); }

        bl_crashed = false;

    }

    private void UseAccelerationBoost() 
    {
        // if player has 10 charges (accel boost num is 10)
        if(in_acceleration_boost_number >= 10)
        {
            // increase speed multi
            fl_speed_multiplier++;
            // reset boost
            in_acceleration_boost_number = 0;
        }
    }

    private IEnumerator IncreaseAccelNum()
    {
        // set runnign bool to true so that update only calls once during delay 
        bl_accelboostCoroutine_isRunning = true;
        
        
        
        // delay function 
        yield return new WaitForSeconds(2);

        //logic gate to stop when game over 

        if (!bl_LevelEnded)
        {
            // if the player is not crashed increase acecel boost charge number 
            if (!bl_crashed && in_acceleration_boost_number < 10) { in_acceleration_boost_number++; }
            // then set isRunning to false for update to call
            bl_accelboostCoroutine_isRunning = false;
        }

    }

    private void SetSpeedMulti(float newMulti)
    {
        fl_speed_multiplier = newMulti;
        Debug.Log("Current Speed Multiplier: " + fl_speed_multiplier);
    }
    private void OnTriggerStay(Collider cl_trigger_collider)
    {
        if (cl_trigger_collider.gameObject.tag == "FinishLine")
        {
            //output a message saying you win on console (testing)
            Debug.Log("You Win!");
            //stop PC for now
            SetSpeedMulti(0);

            //stop timer
            bl_startTimer = false;

            //stop boost
            bl_accelboostCoroutine_isRunning = true;

            // set ended level bool to true
            bl_LevelEnded = true;
        }
    }


    //A function called every frame like update that checks when collider enters a trigger
    void OnTriggerEnter(Collider cl_trigger_collider)
    {
        //when entering trigger connected to an object with tag FinishLine 
        if(cl_trigger_collider.gameObject.tag == "FinishLine") 
        {
            //output a message saying you win on console (testing)
            Debug.Log("You Win!");
            //stop PC for now
            //SetSpeedMulti(0);

            // swap camera
            ca_main.SetActive(false);
            ca_end.SetActive(true);


            //stop timer
            bl_startTimer = false;

            //stop boost
            bl_accelboostCoroutine_isRunning = true;

            // set ended level bool to true
            bl_LevelEnded = true;
        }

        //when enter object with SpeedBoost tag, increase speed multiplier to increase speed of PC
        if(cl_trigger_collider.gameObject.tag == "SpeedBoost") 
        {
            Debug.Log("Past Speed Multiplier: " + fl_speed_multiplier);
            fl_speed_multiplier++;
            Debug.Log("Current Speed Multiplier: " + fl_speed_multiplier);
            //also increase 1 charge of accel boost num if has less than 10 charges 
            if (in_acceleration_boost_number<10) { in_acceleration_boost_number++; }

        }
        //when enter obstacle trigger,  
        if (cl_trigger_collider.gameObject.tag == "Obstacle")
        {
            //output last multiplier to console, for debug n test
            Debug.Log("Past Speed Multiplier: " + fl_speed_multiplier);
            //set fl speed multi history to last speed so we can adjust it when pc moves again
            fl_speed_multiplier_history = fl_speed_multiplier;
            //call crash function , use start coroutine to have delay 
            if (!bl_crashed) { StartCoroutine(Crash()); }
        }

        if(cl_trigger_collider.gameObject.tag == "StartLine")
        {
            //stop update funcs for timer and accell boost when cross finish 
            bl_startTimer = true;
            bl_accelboostCoroutine_isRunning = true;
        }

    }



}
