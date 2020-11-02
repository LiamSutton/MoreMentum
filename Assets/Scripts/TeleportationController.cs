using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportationController : MonoBehaviour
{
    public bool isReadyToTeleport = true;
    public float maxTeleporterDistance = 20f;
    public float teleporterForce = 750f;
    public float teleportCooldown;
    private Rigidbody rb;
    
    public Image TeleportUI;
    public Color teleportEnabled;
    public Color teleportDisabled;
    private DashController dashController;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        dashController = GetComponent<DashController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, maxTeleporterDistance))
            {
                if (hit.collider.CompareTag("Teleporter"))
                {
                    StartCoroutine(Teleport(hit));
                }
            }
        }
    }

    private IEnumerator Teleport(RaycastHit location) {
        TeleportUI.color = teleportDisabled;
        isReadyToTeleport = false;
        rb.velocity = Vector3.zero;
        transform.position = location.transform.position + Vector3.up * 2f;
        dashController.SendMessage("ResetDash");
        rb.AddForce(Vector3.up * teleporterForce * 3f);
        yield return new WaitForSeconds(teleportCooldown);
        isReadyToTeleport = true;
        TeleportUI.color = teleportEnabled;
    }
}
