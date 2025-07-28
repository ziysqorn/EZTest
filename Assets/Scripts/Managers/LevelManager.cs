using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] protected PrefData prefData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		Instantiate(prefData?.Pref_ControlUI);
		SpawnManager.instance?.SpawnCharacter();
		GameObject GB_spawnPoint = FindFirstObjectByType<PlayerSpawnPoint>()?.gameObject;
		if (GB_spawnPoint != null)
        {
			GameObject GB_mainChar = Instantiate(prefData?.Pref_MainChar, GB_spawnPoint.transform.position, GB_spawnPoint.transform.rotation);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
