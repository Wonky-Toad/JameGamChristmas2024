using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Movement : MonoBehaviour
{
    float fl_speed = 5;
    float fl_speed_multiplier = 1;
    float fl_gravity = 2;

    float fl_speed_multiplier_history = 0; 

    public Vector3 v3_move_direction = Vector3.zero;
    public CharacterController cc_PC;



    // Start is called before the first frame update
    void Start()
    {
        cc_PC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePC();
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

    void OnTriggerEnter(Collider cl_trigger_collider)
    {
        if(cl_trigger_collider.gameObject.tag == "FinishLine") { Debug.Log("You Win!"); }

        if(cl_trigger_collider.gameObject.tag == "SpeedBoost") 
        {
            Debug.Log("Past Speed Multiplier: " + fl_speed_multiplier);
            fl_speed_multiplier++;
            Debug.Log("Current Speed Multiplier: " + fl_speed_multiplier);

        }

        if (cl_trigger_collider.gameObject.tag == "Obstacle")
        {
            Debug.Log("Past Speed Multiplier: " + fl_speed_multiplier);
            fl_speed_multiplier_history = fl_speed_multiplier;
            StartCoroutine (Crash());
            //fl_speed_multiplier = fl_speed_multiplier - 0.5f;

            //SetSpeedMulti(1, 3);

            //fl_speed_multiplier = fl_speed_multiplier - 0.5f;


        }
    }


    private IEnumerator Crash()
    {
        SetSpeedMulti(0); 
        yield return new WaitForSeconds(3);
        SetSpeedMulti(fl_speed_multiplier_history);

    }

    private void SetSpeedMulti(float newMulti) 
    {
        fl_speed_multiplier = newMulti;
        Debug.Log("Current Speed Multiplier: " + fl_speed_multiplier);
    }

}
