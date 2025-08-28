using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Constructable : MonoBehaviour, IDamageable
{
    private float constHealth;
    public float constMaxHealth;

    public HealthTracker healthTracker;

    public bool isEnemy = false;

    NavMeshObstacle obstacle;

    public BuildingType buildingType;
    private void Start()
    {
        constHealth = constMaxHealth;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthTracker.UpdateSliderValue(constHealth, constMaxHealth);
        if(constHealth <= 0)
        {
            ResourceManager.Instance.UpdateBuildingsChanged(buildingType, false);

            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        constHealth -= damage;
        UpdateHealthUI();
    }

    public void ConstructableWasPlaced()
    {
        healthTracker.gameObject.SetActive(true);
        ActivateObstacle();
        if (isEnemy)
        {
            gameObject.tag = "Enemy";
        }
    }

    private void ActivateObstacle()
    {
        obstacle = GetComponentInChildren<NavMeshObstacle>();
        obstacle.enabled = true;
    }
}
