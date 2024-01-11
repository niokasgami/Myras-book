using System;
using System.Collections.Generic;
using Hollow.conditions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Hollow.ScriptableObjects.Nodes
{
  [CreateAssetMenu(menuName = "hollow/create new Choice node", fileName = "newChoiceNode")]
  public class NodeChoice : NodeBase
  {
    [Title("Choice node data")]
    [BoxGroup("choice data", showLabel: false)]
    [TextArea]
    public string text;
    
    [BoxGroup("choice data"), LabelWidth(70), PropertySpace(6), LabelText("Conditions")]
    public List<ChoiceStruct> list;

    public NodeChoice()
    {
      this.type = NodeType.Choice;
    }

    public override string Text()
    {
      return text;
    }

    public override string FetchNextNode(int index)
    {
      return list[index].nextNode;
    }
  }

  [Serializable]
  public struct ChoiceStruct
  {
    [BoxGroup("Main data"), LabelText("Text"), LabelWidth(100)]
    public string text;
    [FormerlySerializedAs("next")]
    [BoxGroup("Main data"), LabelText("Next Node"), LabelWidth(100)]
    public string nextNode;
    
    [PropertySpace(spaceBefore:10)]
    [SerializeReference, InlineProperty, LabelText("Conditions")]
    [ListDrawerSettings(ShowIndexLabels = false, DraggableItems = true)] // Provide a handy list view for the Conditions
    public List<ConditionBase> conditions;
  }

}
