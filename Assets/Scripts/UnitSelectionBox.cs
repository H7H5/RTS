using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSelectionBox : MonoBehaviour
{
    Camera cam;

    [SerializeField]
    RectTransform boxVisual;

    Rect selectionBox;

    Vector2 startPosition;
    Vector2 endPosition;
    void Start()
    {
        cam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        DrawVisual();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject()) // тільки якщо не по UI
            {
                startPosition = Input.mousePosition;

                selectionBox = new Rect();
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject()) // тільки якщо не по UI
            {
                endPosition = Input.mousePosition;

                // Різниця між початком і кінцем > 5 пікселів (запобігає миттєвому вибору)
                if (Mathf.Abs(endPosition.x - startPosition.x) > 5f ||
                    Mathf.Abs(endPosition.y - startPosition.y) > 5f)
                {
                    UnitSelectedManager.Instance.DeselectAll();
                    DrawVisual();
                    DrawSelection();
                    SelectUnits();
                }
                else
                {
                    DrawVisual();
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            SelectUnits();
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisual();
        }
    }

    private void DrawVisual()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;

        boxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(MathF.Abs(boxStart.x - boxEnd.x), MathF.Abs(boxStart.y - boxEnd.y));

        boxVisual.sizeDelta = boxSize;
    }

    private void DrawSelection()
    {
        if (Input.mousePosition.x < startPosition.x)
        {
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = Input.mousePosition.x;
        }


        if (Input.mousePosition.y < startPosition.y)
        {
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = Input.mousePosition.y;
        }
    }

    private void SelectUnits()
    {
        foreach (var unit in UnitSelectedManager.Instance.allUnitsList)
        {
            if (selectionBox.Contains(cam.WorldToScreenPoint(unit.transform.position)))
            {
                 UnitSelectedManager.Instance.DragSelect(unit);
            }
        }
    }
}
