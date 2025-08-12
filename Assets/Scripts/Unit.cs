using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private float unitHealth;
    public float unitMaxHealth;

    public HealthTracker healthTracker;
    void Start()
    {
        UnitSelectedManager.Instance.allUnitsList.Add(gameObject);

        unitHealth = unitMaxHealth;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthTracker.UpdateSliderValue(unitHealth, unitMaxHealth);
        if(unitHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        UnitSelectedManager.Instance.allUnitsList.Remove(gameObject);
    }

    internal void TakeDamage(int damageToInflict)
    {
        unitHealth -= damageToInflict;
        UpdateHealthUI();
    }
}
