using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Movement : MonoBehaviour
{
    float fl_speed = 5;
    float fl_speed_multiplier = 2;
    float fl_gravity = 2;

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

        // Add fl_gravity to the direction vector
        v3_move_direction.y -= fl_gravity * Time.deltaTime * fl_speed_multiplier;

        // Move the character controller with the direction vector
        cc_PC.Move(v3_move_direction * Time.deltaTime);

    }

    void OnTriggerEnter(Collider cl_trigger_collider)
    {
        if(cl_trigger_collider.gameObject.tag == "FinishLine") { Debug.Log("You Win!"); }
    }



}
