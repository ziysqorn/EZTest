using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance instance;
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
}
