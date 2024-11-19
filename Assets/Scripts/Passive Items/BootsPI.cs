using UnityEngine;

public class BootsPI : PassiveItem
{
   protected override void ApplyModifier()
   {
      player.currentMoveSpeed *= 1 + passiveItemData.Multiplier / 100f;
   }
}
