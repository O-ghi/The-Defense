using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    public List<TypeEnemy> EnemyList;
}

[System.Serializable]
public class TypeEnemy
{
    public GameObject enemy;
    public int count;
    public float rate;

    public TypeEnemy(GameObject enemy, int count, float rate)
    {
        this.enemy = enemy;
        this.count = count;
        this.rate = rate;
    }
}
