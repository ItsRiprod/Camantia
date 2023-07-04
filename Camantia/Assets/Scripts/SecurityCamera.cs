using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public double loopTime;
    private double loopHolder;
    bool isLightOn = false;

    // Start is called before the first frame update
    void Start()
    {
        loopHolder = loopTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (loopTime <= 0)
        {
           flipLight();
           loopTime = loopHolder;
        }
        loopTime -= Time.deltaTime; 
        
    }

    private void flipLight()
    {
        if (isLightOn)
        {
            GetComponent<MeshRenderer>().enabled = false;
            isLightOn = false;
        }
        else
        {

            isLightOn = true;
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isLightOn)
        {
            Debug.Log("Caught!");
        }
    }
}
