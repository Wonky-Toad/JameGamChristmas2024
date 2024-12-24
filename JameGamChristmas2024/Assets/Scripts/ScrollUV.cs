using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUV : MonoBehaviour
{
    public float fl_scrollX = 0.5F;
    public float fl_scrollY = 0;


    // Update is called once per frame
    void Update()
    {
        float OffsetX = Time.time * fl_scrollX;
        float OffsetY = Time.time * fl_scrollY;

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (OffsetX, OffsetY);
    }
}
