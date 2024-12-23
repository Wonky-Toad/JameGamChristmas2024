using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //life count of 3
    //public float fl_life = 50;

    public GameObject go_PC;

    Vector3 v3_BulletCurrentPos = Vector3.zero;
    Vector3 v3_PC_CurrentPos = Vector3.zero;

    public float fl_range = 0F; 



    //on awake (when spawned or enabled)
    private void Awake()
    {
        fl_range = 15F;
        go_PC = GameObject.FindGameObjectWithTag("PC");
        //destroy gameobject once lifetime has past
        //Destroy(gameObject, fl_life);
        StartCoroutine(ByeCandyCane());
    }
    private void Update()
    {
        DeathByRange();

    }

    IEnumerator ByeCandyCane()
    {


        yield return new WaitForSeconds(2);
        Destroy(gameObject); 
    }

    private  void DeathByRange()
    {
        v3_PC_CurrentPos = go_PC.transform.position;
        v3_BulletCurrentPos = gameObject.transform.position;
        if (Vector3.Distance(v3_BulletCurrentPos, v3_PC_CurrentPos) > fl_range) { Destroy(gameObject); }
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
