using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetSystem : MonoBehaviour
{
    public float x, y, z;
    private bool canTele;
    public ParticleSystem savePart;
    public ParticleSystem loadPart;

    public Transform sPart;
    public Transform lPart;

  
    CharacterController cc;
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        Save();
        canTele = false;

    }
    void Update()
    {
       if (Input.GetKey(KeyCode.P))
        {
            Save();
            savePart.Play();
            sPart.transform.position = this.transform.position;
            
        }
       if (Input.GetKey(KeyCode.L) && canTele)
        {
            lPart.transform.position = GameObject.Find("FirstPersonPlayer").transform.position;
            
            Load();

            loadPart.Play();
        }

    }

    
    public void Save()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;

        PlayerPrefs.SetFloat("x", x);
        PlayerPrefs.SetFloat("z", z);
        PlayerPrefs.SetFloat("y", y);
        canTele = true;
    }

    public void Load()
    {



        x = PlayerPrefs.GetFloat("x");
        y = PlayerPrefs.GetFloat("y");
        z = PlayerPrefs.GetFloat("z");

        Vector3 LoadPosition = new Vector3(x, y, z);
        cc.enabled = false;
        transform.position = LoadPosition;
        cc.enabled = true;

    }
}
