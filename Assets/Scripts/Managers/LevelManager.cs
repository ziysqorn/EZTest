using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] protected PrefData prefData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		Instantiate(prefData?.Pref_ControlUI);
		SpawnManager.instance?.SpawnCharacter();
        if(GameInstance.instance != null)
        {
			SpawnManager.instance?.SpawnTurrets(GameInstance.instance.GetCurLevelCount() + 2);
		}   
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
