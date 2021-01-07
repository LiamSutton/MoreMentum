using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightInteractableController : MonoBehaviour
{
    public float maxTeleporterDistance = 20f;
    void Update()
    {
        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxTeleporterDistance)) {
            if (hit.collider.CompareTag("Teleporter")) {
                hit.collider.gameObject.SendMessage("Highlight");
            }
        }
    }
}
