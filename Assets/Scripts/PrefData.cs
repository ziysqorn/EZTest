using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class PrefData : ScriptableObject
{
	[SerializeField] public GameObject Pref_ControlUI;
	[SerializeField] public GameObject Pref_MainChar;
	[SerializeField] public GameObject Pref_Ally;
	[SerializeField] public GameObject Pref_Enemy;
}
