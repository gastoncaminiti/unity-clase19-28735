using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBee : Enemy
{
    [SerializeField] private float delayJump = 5f;

    private Rigidbody rbEnemy;

    private void Start()
    {
        rbEnemy = GetComponent<Rigidbody>();
        InvokeRepeating("JumpBee", 0f, delayJump);
        enemyStats.HP++;
        Debug.Log("LA VIDA ACTUAL ES"+enemyStats.HP);
    }

    private void JumpBee()
    {
        Debug.Log("JUMPE BEE");
        rbEnemy.AddForce(Vector3.up * enemyStats.Speed, ForceMode.Impulse);
    }
}
