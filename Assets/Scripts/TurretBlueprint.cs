using UnityEngine;
using System.Collections;

[System.Serializable]
public class TurretBlueprint
{

    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefabLevel2;
    public int upgradeCostLevel2;
    public GameObject upgradedPrefabLevel3;
    public int upgradeCostLevel3;

    public int GetSellAmount()
    {
        return cost / 2;
    }

}
