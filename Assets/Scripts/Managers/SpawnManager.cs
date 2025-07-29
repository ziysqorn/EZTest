using NUnit.Framework;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int enemyCount = 0;
    public int allyCount = 0;
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

    public void SpawnCharacter()
    {
        if(enemyCount > 0)
        {
            int curEnemyCount = enemyCount;
			EnemySpawnPoint[] spawnPointList = FindObjectsByType<EnemySpawnPoint>(FindObjectsSortMode.None);
            for(int i = 0; i < spawnPointList.Length && curEnemyCount > 0; ++i)
            {
                GameObject GB_spawnPoint = spawnPointList[i].gameObject;
                if (GB_spawnPoint != null) {
					GameObject GB_enemyChar = Instantiate(prefData?.Pref_Enemy, GB_spawnPoint.transform.position, GB_spawnPoint.transform.rotation);
                    Enemy enemyChar = GB_enemyChar.GetComponent<Enemy>();
                    if (enemyChar != null) enemyChar.IncreaseRestDuration(3.0f * i);
                    --curEnemyCount;
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

    public void SpawnTurrets(int turretCount)
    {
        TurretSpawnArea[] turretSpawnAreas = FindObjectsByType<TurretSpawnArea>(FindObjectsSortMode.None);
        for(int i = 0; i < turretCount; ++i)
        {
			int randomPoint = Random.Range(0, turretSpawnAreas.Length);
			TurretSpawnArea chosenArea = turretSpawnAreas[randomPoint];
            float spawnPointLocalX = Random.Range(chosenArea.LeftPoint.transform.localPosition.x, chosenArea.RightPoint.transform.localPosition.x);
            Vector3 spawnPoint = chosenArea.transform.TransformPoint(new Vector3(spawnPointLocalX, 0.0f, 0.0f));
            GameObject GB_Turret = Instantiate(prefData?.Pref_Turret, spawnPoint, chosenArea.LeftPoint.transform.rotation);
            if (GB_Turret != null) { 
                Turret turret = GB_Turret.GetComponent<Turret>();
                turret?.SetDelayShoot(Random.Range(3.5f, 10.0f));
            }
		}
    }
}
