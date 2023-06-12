using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeBox : MonoBehaviour
{
   // public Transform holdPoint;
   // Rigidbody rots;
    public Gloves hg;

/*
    private void Start()
    {
        rots = GetComponent<Rigidbody>();
    }*/
    private void Update()
    {

        if (hg.hasGloves == true)
        {
            this.tag = "LargeBox";
        }
    }

/*    private void OnMouseDown()
    {

        if (hg.hasGloves == true)
        {
            rots.freezeRotation = true;
            GetComponent<Rigidbody>().useGravity = false;
            this.transform.parent = GameObject.Find("holdPoint").transform;
        }

    }*/

/*    private void OnMouseUp()
    {
        rots.freezeRotation = false;
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
    }*/
}
