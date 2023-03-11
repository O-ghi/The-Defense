using UnityEngine;

public class Shop : MonoBehaviour
{

    public TurretBlueprint bowTower;
    public TurretBlueprint thunderTower;
    public TurretBlueprint slingTower;
    public TurretBlueprint catapultTower;


    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectBowTower()
    {
        Debug.Log("bowTower Selected");
        buildManager.SelectTurretToBuild(bowTower);
    }

    public void SelectThunderTower()
    {
        Debug.Log("thunderTower Selected");
        buildManager.SelectTurretToBuild(thunderTower);
    }

    public void SelectSlingTower()
    {
        Debug.Log("slingTower Selected");
        buildManager.SelectTurretToBuild(slingTower);
    }

    public void SelectCatapultTower()
    {
        Debug.Log("catapultTower Selected");
        buildManager.SelectTurretToBuild(catapultTower);
    }
}
