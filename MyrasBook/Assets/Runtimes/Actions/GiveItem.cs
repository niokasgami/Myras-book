using System;
using Hollow.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hollow.Actions
{
  [Serializable, InlineProperty]
  public class GiveItem : ActionBase
  {
    [BoxGroup("Item Settings")]
    [Tooltip("The ID of the item required for this condition")]
    public string itemId;

    public GiveItem()
    {
      this.name = "GiveItem";
    }

    public override void OnAction()
{
    var player = Player.Instance;
    var slot = player.FindEmptySlot();
    player.AddItem(itemId, slot);
}
  }
}
