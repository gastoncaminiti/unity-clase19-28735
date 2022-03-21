using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    // Start is called before the first frame update
    /*
    [SerializeField] GameObject cameraA;
    [SerializeField] GameObject cameraB;
    [SerializeField] GameObject cameraC;
    */

    [SerializeField] GameObject[] cameras;

    void Start()
    {
        //cameraA.SetActive(true);
        enableCamera(0, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            enableCamera(0, true);
            enableCamera(1, false);
            enableCamera(2, false);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            enableCamera(0, false);
            enableCamera(1, true);
            enableCamera(2, false);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            enableCamera(0, false);
            enableCamera(1, false);
            enableCamera(2, true);
        }
    }

    void enableCamera(int posicion, bool status)
    {
        cameras[posicion].SetActive(status);
    }

}
