using UnityEngine;
using UnityEngine.UI;


public class Shop : MonoBehaviour {


    public Soldier SoldierPrefab;
    public Archer ArcherPrefab;
    public BomberMan BomberPrefab;

    private int soldierCost = 10;
    private int soldierUpgradeLifeCost = 50;
    private int soldierUpgradeDamageCost = 100;

    private int archerCost = 20;
    private int archerUpgradeLifeCost = 90;
    private int archerUpgradeDamageCost = 70;

    private int bomberCost = 20;
    private int bomberUpgradeLifeCost = 90;
    private int bomberUpgradeDamageCost = 70;

    public Text soldierCostText;
    public Text soldierUpgradeLifeCostText;
    public Text soldierUpgradeDamageCostText;

    public Text archerCostText;
    public Text archerUpgradeLifeCostText;
    public Text archerUpgradeDamageCostText;

    public Text bomberCostText;
    public Text bomberUpgradeLifeCostText;
    public Text bomberUpgradeDamageCostText;


    private Character ItemToBuild;

    private void setShopText()
    {
        soldierCostText.text = "$" + Mathf.Round(soldierCost).ToString();
        soldierUpgradeLifeCostText.text = "Life up $" + Mathf.Round(soldierUpgradeLifeCost).ToString();
        soldierUpgradeDamageCostText.text = "Dmg up $" + Mathf.Round(soldierUpgradeDamageCost).ToString();
        archerCostText.text = "$" + Mathf.Round(archerCost).ToString();
        archerUpgradeLifeCostText.text = "Life up $" + Mathf.Round(archerUpgradeLifeCost).ToString();
        archerUpgradeDamageCostText.text = "Dmg up $" + Mathf.Round(archerUpgradeDamageCost).ToString();
        bomberCostText.text = "$" + Mathf.Round(bomberCost).ToString();
        bomberUpgradeLifeCostText.text = "Life up $" + Mathf.Round(bomberUpgradeLifeCost).ToString();
        bomberUpgradeDamageCostText.text = "Dmg up $" + Mathf.Round(bomberUpgradeDamageCost).ToString();
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
        buildUnit(SoldierPrefab, soldierCost);
    }

    public void PurchaseArcher()
    {
        buildUnit(ArcherPrefab, archerCost);
    }

    public void PurchaseBomber()
    {
        buildUnit(BomberPrefab, archerCost);
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

    public void UpgradeBomberLife()
    {
        if (Game.Ressource() >= bomberUpgradeLifeCost)
        {
            BomberMan.unitLife += 200;
            Game.removeRessources(bomberUpgradeLifeCost);
        }
    }

    public void UpgradeBomberDamage()
    {
        if (Game.Ressource() >= bomberUpgradeDamageCost)
        {
            Bomb.unitDamage += 200;
            Game.removeRessources(bomberUpgradeDamageCost);
        }
    }

    public Character GetItemToBuild()
    {
        return ItemToBuild;
    }
    
}
