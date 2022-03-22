using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpykeMovement : MonoBehaviour
{
    [SerializeField] float startForce = 200f;
    [SerializeField] float torqueForce = 200f;
    // Start is called before the first frame update
    private Rigidbody rbSpike;
    
    void Start()
    {
        rbSpike = GetComponent<Rigidbody>();
        rbSpike.AddRelativeForce(Vector3.forward * startForce);
        //rbSpike.AddTorque(Vector3.up * torqueForce);
        //rbSpike.AddTorque(Vector3.forward * torqueForce);
    }
 
}
