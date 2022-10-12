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

    [Header("Opponent Stuff")]
    [HorizontalLine(color: EColor.Green)]
    [SerializeField] private float _acceleration;
    [SerializeField] private float _opponentEngineForce;

    [Header("Player Stats")]
    [HorizontalLine(color: EColor.Blue)]
    [SerializeField] private int _baseHealth;

    [Header("Render Presets")]
    [HorizontalLine(color: EColor.Yellow)]
    [SerializeField] private RenderPipelineAsset[] _qualityPresets;

    [Header("Render Presets")]
    [HorizontalLine(color: EColor.Orange)]
    [SerializeField] private AssetReferenceContainer _referenceContainer;

    public float BaseForce => _baseForce;
    public float BrakeForce => _brakeForce;
    public float MaxSteerAngle => _maxSteerAngle;
    public float Acceleration => _acceleration;
    public float OpponentForce => _opponentEngineForce;
    public int BaseHealth => _baseHealth;
    public RenderPipelineAsset[] QualityPresets => _qualityPresets;
    public AssetReferenceContainer ReferenceContainer => _referenceContainer;
}