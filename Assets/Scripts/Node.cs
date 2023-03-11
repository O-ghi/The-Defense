using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Transform select;
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        select = transform.Find("Select").GetComponent<Transform>();
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        // if (EventSystem.current.IsPointerOverGameObject())
        //     return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

<<<<<<< HEAD
=======
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
>>>>>>> c5bd083 (menu scene)

        Debug.Log("Turret build!");
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        //Get rid of the old turret
        Destroy(turret);

        //Build a new one
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

<<<<<<< HEAD
=======
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
>>>>>>> c5bd083 (menu scene)

        isUpgraded = true;

        Debug.Log("Turret upgraded!");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

<<<<<<< HEAD
=======
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

>>>>>>> c5bd083 (menu scene)
        Destroy(turret);
        turretBlueprint = null;
    }

    void OnMouseEnter()
    {
        select.gameObject.SetActive(true);

        // if (EventSystem.current.IsPointerOverGameObject())
        //     return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            // rend.material.color = hoverColor;
        }
        else
        {
            // rend.material.color = notEnoughMoneyColor;
        }

    }

    void OnMouseExit()
    {
        select.gameObject.SetActive(false);
    }

}
