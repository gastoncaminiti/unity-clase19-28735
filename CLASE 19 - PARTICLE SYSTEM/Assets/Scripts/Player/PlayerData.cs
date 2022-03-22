using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Create Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("CONFIGURACION DE MOVIMIENTO")]

    [SerializeField][Range(1f, 50f)] private float speed = 2f;
    public float Speed { get { return speed; } set { speed = value; } }

    [SerializeField][Range(1f, 10f)] private float speedJump = 1f;
    public float SpeedJump { get { return speedJump; } set { speedJump = value; } }

    [SerializeField][Range(5f, 100f)] private float speedLimit = 15f;
    public float SpeedLimit { get { return speedLimit; } set { speedLimit = value; } }

    [Header("CONFIGURACION DE ATAQUE")]

    [SerializeField][Range(1f, 10f)] private float cooldown = 2f;
    public float Cooldown { get { return cooldown; } set { cooldown = value; } }

    [SerializeField] private GameObject bulletPrefab;
    public GameObject BulletPrefab { get { return bulletPrefab; } }

    [Header("CONFIGURACION DE SFX")]

    [SerializeField] private AudioClip jumpSound;
    public AudioClip JumpSound { get { return jumpSound; } }
    [SerializeField] private AudioClip shootSound;
    public AudioClip ShootSound { get { return shootSound; } }

}
