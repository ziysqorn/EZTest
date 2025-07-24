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
        if (owner != null && other && other.gameObject && other.gameObject != owner.gameObject && !other.tag.Equals(owner.tag))
        {
            CustomStateMachine stateMachine = owner.GetComponent<CustomStateMachine>();
            ICharacterState characterState = stateMachine.GetCurrentState();
            if(characterState.GetType() == typeof(AttackState))
            {
				Debug.Log("Hehe");
				GameplayStatics.ApplyDamage(other.gameObject, 5, owner.gameObject, gameObject);
			}
        }
	}
}
