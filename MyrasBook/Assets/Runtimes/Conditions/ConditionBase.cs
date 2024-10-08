﻿using System;
using Sirenix.OdinInspector;

namespace Hollow.conditions
{
  [Serializable]
  public abstract class ConditionBase
  {
    [DisplayAsString, LabelWidth(70)]
    public string name;
    
    // 
    
    public abstract bool IsFulfilled();
  }
}
