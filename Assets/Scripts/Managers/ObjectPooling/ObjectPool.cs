using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
	protected GameObject objPref;
	private int poolSize = 20;
	Queue<GameObject> objQueue = new Queue<GameObject>();

	public ObjectPool(GameObject pref)
	{
		objPref = pref;
		if (objPref)
		{
			for (int i = 0; i < poolSize; ++i)
			{
				GameObject newObj = Object.Instantiate(objPref);
				newObj.SetActive(false);
				objQueue.Enqueue(newObj);
			}
		}
	}

	public GameObject GetObj(Vector3 position, Quaternion rotation)
	{
		if (objQueue.Count == 0 && objPref)
		{
			GameObject newObj = Object.Instantiate(objPref);
			newObj.SetActive(false);
			objQueue.Enqueue(newObj);
		}
		GameObject frontObj = objQueue.Dequeue();
		if (!frontObj)
		{
			frontObj = Object.Instantiate(objPref);
			frontObj.SetActive(false);
		}
		frontObj.transform.position = position;
		frontObj.transform.rotation = rotation;
		frontObj.SetActive(true);
		return frontObj;
	}

	public void RetrieveObj(GameObject obj)
	{
		if (obj)
		{
			obj.SetActive(false);
			objQueue.Enqueue(obj);
		}
	}
}
