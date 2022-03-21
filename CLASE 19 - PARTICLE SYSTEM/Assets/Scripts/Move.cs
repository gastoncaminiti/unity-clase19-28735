using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector3 direction = new Vector3(0, 0, 1);
    [SerializeField] float speed = 2f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if(transform.position.x > 20f){
            Destroy(gameObject);
        }
        
    }

    private void Movement()
    {
        transform.Translate(speed * Time.deltaTime * direction);
    }
}
