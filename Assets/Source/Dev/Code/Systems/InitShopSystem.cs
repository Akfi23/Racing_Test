using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InitShopSystem : GameSystemWithScreen<ShopScreen> // Rename to ColorSelectorSystem !!!
{
    [SerializeField] private ColorButtonComponent _buttonPrefab;

    private Dictionary<string, ColorConfig> _colorConfigs = new Dictionary<string, ColorConfig>();

    public override void OnInit()
    {
        screen.ShopButton.onClick.AddListener(ToggleShopWindow);

        LoadColorConfigsToDictionary();
        InitColorPanel();
        SetColorBySave(player.ColorName);
    }

    private void ToggleShopWindow()
    {
        screen.ShopWindow.gameObject.SetActive(!screen.ShopWindow.gameObject.activeSelf);
    }

    private void LoadColorConfigsToDictionary()
    {
        _colorConfigs = Resources.LoadAll<ColorConfig>("ColorConfigs").ToDictionary(x=>x.Name);
    }

    private void InitColorPanel()
    {
        foreach (var config in _colorConfigs)
        {
            var newColorButton = Instantiate(_buttonPrefab, screen.ContentParent);
            newColorButton.InitButton(config.Value.Color);
            newColorButton.Button.onClick.AddListener(() => SetCarColor(config.Value.ColorMaterial,config.Value.Name));
        }
    }

    private void SetCarColor(Material material,string name)
    {
        game.Player.BodyRenderer.material = material;
        player.ColorName = name;
    }

    private void SetColorBySave(string name)
    {
        var material = _colorConfigs[name].ColorMaterial;
        var color = _colorConfigs[name].Name;

        SetCarColor(material,color);
    }
}
