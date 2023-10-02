using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    //Stats Of The Current Damagable
    public int CurrentHP { get; }
    public int MaxHP { get; }


    //Called When Damagable Takes Damage
    public delegate void TakeDamageEvent(int Damage);
    public event TakeDamageEvent OnTakeDamage;


    //Called When Damagable Dies
    public delegate void DeathEvent(Vector3 PositionOfDeath);
    public event DeathEvent OnDeath;

    public void TakeDamage(GameObject Damager,int Damage);
}
