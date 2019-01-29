using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBuilder : MonoBehaviour
{
    private Vector3 clickPosition;

    public GameObject shopMenu;

    private void OnMouseDown()
    {
        if (Shop.itemCanBeInstantiate)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                clickPosition = hit.point;
                Debug.Log(clickPosition);
            }
            buildUnit(Shop.GetItemToBuild());
        }
    }

    void buildUnit(Character item)
    {
        Character c = Instantiate(item, clickPosition + new Vector3(0, 0.1f, 0), Quaternion.identity);
        Game.addTroop(c);
        Shop.itemCanBeInstantiate = false;
        shopMenu.SetActive(!shopMenu.activeSelf);
    }
}