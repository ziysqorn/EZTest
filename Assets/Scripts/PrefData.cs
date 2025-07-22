using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class PrefData : ScriptableObject
{
	[SerializeField] public GameObject Pref_ControlUI;
}
