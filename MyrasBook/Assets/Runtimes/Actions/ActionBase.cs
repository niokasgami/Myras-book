using System;
using Sirenix.OdinInspector;

namespace Hollow.Actions
{
  [Serializable]
  public abstract class ActionBase
  {
    [DisplayAsString]
    public string name;

    /// <summary>
    /// Defines an abstract method that represents an action that can be performed.
    /// </summary>
    public abstract void OnAction();
  }
}
