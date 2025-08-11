using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    
    void Start()
    {
        UnitSelectedManager.Instance.allUnitsList.Add(gameObject);
    }


    private void OnDestroy()
    {
        UnitSelectedManager.Instance.allUnitsList.Remove(gameObject);
    }
}
