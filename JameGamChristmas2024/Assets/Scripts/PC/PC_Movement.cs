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

    public Vector3 v3_move_direction = Vector3.zero;
    public CharacterController cc_PC;
    public UnityEngine.UI.Text T_levelTime; 


    // Start is called before the first frame update
    void Start()
    {
        cc_PC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePC();
        // if bl_startTimer is true then start timer
        if (bl_startTimer)
        {
            // count the frames per second, so summing them gives each second
            fl_lvl_time = fl_lvl_time + Time.deltaTime;
            // log the seconds to UI text and converts to string ( with 2 decimals ) so it can be used and shown
            T_levelTime.text = fl_lvl_time.ToString("F2");
        }

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

    // IEmuerator allows the function to be called at a delay 
    private IEnumerator Crash()
    {
        //set multipler to 0 to bring to a halt 
        SetSpeedMulti(0);
        // now delay for x secs
        yield return new WaitForSeconds(3);
        // now if multi if less than or equal to 2 then reset multi
        if (fl_speed_multiplier_history <= 2) { SetSpeedMulti(fl_speed_multiplier_history); }
        //otherwise half the speed multi history and reset
        else { SetSpeedMulti(fl_speed_multiplier_history / 2); }

    }

    private void SetSpeedMulti(float newMulti)
    {
        fl_speed_multiplier = newMulti;
        Debug.Log("Current Speed Multiplier: " + fl_speed_multiplier);
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
            SetSpeedMulti(0);

            //stop timer
            bl_startTimer = false;
        }

        //when enter object with SpeedBoost tag, increase speed multiplier to increase speed of PC
        if(cl_trigger_collider.gameObject.tag == "SpeedBoost") 
        {
            Debug.Log("Past Speed Multiplier: " + fl_speed_multiplier);
            fl_speed_multiplier++;
            Debug.Log("Current Speed Multiplier: " + fl_speed_multiplier);

        }
        //when enter obstacle trigger,  
        if (cl_trigger_collider.gameObject.tag == "Obstacle")
        {
            //output last multipler to console, for debug n test
            Debug.Log("Past Speed Multiplier: " + fl_speed_multiplier);
            //set fl speed multi history to last speed so we can adjust it when pc moves again
            fl_speed_multiplier_history = fl_speed_multiplier;
            //call crash function , use start coroutine to have delay 
            StartCoroutine (Crash());
        }

        if(cl_trigger_collider.gameObject.tag == "StartLine")
        {
            bl_startTimer = true;
        }

    }



}
