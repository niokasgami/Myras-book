using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hollow.ScriptableObjects
{
  
  // VARIABLES scriptables is just for initials Default Value it shouldnt be used for 
  // storing or changing the value. 
  // TODO: Maybe change this to default value
  [CreateAssetMenu(menuName = "hollow/Create Variable Containers", fileName = "GameVariables")]
  public class VariableOverview : ScriptableObject
  {
    [TitleGroup("Local variables")]
    [ListDrawerSettings(ShowFoldout = false)]
    public List<VariableContainer> local;
    
    [TitleGroup("Global variables")]
    [ListDrawerSettings(ShowFoldout = false)]
    public List<VariableContainer> global;
  }
  
    public enum VariableType
    {
      String,
      Boolean,
      Int,
      Float,
    }
    
    
    // here it is serializabe so we could use that structure for the save system.
    [Serializable]
    public struct VariableContainer
    {
      [HorizontalGroup("gr", LabelWidth = 40)]
      public string name;
      [HorizontalGroup("gr")]
      [EnumToggleButtons]
      public VariableType type;
      [HorizontalGroup("gr")]
      public string value;
    }
}
