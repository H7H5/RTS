using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button buildButton;
    public PlacementSystem placement;

    private void Start()
    {
        buildButton.onClick.AddListener(() => Construct(0)); // id 0
    }

    private void Construct(int id)
    {
        placement.StartPlacement(id);
    }

}
