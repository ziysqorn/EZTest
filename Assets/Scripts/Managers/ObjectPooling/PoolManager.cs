using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	public static PoolManager instance;
	Dictionary<string, ObjectPool> pools = new Dictionary<string, ObjectPool>();

	private void Awake()
	{
		if (!instance)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void RegisterPool(in string poolName, in ObjectPool newPool)
	{
		if (poolName != null && newPool != null && !pools.ContainsKey(poolName))
		{
			pools.Add(poolName, newPool);
		}
	}

	public ObjectPool GetPool(in string poolName)
	{
		if (poolName != null && pools.ContainsKey(poolName)) return pools[poolName];
		return null;
	}

	public GameObject ActivateObjFromPool(in string poolName, Vector3 position, Quaternion rotation)
	{
		if (poolName != null && pools.ContainsKey(poolName)) return pools[poolName].GetObj(position, rotation);
		return null;
	}

	public void RetrieveObjToPool(in string poolName, in GameObject prefab)
	{
		if (poolName != null && pools.ContainsKey(poolName) && prefab != null) pools[poolName].RetrieveObj(prefab);
	}

	public bool PoolExisted(in string poolName)
	{
		return poolName != null && pools.ContainsKey(poolName);
	}
}
