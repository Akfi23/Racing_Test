using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Config/GameConfig")]
public sealed class GameConfig : ScriptableObject
{
    // Example
    // [SerializeField] [BoxGroup("Moving")] private float moveSpeed;
    // public float MoveSpeed => moveSpeed;

    [Header("Car Stuff")]
    [HorizontalLine(color: EColor.Green)]
    [SerializeField] private float _baseForce;
    [SerializeField] private float _brakeForce;
    [SerializeField] private float _maxSteerAngle;

    [Header("Player Stats")]
    [HorizontalLine(color: EColor.Blue)]
    [SerializeField] private float _baseHealth;

    [Header("Render Presets")]
    [HorizontalLine(color: EColor.Blue)]
    [SerializeField] private RenderPipelineAsset[] _qualityPresets; 

    public float BaseForce => _baseForce;
    public float BrakeForce => _brakeForce;
    public float MaxSteerAngle => _maxSteerAngle;
    public float BaseHealth => _baseHealth;
    public RenderPipelineAsset[] QualityPresets => _qualityPresets;
}