using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySlot : MonoBehaviour
{
    public Sprite availableSprite;
    public Sprite unAvailaibleSprite;

    public bool isAvailable;

    private void Start()
    {
        UpdateAvailableUI();
    }

    private void UpdateAvailableUI()
    {
        if (isAvailable)
        {
            GetComponent<Image>().sprite = availableSprite;
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Image>().sprite = unAvailaibleSprite;
            GetComponent<Button>().interactable = false;
        }
    }
}
