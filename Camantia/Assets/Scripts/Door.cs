using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform start, end;
    public GameObject Button;

    void Start()
    {
      transform.position = start.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, end.position, Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(start.position, Vector3.one * 0.1f);
        Gizmos.DrawCube(end.position, Vector3.one * 0.1f);
    }
}
