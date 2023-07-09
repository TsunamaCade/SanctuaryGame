using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [SerializeField] private GameObject[] inventorySlots;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private int stackAmount;

    [SerializeField] private int slot = 0;
    [SerializeField] private KeyCode[] keys;
    [SerializeField] private GameObject hotbar;

    int selectedSlot = -1;

    void Start()
    {
        ChangeSelectedSlot(0);
    }

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        int previousSlot = slot;

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            slot++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            slot--;
            if (slot < 0)
            {
                slot = hotbar.transform.childCount - 1;
            }
        }
        slot = slot % hotbar.transform.childCount;

        for (int i = 0; i < keys.Length; i++)
        {
            if(Input.GetKeyDown(keys[i]))
            {
                slot = i;
            }
        }

        if (previousSlot != slot)
        {
            ChangeSelectedSlot(slot);
        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if(selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].transform.GetComponent<InventorySlot>().Deselect();
        }

        inventorySlots[newValue].transform.GetComponent<InventorySlot>().Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            GameObject slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot != null && itemInSlot.item == item && itemInSlot.count < stackAmount && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }
        
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            GameObject slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, GameObject slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    public Item GetSelectedItem(bool use)
    {
        GameObject slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if(itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if(use == true)
            {
                itemInSlot.count--;
                if(itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }

        return null;
    }

    void SelectWeapon ()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == slot)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
