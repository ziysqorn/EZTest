using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
	[SerializeField] protected PrefData prefData;
	protected Dictionary<string, Camera> cameraMap = new Dictionary<string, Camera>();
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
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

	public void AddCamera(string name, Camera camera)
	{
		if (!cameraMap.ContainsKey(name)) cameraMap.Add(name, camera);
	}

	public void SwitchToCamera(string name)
	{
		foreach(var camera in cameraMap)
		{
			if (camera.Key != name) camera.Value.enabled = false;
			else camera.Value.enabled = true;
		}
	}

	public void SwitchToCamera(Camera inCamera)
	{
		foreach(var camera in cameraMap.Values)
		{
			if (inCamera != camera) camera.enabled = false;
			else camera.enabled = true;
		}
	}

	public void CameraSetup()
	{
		GameObject GB_spawnPoint = FindFirstObjectByType<PlayerSpawnPoint>()?.gameObject;
		if (GB_spawnPoint != null)
		{
			Camera.main.enabled = false;
			GameObject GB_mainChar = Instantiate(prefData?.Pref_MainChar, GB_spawnPoint.transform.position, GB_spawnPoint.transform.rotation);
			MainCharacter mainChar = GB_mainChar?.GetComponent<MainCharacter>();
			if (mainChar)
			{
				mainChar.characterCamera.enabled = true;
			}
		}
	}

	public Camera GetCamera(string name)
	{
		if (cameraMap.ContainsKey(name)) { 
			return cameraMap[name]; 
		}
		return null;
	}
}
