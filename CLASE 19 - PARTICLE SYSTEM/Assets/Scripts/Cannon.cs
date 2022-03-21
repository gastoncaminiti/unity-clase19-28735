using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject shootOrigen;

    [SerializeField] private int shootCooldown = 2;
    [SerializeField] private float timeShoot = 0;

    private bool canShoot = true;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float distanceRay = 10f;

    [SerializeField] GameObject[] waypoints;
    [SerializeField] private bool isActivate = true;

    private void Start()
    {
        StartCoroutine(RotateBehaviour());
        StartCoroutine(WaypointsBehavior());
    }

    IEnumerator RotateBehaviour()
    {

        /*
        transform.Rotate(0f, 45f, 0f);
        yield return new WaitForSeconds(2f);
        transform.Rotate(0f, 45f, 0f);
        yield return new WaitForSeconds(2f);
        transform.Rotate(0f, 45f, 0f);
        */
        while (isActivate)
        {
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(1f);
                transform.Rotate(0f, 90f, 0f);
            }
        }
    }

    IEnumerator WaypointsBehavior()
    {
        for (int i = 0; i < waypoints.Length; ++i)
        {
            while (transform.position != waypoints[i].transform.position)
            {
                yield return null;
                transform.position = Vector3.MoveTowards(transform.position, waypoints[i].transform.position, 10f * Time.deltaTime);
            }
            yield return new WaitForSeconds(3f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            emitirRaycast();
        }
        else
        {
            timeShoot += Time.deltaTime;
        }

        if (timeShoot > shootCooldown)
        {
            canShoot = true;
        }
     }

    private void emitirRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(shootOrigen.transform.position, shootOrigen.transform.TransformDirection(Vector3.forward), out hit, distanceRay))
        {
            if (hit.transform.tag == "Player")
            {
                canShoot = false;
                timeShoot = 0;
                GameObject b = Instantiate(bulletPrefab, shootOrigen.transform.position, bulletPrefab.transform.rotation);
                b.GetComponent<Rigidbody>().AddForce(shootOrigen.transform.TransformDirection(Vector3.forward) * 10f, ForceMode.Impulse);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (canShoot)
        {
            Gizmos.color = Color.blue;
            Vector3 puntob = shootOrigen.transform.TransformDirection(Vector3.forward) * distanceRay;
            Gizmos.DrawRay(shootOrigen.transform.position, puntob);
        }
    }

    public void DiseableCannon()
    {
        gameObject.SetActive(false);
    }

}
