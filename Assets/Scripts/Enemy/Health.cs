using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    public int CurrentHP { get; private set; }
    public int MaxHP { get; }
    public event IDamagable.TakeDamageEvent OnTakeDamage;
    public event IDamagable.DeathEvent OnDeath;

    public void TakeDamage(GameObject Damager, int Damage)
    {
        var damageTaken = Mathf.Clamp(Damage, 0, CurrentHP);
        CurrentHP -= damageTaken;

        //Check if we actually took damage this call
        if (damageTaken != 0)
        {
            OnTakeDamage?.Invoke(damageTaken);
        }

        //Check if the damage caused us to die
        if (CurrentHP == 0 && damageTaken != 0)
        {
            OnDeath?.Invoke(transform.position);
        }
    }
}