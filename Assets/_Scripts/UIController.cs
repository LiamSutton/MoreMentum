﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject panel;
    public GameObject dashUI;
    public GameObject jumpUI;
    public GameObject teleportUI;

    void Awake()
    {

    }

    void HideAbilities() {
        dashUI.SetActive(false);
        teleportUI.gameObject.SetActive(false);
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
    }

    void ShowAbilities() {
        dashUI.SetActive(true);
        teleportUI.gameObject.SetActive(true);
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 100);
    }
}
