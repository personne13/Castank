using UnityEngine;
using UnityEngine.UI;


public class Shop : MonoBehaviour {


    public Soldier SoldierPrefab;
    public Archer ArcherPrefab;

    private int soldierCost = 10;
    private int soldierUpgradeLifeCost = 50;
    private int soldierUpgradeDamageCost = 100;
    private int archerCost = 20;
    private int archerUpgradeLifeCost = 90;
    private int archerUpgradeDamageCost = 70;

    public Text soldierCostText;
    public Text soldierUpgradeLifeCostText;
    public Text soldierUpgradeDamageCostText;
    public Text archerCostText;
    public Text archerUpgradeLifeCostText;
    public Text archerUpgradeDamageCostText;


    private Character ItemToBuild;

    private void setShopText()
    {
        soldierCostText.text = "$" + Mathf.Round(soldierCost).ToString();
        soldierUpgradeLifeCostText.text = "Life up $" + Mathf.Round(soldierUpgradeLifeCost).ToString();
        soldierUpgradeDamageCostText.text = "Dmg up $" + Mathf.Round(soldierUpgradeDamageCost).ToString();
        archerCostText.text = "$" + Mathf.Round(archerCost).ToString();
        archerUpgradeLifeCostText.text = "Life up $" + Mathf.Round(archerUpgradeLifeCost).ToString();
        archerUpgradeDamageCostText.text = "Dmg up $" + Mathf.Round(archerUpgradeDamageCost).ToString();
    }

    void Start()
    {
        setShopText();
    }

  

    void buildUnit(Character item, int cost)
    {
        if (Game.Ressource() >= cost)
        {
            Character c = Instantiate(item, new Vector3(-6, 0.1f, 6), Quaternion.identity);
            Game.addTroop(c);
            Game.removeRessources(cost);
        }
    }

    public void PurchaseSoldier()
    {
        Debug.Log("Soldier purchased");
        buildUnit(SoldierPrefab, soldierCost);
    }

    public void PurchaseArcher()
    {
        Debug.Log("Archer purchased");
        buildUnit(ArcherPrefab, archerCost);
    }

    public void UpgradeSoldierLife()
    {
        if (Game.Ressource() >= soldierUpgradeLifeCost)
        {
            Soldier.unitLife += 200;
            Game.removeRessources(soldierUpgradeLifeCost);
        }
    }

    public void UpgradeSoldierDamage()
    {
        if (Game.Ressource() >= soldierUpgradeDamageCost)
        {
            Sword.unitDamage += 200;
            Game.removeRessources(soldierUpgradeDamageCost);
        }
    }

    public void UpgradeArcherLife()
    {
        if (Game.Ressource() >= archerUpgradeLifeCost)
        {
            Archer.unitLife += 200;
            Game.removeRessources(archerUpgradeLifeCost);
        }
    }

    public void UpgradeArcherDamage()
    {
        if (Game.Ressource() >= archerUpgradeDamageCost)
        {
            Projectile.unitDamage += 200;
            Game.removeRessources(archerUpgradeDamageCost);
        }
    }


    public Character GetItemToBuild()
    {
        return ItemToBuild;
    }
    
}
