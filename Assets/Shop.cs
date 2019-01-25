using UnityEngine;

public class Shop : MonoBehaviour {


    public Soldier SoldierPrefab;
    public Archer ArcherPrefab;

    private Character ItemToBuild;

    void Start()
    {
        
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

    void OnMouseDown()
    {
        if (GetItemToBuild() == null)
            return;


    }
    public void PurchaseSoldier()
    {
        Debug.Log("Soldier purchased");
        buildUnit(SoldierPrefab, 10);
    }

    public void PurchaseArcher()
    {
        Debug.Log("Archer purchased");
        buildUnit(ArcherPrefab, 20);
    }


    public Character GetItemToBuild()
    {
        return ItemToBuild;
    }
    
}
