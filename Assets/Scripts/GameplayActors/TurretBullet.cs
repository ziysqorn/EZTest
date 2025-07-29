using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class TurretBullet: MonoBehaviour
{
	protected float flightSpeed = 15.0f;
	protected Rigidbody rBody;
	protected BoxCollider boxCollider;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnEnable()
	{
		if (rBody == null) rBody = GetComponent<Rigidbody>();
		if (boxCollider == null) boxCollider = GetComponent<BoxCollider>();
		rBody.linearVelocity = transform.forward * flightSpeed;
	}

	protected void OnBecameInvisible()
	{
		PoolManager manager = PoolManager.instance;
		if (manager != null)
		{
			manager.RetrieveObjToPool("TurretBullet", gameObject);
		}
	}

	protected void OnTriggerEnter(Collider collision)
	{
		GameObject target = collision.gameObject;
		Character targetCharacter = target.GetComponent<Character>();
		if (targetCharacter)
		{
			GameplayStatics.ApplyDamage(target, 2, null, gameObject);
			gameObject.SetActive(false);
		}
	}
}
