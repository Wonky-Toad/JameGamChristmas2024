using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // when shot by bullet, destroy bullet and obstacle 
        if(other.gameObject.tag == "Bullet") { Destroy(other.gameObject); Destroy(this.gameObject); }
    }

}

