using System;
using Hollow.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hollow.Actions
{
  [Serializable, InlineProperty]
  public class RemoveItem : ActionBase
  {
    [BoxGroup("Item Settings")]
    [Tooltip("The ID of the item required for this condition")]
    [LabelWidth(70)]
    public string itemId;

    public RemoveItem()
    {
      this.name = "RemoveItem";
    }

    public override void OnAction()
    {
      var player = Player.Instance;
      if (player.HasItem(itemId))
      {
        player.RemoveItem(itemId);
      }
    }

  }

}
