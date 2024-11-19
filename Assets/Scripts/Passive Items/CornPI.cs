using UnityEngine;

public class CornPI : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentMight *= 1 + passiveItemData.Multiplier / 100f;
    }
}
