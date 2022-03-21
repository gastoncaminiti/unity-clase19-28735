using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Enemy Data", menuName ="Create Zombie Data")]
public class EnemyData : ScriptableObject
{
    //DESING DATA
    [Header("CONFIGURACION DE MOVIMIENTO")]
    [Tooltip("LA VELOCIDAD ES DE 0.1 A 5")]
    [SerializeField][Range(0.1f,5f)] private float speed = 2f;
    public float Speed{ get{  return speed; } set{ speed = value; } }

    [Header("CONFIGURACION DE ESTADISTICAS")]
    [SerializeField] private int hp = 5;
    public int HP{ get{  return hp; } set{ hp = value; } }


    [Header("CONFIGURACION DE ATAQUE")]
    [SerializeField] private float rangeAttack = 10f;
     public float RangeAttack{ get{  return rangeAttack; } set{ rangeAttack = value; } }
}
