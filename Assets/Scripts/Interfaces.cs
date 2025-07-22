using UnityEngine;

public interface IMoveable
{
    public void Move();
}

public interface IAttackable
{
    public void Attack();
}

public interface IDamageable
{
    public void TakeDamage(int damageAmount, in GameObject instigator, in GameObject damageCauser);
}
