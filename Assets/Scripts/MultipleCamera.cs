using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleCamera : MonoBehaviour
{
    public Camera Main_Camera;
    public Camera Third_Person_Camera;

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            ShowFPC();
        }

        else if (Input.GetKeyDown("2"))
        {
            ShowTPC();
        }
    }

        public void ShowTPC()
    {
        Main_Camera.enabled = false;
        Third_Person_Camera.enabled = true;
    }

    // Call this function to enable FPS camera,
    // and disable overhead camera.
    public void ShowFPC()
    {
        Main_Camera.enabled = true;
        Third_Person_Camera.enabled = false;
    }
}
