using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Shoot : MonoBehaviour
{
    public GameObject go_projectile_prefab;
    public Transform tr_spawn_location;
    public Transform tr_spawn_location2;

    public PC_Movement sc_PC_Move;

    public float fl_bulletSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { SpawnProjectile();        /*Debug.Log("Space Pressed");*/ }
    }

    private void SpawnProjectile() 
    {
        // instantiate/spawn bullet and save  as variable 
        var bullet = Instantiate(go_projectile_prefab, tr_spawn_location.position, tr_spawn_location.rotation);
        //send bullet forward using rigidbody , multiply by speed and current speed multiplier 
        bullet.GetComponent<Rigidbody>().velocity = Vector3.forward * fl_bulletSpeed * sc_PC_Move.fl_speed_multiplier;

        //same again but for gun two 
        // instantiate/spawn bullet and save  as variable 
        var bullet2 = Instantiate(go_projectile_prefab, tr_spawn_location2.position, tr_spawn_location2.rotation);
        //send bullet forward using rigidbody , multiply by speed and current speed multiplier 
        bullet2.GetComponent<Rigidbody>().velocity = Vector3.forward * fl_bulletSpeed * sc_PC_Move.fl_speed_multiplier;

    }
}
