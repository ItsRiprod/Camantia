using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{

    public List<GameObject> onPad;
    public float boxOnPad;
    public GameObject door;
    public Transform start, end;

    private void Update()
    {
        if (onPad.Count >= boxOnPad)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, end.position, 2*Time.deltaTime);
        }
        else if (onPad.Count <= boxOnPad)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, start.position, 2*Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Box")
        {
            onPad.Add(other.gameObject);
        }
        if (other.tag == "LargeBox" || other.tag == "SmallBox")
        {
            onPad.Add(other.gameObject);
            onPad.Add(other.gameObject);
        }

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Box")
        {
            onPad.Remove(other.gameObject);
        }

        if (other.tag == "LargeBox" || other.tag == "SmallBox")
        {
            onPad.Remove(other.gameObject);
            onPad.Remove(other.gameObject);
        }

    }






}
