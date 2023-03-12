using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;

    public float startHealth = 100;
    private float health;

    public int worth = 50;


    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    //Animation Event
    private AnimationEvent animationEvent;
    public EnemyMovement enemyMovement;
    public Transform target;
    void Start()
    {
        speed = startSpeed;
        health = startHealth;

        animationEvent = gameObject.AddComponent<AnimationEvent>();
        enemyMovement = gameObject.AddComponent<EnemyMovement>();
        enemyMovement.SetTarget(target);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            StartCoroutine("Die");
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    IEnumerator Die()
    {
        isDead = true;
        Debug.Log($"Die");
        PlayerStats.Money += worth;

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Die");
        GetComponent<EnemyMovement>().isDie = isDead;
        WaveSpawner.EnemiesAlive--;

        yield return new WaitForSeconds(2f);
        animationEvent.DestroyObject();
    }

}
