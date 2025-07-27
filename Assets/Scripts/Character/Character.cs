using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 10;
    [SerializeField] protected int curHealth;
	[SerializeField] protected float speed = 1.0f;
    public Animator animator;
    protected CharacterController characterController;
	protected CustomStateMachine stateMachine;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	protected virtual void Start()
    {
        stateMachine = GetComponent<CustomStateMachine>();
		characterController = GetComponent<CharacterController>();
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
