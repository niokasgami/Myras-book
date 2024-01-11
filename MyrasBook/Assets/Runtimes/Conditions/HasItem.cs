using System;
using Hollow.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hollow.conditions
{
  [Serializable]
  public class HasItem : ConditionBase
  {
    [BoxGroup("Item Settings")]
    [Tooltip("The ID of the item required for this condition")]
    public string itemId;
    
    public HasItem()
    {
      this.name = "HasItem";
    }
    
    public override bool IsFulfilled()
    {
      var player = Player.Instance;
      return player.HasItem(itemId);
    }
  }
}
