using UnityEngine;

public class HandDetector : MonoBehaviour
{
    [SerializeField] private Character owner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter(Collider other)
	{
        if (owner != null && other.gameObject != owner.gameObject && !other.tag.Equals(owner.tag))
        {
            Debug.Log("Hehe");
        }
	}
}
