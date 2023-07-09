using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vault : MonoBehaviour
{
    [SerializeField] private LayerMask vaultLayer;
    [SerializeField] private Camera cam;
    private float playerHeight = 2f;
    private float playerRadius = 0.5f;

    [SerializeField] private GameObject text;

    void Update()
    {
        Vaulting();
    }

    void Vaulting()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out var firstHit, 1f, vaultLayer))
            {
                if(Physics.Raycast(firstHit.point + (cam.transform.forward * playerRadius) + (Vector3.up * 0.6f * playerHeight), Vector3.down, out var secondHit, playerHeight))
                {
                    StartCoroutine(LerpVault(secondHit.point, 0.5f));
                }
            }
        }

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out var textHit, 1f, vaultLayer))
        {
            text.SetActive(true);
        }
        else
        {
            text.SetActive(false);
        }
    }

    IEnumerator LerpVault(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
