using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite selected;
    [SerializeField] private Sprite nonSelected;

    void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.sprite = selected;
    }

    public void Deselect()
    {
        image.sprite = nonSelected;
    }

}
