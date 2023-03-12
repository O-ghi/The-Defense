using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{

    public Transform target;
    private int wavepointIndex = 0;

    private Enemy enemy;

    public bool isDie;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        if (isDie)
        {
            return;
        }
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            EndPath();
        }

        enemy.speed = enemy.startSpeed;
    }



    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
