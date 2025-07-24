using UnityEngine;

public interface IMoveable
{
    public void Move(in Vector3 moveDirection);
}

public interface IAttackable
{
    public void Attack();
}

public interface IDamageable
{
    public void TakeDamage(int damageAmount, in GameObject instigator, in GameObject damageCauser);
}

public interface IDieable
{
    public void Die();
}

public interface ICharacterState
{
    public void Enter();
    public void Update();
    public void Exit();

    public bool CanSwitchTo(ICharacterState inState);
}


