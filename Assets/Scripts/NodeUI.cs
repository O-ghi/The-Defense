using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{

    public GameObject ui;

    public Text upgradeCost;
    public Text damage;
    public Text level;
    public Button upgradeButton;

    public Text sellAmount;

    private Node target;

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = target.UpdateCost + "$";
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmount.text = target.currentPrice / 2 + "$";
        Bullet bullet = target.turret.GetComponent<Turret>().bulletPrefab.GetComponent<Bullet>();
        damage.text = "+" + bullet.damage.ToString();
        level.text = "Level " + target.currentLevel.ToString();
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }

    public void CloseButton()
    {
        ui.SetActive(false);
    }
}
