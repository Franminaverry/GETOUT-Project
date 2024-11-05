using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;

    public AudioSource turnOn;
    public AudioSource turnOff;

    private bool on;
    private bool off;




    void Start()
    {
        on = true;
        flashlight.SetActive(true);
    }




    void Update()
    {
        if(off && Input.GetKeyDown(KeyCode.F))
        {
            flashlight.SetActive(true);
            turnOn.Play();
            off = false;
            on = true;           
        }
        else if (on && Input.GetKeyDown(KeyCode.F))
        {
            flashlight.SetActive(false);
            turnOff.Play();
            off = true;
            on = false;
        }
    }
}
