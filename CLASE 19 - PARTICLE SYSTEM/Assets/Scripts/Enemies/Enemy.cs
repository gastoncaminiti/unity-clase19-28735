using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    /*
    //DESING DATA
    [SerializeField] protected float speed = 2f;
    [SerializeField] int hp = 5;
    [SerializeField] float rangeAttack = 10f;
    */
    [SerializeField] protected EnemyData enemyStats;

    //RUNTIME DATA
    bool isAttack;

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    private void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, enemyStats.RangeAttack))
        {
            if (hit.transform.CompareTag("Player"))
            {
                isAttack = true;
                Debug.Log("TE ESTA ATACANDO : " + gameObject.name);
            }
        }
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
}
