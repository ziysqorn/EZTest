using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button btn_OneVOne;
    [SerializeField] private Button btn_OneVMany;
    [SerializeField] private Button btn_ManyVMany;
    [SerializeField] private PrefData prefData;
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
        Instantiate(prefData?.Pref_ControlUI);
        CameraManager.instance?.CameraSetup();
        SpawnManager.instance?.SpawnCharacter(1, 0);
        GameInstance.instance?.SetEnemyCount(1);
        Destroy(gameObject);
    }

    void OneVManyClick()
    {
		Debug.Log("One vs Many click");
		Instantiate(prefData?.Pref_ControlUI);
        CameraManager.instance?.CameraSetup();
        SpawnManager.instance?.SpawnCharacter(2, 0);
		GameInstance.instance?.SetEnemyCount(2);
		Destroy(gameObject);
	}

    void ManyVManyClick()
    {
		Debug.Log("Many vs Many click");
		Instantiate(prefData?.Pref_ControlUI);
        CameraManager.instance?.CameraSetup();
        SpawnManager.instance?.SpawnCharacter(2, 1);
        GameInstance.instance.SetEnemyCount(2);
		Destroy(gameObject);
	}
}
