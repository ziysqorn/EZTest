using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour
{
    public static GameInstance instance;
    protected int enemyCount = 0;
    protected int curEnemyCount = 0;
    protected int levelCount = 10;
    protected int curLevelCount = 0;
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

    public int GetCurLevelCount()
    {
        return curLevelCount;
    }


    public void SetEnemyCount(int count)
    {
        if(count >= 0)
        {
			enemyCount = count;
            curEnemyCount = count;
		}
    }

    public void ResetGame()
    {
        curLevelCount = 1;
    }

    public void SetLevelCount(int count)
    {
        if(count >= 0) levelCount = count;  
    }

    public void DecreaseEnemyCount(int amount)
    {
        curEnemyCount -= amount;
        if(curEnemyCount <= 0)
        {
            EndLevel();
        }
    }

    public void EndLevel()
    {
        Debug.Log("All enemies defeated !");
        if(curLevelCount == levelCount)
        {
            SceneManager.LoadScene("StartLevel");
        }
        ++curLevelCount;
        SceneManager.LoadScene("MainLevel");
    }
}
