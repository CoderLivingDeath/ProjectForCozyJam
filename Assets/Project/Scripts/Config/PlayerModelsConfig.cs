using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerModelsConfig", menuName = "Scriptable Objects/PlayerModelsConfig")]
public class PlayerModelsConfig : ScriptableObject
{
    public AnimatorController Normal;
    public AnimatorController Small;
    public AnimatorController Large;

    public PlayerModelType StartPlayerModelType;

    public float NoramalSpeed;
    public float Smallspeed;
    public float LargeSpeed;
}
