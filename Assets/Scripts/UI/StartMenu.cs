using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button btn_OneVOne;
    [SerializeField] private Button btn_OneVMany;
    [SerializeField] private Button btn_ManyVMany;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btn_OneVOne.onClick?.AddListener(OneVOneClick);
        btn_OneVMany.onClick?.AddListener(OneVManyClick);
        btn_ManyVMany.onClick?.AddListener(ManyVManyClick);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnDestroy()
	{
        btn_OneVOne.onClick?.RemoveAllListeners();
        btn_OneVMany.onClick?.RemoveAllListeners();
        btn_ManyVMany.onClick?.RemoveAllListeners();
	}

	void OneVOneClick()
    {
        Debug.Log("One vs One click");
        if(SpawnManager.instance != null)
        {
            SpawnManager.instance.enemyCount = 1;
			SpawnManager.instance.allyCount = 0;
		}
        GameInstance.instance?.SetEnemyCount(1);
        GameInstance.instance?.ResetGame();
        SceneManager.LoadScene("MainLevel");
    }

    void OneVManyClick()
    {
		Debug.Log("One vs Many click");
		if (SpawnManager.instance != null)
		{
			SpawnManager.instance.enemyCount = 2;
			SpawnManager.instance.allyCount = 0;
		}
		GameInstance.instance?.SetEnemyCount(2);
		GameInstance.instance?.ResetGame();
		SceneManager.LoadScene("MainLevel");
	}

    void ManyVManyClick()
    {
		Debug.Log("Many vs Many click");
		if (SpawnManager.instance != null)
		{
			SpawnManager.instance.enemyCount = 2;
			SpawnManager.instance.allyCount = 1;
		}
		GameInstance.instance?.SetEnemyCount(2);
		GameInstance.instance?.ResetGame();
		SceneManager.LoadScene("MainLevel");
	}
}
