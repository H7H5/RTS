using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

        var img = boxVisual.GetComponent<Image>();
        if (img) img.raycastTarget = false; // критично!

        DrawVisual();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverBlockingUI()) // тільки якщо не по UI
            {
                startPosition = Input.mousePosition;
                //endPosition = startPosition;
                selectionBox = new Rect();
                DrawVisual();
            }
        }

        if (Input.GetMouseButton(0))
        {
                //endPosition = Input.mousePosition;

                // Різниця між початком і кінцем > 5 пікселів (запобігає миттєвому вибору)
                if (startPosition != Vector2.zero)
                {
                    Vector2 currentMouse = Input.mousePosition;

                    selectionBox = new Rect(
                        Mathf.Min(startPosition.x, currentMouse.x),
                        Mathf.Min(startPosition.y, currentMouse.y),
                        Mathf.Abs(startPosition.x - currentMouse.x),
                        Mathf.Abs(startPosition.y - currentMouse.y)
                    );
                    UnitSelectedManager.Instance.DeselectAll();
                    DrawVisual();
                    DrawSelection();
                    SelectUnits();   ///////////////////
                }
                else
                {
                    DrawVisual();
                }
        }
        if (Input.GetMouseButtonUp(0))
        {
            SelectUnits();
            startPosition = Vector2.zero;
            //endPosition = Vector2.zero;
            selectionBox = new Rect();
            DrawVisual();
        }
    }


    private void DrawVisual()
    {
        if (selectionBox.width > 0 && selectionBox.height > 0)
        {
            Vector2 center = new Vector2(selectionBox.x + selectionBox.width / 2,
                                         selectionBox.y + selectionBox.height / 2);

            boxVisual.position = center;
            boxVisual.sizeDelta = new Vector2(selectionBox.width, selectionBox.height);
        }
        else
        {
            boxVisual.sizeDelta = Vector2.zero; // ховаємо, якщо нема прямокутника
        }
    }
    private bool IsPointerOverBlockingUI()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            // Перевіряємо, чи це не наші юнітські UI елементи
            if (!result.gameObject.GetComponentInParent<Unit>())
            {
                return true; // це чужий UI → блокуємо
            }
        }
        return false; // або немає UI → можна виділяти
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
