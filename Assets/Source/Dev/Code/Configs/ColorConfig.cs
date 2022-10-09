using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ShopConfig/ColorConfig")]
public class ColorConfig : ScriptableObject
{
    [SerializeField] private Material _colorMaterial;
    [SerializeField] private string _name;
    [SerializeField] private Color _color;

    public Material ColorMaterial => _colorMaterial;
    public string Name => _name;
    public Color Color => _color;
}
