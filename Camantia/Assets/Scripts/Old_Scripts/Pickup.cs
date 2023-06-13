using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform holdPoint;
    Rigidbody rots;
    private bool isHolding;

    private void Start()
    {
        rots = GetComponent<Rigidbody>();
        isHolding = false;
    }
    private void Update()
    {
        if (isHolding)
        {
            //this.transform.position = holdPoint.position;
            //this.transform.position = Vector3.MoveTowards(this.transform.position, GameObject.Find("holdPoint").transform.position, .05f);
        }
    }

    private void OnMouseDown()
    {
       
        rots.freezeRotation = true;
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.parent = GameObject.Find("holdPoint").transform;
        isHolding = true;


    }

    private void OnMouseUp()
    {
        rots.freezeRotation = false;
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        isHolding = false;
    }

    private void OnTrigger(Collider other)
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (isHolding)
        {
            //this.transform.position = holdPoint.position;
            this.transform.position = Vector3.MoveTowards(this.transform.position, GameObject.Find("holdPoint").transform.position, .05f);
        }
    }

}
