using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour
{
    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;

    [Header("UI")]
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI itemCountText;

    public void InitializeItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }

    public void RefreshCount()
    {
        itemCountText.text = count.ToString();
        bool textActive = count > 1;
        itemCountText.gameObject.SetActive(textActive);
    }
}
