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
    [HideInInspector]
    public int currentLevel = 0;

    [HideInInspector]
    public int UpdateCost = 0;

    [HideInInspector]
    public int currentPrice = 0;
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

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity, transform);
        turret = _turret;

        turretBlueprint = blueprint;
        currentPrice = turretBlueprint.cost;
        currentLevel = 1;
        UpdateCost = turretBlueprint.upgradeCostLevel2;
        Debug.Log("Turret build!");
    }

    public void UpgradeTurret()
    {
        if (currentLevel == 1)
        {
            if (PlayerStats.Money < turretBlueprint.upgradeCostLevel2)
            {
                Debug.Log("Not enough money to upgrade that!");
                return;
            }

            PlayerStats.Money -= turretBlueprint.upgradeCostLevel2;

            //Get rid of the old turret
            Destroy(turret);

            //Build a new one
            GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefabLevel2, GetBuildPosition(), Quaternion.identity, transform);
            turret = _turret;

            currentLevel = 2;
            currentPrice = turretBlueprint.upgradeCostLevel2;
            UpdateCost = turretBlueprint.upgradeCostLevel3;
            Debug.Log("Turret upgraded!");
            return;
        }
        if (currentLevel == 2)
        {
            if (PlayerStats.Money < turretBlueprint.upgradeCostLevel3)
            {
                Debug.Log("Not enough money to upgrade that!");
                return;
            }

            PlayerStats.Money -= turretBlueprint.upgradeCostLevel3;

            //Get rid of the old turret
            Destroy(turret);

            //Build a new one
            GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefabLevel3, GetBuildPosition(), Quaternion.identity);
            turret = _turret;
            currentPrice = turretBlueprint.upgradeCostLevel3;
            isUpgraded = true;
            currentLevel = 2;
            Debug.Log("Turret upgraded!");
        }
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

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
