using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //life count of 3
    public float fl_life = 50;

    //on awake (when spawned or enabled)
    private void Awake()
    {
        //destroy gameobject once lifetime has past
        Destroy(gameObject, fl_life);
    }


    private void OnCollisionEnter(Collision collision)
    {
        //on collision destroy this game object 
        //Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        // if collides with obstacle then detroy that obsticles 
        if (other.gameObject.tag == "Obstacle") { Destroy(other.gameObject); }
    }
}
