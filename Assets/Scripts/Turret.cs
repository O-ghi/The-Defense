using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{

    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]

    public float range = 15f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Is Thunder")]
    public bool isThunder = false;
    public float slowAmount = .5f;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public Transform firePoint;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<Enemy>().isDead) continue;
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            // if (isThunder)
            // {
            //     if (lineRenderer.enabled)
            //     {
            //         lineRenderer.enabled = false;
            //         impactEffect.Stop();
            //         impactLight.enabled = false;
            //     }
            // }

            return;
        }

        LockOnTarget();

        if (isThunder)
        {

            if (fireCountdown <= 0f)
            {
                DropThunder();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

    }

    void LockOnTarget()
    {
        if (isThunder) return;
        Vector2 dir = target.position - partToRotate.position;
        // Quaternion lookRotation = Quaternion.LookRotation(dir);
        // Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        // partToRotate.rotation = Quaternion.Euler(0f, 0f, rotation.z);

        partToRotate.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }

    void DropThunder()
    {
        partToRotate.GetComponent<Animator>().SetTrigger("Attack");
        targetEnemy.Slow(slowAmount);
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, targetEnemy.transform.position + new Vector3(0f, 3f, 0f), targetEnemy.transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
        // Vector3 dir = firePoint.position - target.position;

        // impactEffect.transform.position = target.position + dir.normalized;

        // impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void Shoot()
    {
        partToRotate.GetComponent<Animator>().SetTrigger("Attack");
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
