using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float pickUpDistance;
    [SerializeField] private LayerMask pickupable;
    private Item item;

    void Update()
    {
        RaycastHit hit;
        if(Input.GetButtonDown("Fire2"))
        {
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, pickUpDistance, pickupable))
            {
                hit.transform.GetComponent<PickUpables>().AddToInventory();
            }
        }
    }
}
