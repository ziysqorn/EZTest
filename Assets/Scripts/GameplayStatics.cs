using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameplayStatics
{
	public static void ApplyDamage(in GameObject damagedObj, int damageAmount, in GameObject instigator, in GameObject damageCauser)
	{
		IDamageable damageable = damagedObj?.GetComponent<IDamageable>();
		damageable?.TakeDamage(damageAmount, instigator, damageCauser);
	}
}
