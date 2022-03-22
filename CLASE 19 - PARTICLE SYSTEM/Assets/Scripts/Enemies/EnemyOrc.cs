using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrc : Enemy
{
    private GameObject player;
    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
    }

    protected override void Update()
    {
        base.Update();
        Attack();
    }
    
    protected override void Move()
    {
        LookAtPlayer();
        Vector3 direction = (player.transform.position - transform.position);
        if (direction.magnitude > 3)
        {
            transform.position += enemyStats.Speed * direction.normalized * Time.deltaTime;
        }
    }

    private void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, enemyStats.RangeAttack))
        {
            if (hit.transform.CompareTag("Player"))
            {
                EnableMunition();
            }
        }
    }

    private void LookAtPlayer()
    {
        Quaternion newRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = newRotation;
    }
}
