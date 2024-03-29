﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour
{
    public Material baseMaterial;
    public Material highlightedMaterial;

    private Renderer renderer;

    private Transform playerPosition;

    void Awake()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = baseMaterial;
        playerPosition = GameObject.Find("Head").transform;
        Debug.Log(playerPosition.ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Highlight()
    {
        renderer.material = highlightedMaterial;
    }

    void Base()
    {
        renderer.material = baseMaterial;
    }

    void OnMouseOver()
    {
        if (Vector3.Distance(this.transform.position, playerPosition.transform.position) <= 20f)
        {
            Highlight();
        }
        else
        {
            Base();
        }
    }

    void OnMouseExit()
    {
        Base();
    }
}
