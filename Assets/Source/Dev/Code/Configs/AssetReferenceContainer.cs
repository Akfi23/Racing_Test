using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "ReferenceContainer/ReferenceContainer")]
public class AssetReferenceContainer : ScriptableObject
{
    [Header("UI Ref")]
    public AssetReference ColorButtonRef;
    public AssetReference LevelButtonRef;
    public AssetReference UpgradeButtonRef;

    [Header("Configs Ref")]
    public AssetReference[] ColorConfigsRef;
    public AssetReference[] LevelConfigsRef;
    public AssetReference[] UpgradeConfigRef;

    [Header("Scenes")]
    public AssetReference[] ScenesRef;

    [Header("Cars")]
    public AssetReference CarRef;
}
