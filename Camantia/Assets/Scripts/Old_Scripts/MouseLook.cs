using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MouseLook : NetworkBehaviour
{
    
    public float mouseSes = 500f;
    [SerializeField]
    private Transform playerBody;

    float xRotation = 0;
    // Start is called before the first frame update
    void Start()
    {

        playerBody = transform.parent.parent.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()

    {

        //if (!IsOwner) return;
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSes * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSes * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
