using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    protected float delayShoot = 3.0f;
    [SerializeField] protected PrefData prefData;
    [SerializeField] protected GameObject ejectPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PoolManager poolManager = PoolManager.instance;
        if (poolManager != null) {
            if (!poolManager.PoolExisted("TurretBullet")) poolManager.RegisterPool("TurretBullet", new ObjectPool(prefData.Pref_Bullet)); 
        }
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDelayShoot(float inDelay)
    {
        delayShoot = inDelay;
    }

    IEnumerator Shoot()
    {
        while(0 == 0)
        {
            yield return new WaitForSeconds(delayShoot);
			PoolManager poolManager = PoolManager.instance;
			if (poolManager != null)
			{
                poolManager.ActivateObjFromPool("TurretBullet", ejectPoint.transform.position, transform.rotation);
			}
		}
    }
}
