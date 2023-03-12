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
}
