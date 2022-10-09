using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderSettingsSystem : GameSystemWithScreen<MenuScreen>
{
    public override void OnInit()
    {
        LoadRenderSettings(player.RenderSettingsIndex);

        screen.RenderSettingList.value = QualitySettings.GetQualityLevel();
        screen.RenderSettingList.onValueChanged.AddListener(SetRenderSetting);
    }

    private void SetRenderSetting(int index)
    {
        QualitySettings.SetQualityLevel(index);
        QualitySettings.renderPipeline = config.QualityPresets[index];
        player.RenderSettingsIndex = index;
    }

    private void LoadRenderSettings(int index)
    {
        SetRenderSetting(index);
    }
}
