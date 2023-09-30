using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassStats : MonoBehaviour
{
    public enum StatNames
    {
        AttackDamage,
        AttackSpeed,
        Agility,
        ReloadSpeed,
        Range
    }

    public class stat
    {
        //A list of abilities that this stat can apply too
        List<AbilityType> abilityTypes;
        StatNames name;
        public float value;
        float multiplier;
        int amount;

        public float Multiplier() => multiplier;
        public List<AbilityType> Typse() => abilityTypes;
        public int Amount() => amount;

        Func<int,int,float> CalculationMethod;

        public void Multi(float value) => multiplier = value;
        public void AddMulti(float value) => multiplier += value;

        public void AddAmount(int count)
        {
            float Output = (float)CalculationMethod.Invoke(count,amount);
            amount += count;
            value += Output;
        }

        public void Type(List<AbilityType> values) => abilityTypes = values;
        public void Name(StatNames value) => name = value;

        public stat(List<AbilityType> types,StatNames name, float BaseValue, float BaseMultiplier, Func<int,int,float> CalculationMethod)
        {
            this.abilityTypes = types;
            this.name = name;
            this.value = BaseValue;
            this.multiplier = BaseMultiplier;
            this.amount = 0;
            this.CalculationMethod = CalculationMethod;

        }

    }

    List<stat> Stats = new List<stat>();

    public void Start()
    {
        InitStats();
    }


    private void InitStats()
    {

        //Adds a stat with custom calculation function -> idea behind this is reusablility and fuuture balance
        float AttackDamageSum(int i1,int i2) { return (i1 * (1.2f + 1f / MathF.Max(1, i2))); }
        Func<int, int, float> AttackDamageCalculation = AttackDamageSum;
        Stats.Add(new stat(new List<AbilityType>() { AbilityType.Melee ,AbilityType.FireWeapon,AbilityType.ThrowProjectile}, StatNames.AttackDamage, 1, 1, AttackDamageCalculation));

        float AttackSpeedSum(int i1, int i2) { return (i1 * (1.2f + 1f / MathF.Max(1, i2))); }
        Func<int, int, float> AttackSpeedCalculation = AttackSpeedSum;
        Stats.Add(new stat(new List<AbilityType>() { AbilityType.Melee, AbilityType.FireWeapon, AbilityType.ThrowProjectile }, StatNames.AttackSpeed, 1, 1, AttackSpeedCalculation));

        float AgilitySum(int i1, int i2) { return (i1 * (1.2f + 1f / MathF.Max(1, i2))); }
        Func<int, int, float> AgilityCalculation = AgilitySum;
        Stats.Add(new stat(new List<AbilityType>() {}, StatNames.Agility, 1, 1, AgilityCalculation));

        float ReloadSpeedSum(int i1, int i2) { return (i1 * (1.2f + 1f / MathF.Max(1, i2))); }
        Func<int, int, float> ReloadSpeedCalculation = ReloadSpeedSum;
        Stats.Add(new stat(new List<AbilityType>() { AbilityType.FireWeapon, AbilityType.ThrowProjectile }, StatNames.ReloadSpeed, 1, 1, ReloadSpeedCalculation));

        float RangeSum(int i1, int i2) { return (i1 * (1.2f + 1f / MathF.Max(1, i2))); }
        Func<int, int, float> RangeCalculation = RangeSum;
        Stats.Add(new stat(new List<AbilityType>() { AbilityType.FireWeapon, AbilityType.ThrowProjectile }, StatNames.Range, 1, 1, RangeCalculation));
    }


}
