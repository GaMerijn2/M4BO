using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth
{
    private int currentHealth;
    private int currentMaxHealth;

    public int Health
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
        }
    }

    public int MaxHealth
    {
        get
        {
            return currentMaxHealth;
        }
        set
        {
            currentMaxHealth = value;
        }
    }

    public UnitHealth (int health, int maxhealth)
    {
        currentHealth = health;
        currentMaxHealth = maxhealth;
    }
    public void DmgUnit(int dmgAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= dmgAmount;
        }
    }
    public void HealUnit(int healAmount)
    {
        if (currentHealth < currentMaxHealth)
        {
            currentHealth += healAmount;
        }
        if (currentHealth > currentMaxHealth) 
        {
            currentHealth = currentMaxHealth;
        }
    }


}
