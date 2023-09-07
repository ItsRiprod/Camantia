using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

using System.Globalization;

public class PlayerMovement : NetworkBehaviour
{
    public GameObject cam;
    public GameObject body;
    private CharacterController controller;

    public float speed = 14f;

    Vector3 velocity;
    Vector3 spawn;
    public float gravity = -9.8f;
    public float jumpHeight = 2;

    public Transform groundCheck; 
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public Transform STUCK;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {


        if (!IsOwner)
        {
            cam.GetComponent<MouseLook>().enabled = false;
            
            return;
        }

        if (IsOwner)
        {
            cam.SetActive(true);
            body.SetActive(false);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x) + (transform.forward * z);

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


    }
}
