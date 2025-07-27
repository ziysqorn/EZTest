using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AICharacter : Character
{
    protected List<Vector3> moveDirections = new List<Vector3>() {
        new Vector3(1.0f, 0.0f, 0.0f),
        new Vector3(-1.0f, 0.0f, 0.0f),
        new Vector3(0.0f, 0.0f, 1.0f),
        new Vector3(0.0f, 0.0f, -1.0f)
    };
    protected float moveTime = 2.0f;
    [SerializeField] protected float restDuration = 3.5f;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	protected override void Start()
    {
        base.Start();
        StartCoroutine(MakeDecisionAfterDelay(restDuration));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void MakeDecision()
    {
    }

    protected IEnumerator MakeDecisionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        MakeDecision();
    }

    public void IncreaseRestDuration(float inDuration)
    {
        restDuration += inDuration;
    }
}
