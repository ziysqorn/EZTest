using NUnit.Framework;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    [SerializeField] protected PrefData prefData;
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

    public void SpawnCharacter(int enemyCount, int allyCount)
    {
        if(enemyCount > 0)
        {
			EnemySpawnPoint[] spawnPointList = FindObjectsByType<EnemySpawnPoint>(FindObjectsSortMode.None);
            for(int i = 0; i < spawnPointList.Length && enemyCount > 0; ++i)
            {
                GameObject GB_spawnPoint = spawnPointList[i].gameObject;
                if (GB_spawnPoint != null) {
					GameObject GB_enemyChar = Instantiate(prefData?.Pref_Enemy, GB_spawnPoint.transform.position, GB_spawnPoint.transform.rotation);
                    Enemy enemyChar = GB_enemyChar.GetComponent<Enemy>();
                    if (enemyChar != null) enemyChar.IncreaseRestDuration(3.0f * i);
                    --enemyCount;
				}
                
            }
		}
        if (allyCount > 0) {
            GameObject GB_spawnPoint = FindFirstObjectByType<AllySpawnPoint>()?.gameObject;
            if (GB_spawnPoint != null) {
                Instantiate(prefData?.Pref_Ally, GB_spawnPoint.transform.position, GB_spawnPoint.transform.rotation);
            }
        } 
    }
}
