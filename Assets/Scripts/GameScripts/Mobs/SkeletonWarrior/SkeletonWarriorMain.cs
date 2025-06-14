using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorMain : MonoBehaviour
{
    public HealthBar health_bar;
    public ManaBar mana_bar;

    private SkeletonWarriorStats skeletonWarriorStats;

    private float current_health;
    private float max_health;
    private float current_mana;
    private float max_mana;
    private float defence;
    private float wisdom;
    private float vitality;
    private float damage_take;

    private void Start()
    {
        skeletonWarriorStats = GetComponent<SkeletonWarriorStats>();

        max_health = skeletonWarriorStats.GetMaxHealth();
        max_mana = skeletonWarriorStats.GetMaxMana();
        defence = skeletonWarriorStats.GetDefence();
        wisdom = skeletonWarriorStats.GetWisdom();
        vitality = skeletonWarriorStats.GetVitality();
        current_health = max_health;
        current_mana = max_mana;

        health_bar.SetSliderMax(current_health);
        mana_bar.SetSliderMax(current_mana);

        //UNCOMMENT IF NEED TO GET ATTACK COLLIDER POINTS
        //LogColliderPoints();
    }

    private void Update()
    {
        //MAX CAP OF HEAL (TO PREVENT OVERHEAL/OVERREGEN)
        if (current_health > max_health) { current_health = max_health; }
        if (current_mana > max_mana) { current_mana = max_mana; }

        //REGEN LOGIC
        if (current_health < max_health) { HealthRegen(); }
        if (current_mana < max_mana) { ManaRegen(); }

        //DEATH LOGIC
        if (current_health <= 0) { Die(); }

    }

    public void TakeDamage(float amount)
    {
        if (amount < 0) return;

        damage_take = amount - defence;
        current_health -= damage_take;
        health_bar.SetSlider(current_health);
    }
    public void Heal(float amount)
    {
        current_health += amount;
        health_bar.SetSlider(current_health);
    }

    private void HealthRegen()
    {
        current_health += Mathf.Max(vitality, 0);
        health_bar.SetSlider(current_health);
    }

    private void ManaRegen()
    {
        current_mana += Mathf.Max(wisdom, 0);
        mana_bar.SetSlider(current_mana);
    }

    private void Die()
    {
        Debug.Log("mob died....");
    }

    //TO GET  ATTACK COLLIDER POINTS
    private void LogColliderPoints()
    {
        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
        if (polygonCollider != null)
        {
            foreach (var point in polygonCollider.points)
            {
                Debug.Log($"Point: {point}");
            }
        }
        else
        {
            Debug.LogWarning("PolygonCollider2D not found on the object.");
        }
    }

}