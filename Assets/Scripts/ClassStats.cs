using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassStats : MonoBehaviour
{
    public struct stat
    {
        private string name;
        private float value;
        private float multiplier;
        private float count;

        public float Value() => value * multiplier;
        public float Multiplier() => multiplier;
        public string Name() => name;

        public void Multi(float value) => multiplier = value;
        public void AddMulti(float value) => multiplier += value;
        public void Value(float value) => value = value;
        public void Name(string value) => name = value;

    }

    Dictionary<string,stat> Stats = new Dictionary<string, stat>();

    public stat? GetStat(string name)
    {
        if(Stats.TryGetValue(name, out stat Found)) return Found;
        return null;
    }

    public void SetStat(string name, stat newStat)
    {
        Stats[name] = newStat;
    }

    public float GetValue(string name)
    {
        return Stats[name].Value();
    }

    public void AddMultiplier(string name, float valueToAdd)
    {
        Stats[name].AddMulti(valueToAdd);
    }
}
