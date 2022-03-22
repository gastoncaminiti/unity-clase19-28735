using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] protected EnemyData enemyStats;

    protected ParticleSystem attackVFX;

    protected virtual void Start()
    {
        attackVFX = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }



    protected virtual void Move()
    {
        transform.Translate(Vector3.forward * enemyStats.Speed * Time.deltaTime);
    }

    protected void DrawRaycast()
    {
        Gizmos.color = Color.blue;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * enemyStats.RangeAttack;
        Gizmos.DrawRay(transform.position, direction);
    }

    void OnDrawGizmos()
    {
        DrawRaycast();
    }

    protected void EnableMunition()
    {
        if (!attackVFX.isPlaying)
        {
            attackVFX.Play();
        }
    }
}
