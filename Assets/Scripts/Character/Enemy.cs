using UnityEngine;

public class Enemy : AICharacter, IMoveable, IDamageable, IAttackable
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	protected override void Start()
    {
		base.Start();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Move()
	{

	}

	public void Attack()
	{

	}

	public void TakeDamage(int damageAmount, in GameObject instigator, in GameObject damageCauser)
	{
	}
}
