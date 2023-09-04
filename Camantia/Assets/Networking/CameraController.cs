using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using System.Globalization;


public class CameraController : NetworkBehaviour
{
    public GameObject cameraHolder;

    public override void OnStartAuthority()
    {
        cameraHolder.SetActive(true);
    }
}

