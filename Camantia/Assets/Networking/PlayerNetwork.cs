using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    public GameObject cam;

    Vector3 velocity;
    public float gravity = -9.8f;
    public float jumpHeight = 2;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
           cam.GetComponent<MouseLook>().enabled = false;
            return;
        }

        if(IsOwner) cam.SetActive(true);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        Vector3 moveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) moveDir.z = +1f;  
        if (Input.GetKey(KeyCode.S)) moveDir.z = -1f;  
        if (Input.GetKey(KeyCode.A)) moveDir.x = -1f;  
        if (Input.GetKey(KeyCode.D)) moveDir.x = +1f;

        float moveSpeed = 3f;
        
        transform.position += moveDir * moveSpeed * Time.deltaTime;



    }
}
