using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpables : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private Item item;

    public void AddToInventory()
    {
        InventoryManager.instance.AddItem(item);
    }
}
