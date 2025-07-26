using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance instance;
    protected int enemyCount = 0;
    protected int levelCount = 10;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Awake()
	{
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}
	void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetEnemyCount()
    {
        return enemyCount;
    }

    public int GetLevelCount()
    {
        return levelCount;
    }


    public void SetEnemyCount(int count)
    {
        if(count >= 0) enemyCount = count;
    }

    public void SetLevelCount(int count)
    {
        if(count >= 0) levelCount = count;  
    }

    public void DecreaseEnemyCount(int amount)
    {
        enemyCount -= amount;
        if(enemyCount <= 0)
        {
            EndLevel();
        }
    }

    public void EndLevel()
    {
        Debug.Log("All enemies defeated !");
    }
}
