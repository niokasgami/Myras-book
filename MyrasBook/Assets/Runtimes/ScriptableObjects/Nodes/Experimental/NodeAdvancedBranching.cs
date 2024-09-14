using System;
using System.Linq;
using Hollow.conditions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hollow.ScriptableObjects.Nodes.Experimental
{
  /// <summary>
  /// An experimental implementation of the branching node
  /// this one allow a more advanced sets of branching while the default one only allow
  /// two branching
  /// </summary>
  [CreateAssetMenu(menuName = "hollow/create new advanced branching node", fileName = "newAdvancedBranchingNode")]
  public class NodeAdvancedBranching : NodeBase
  {
    
    [Title("Branching node data")]
    [BoxGroup("branching data", showLabel: false)]
    [TextArea]
    public string text;

    [BoxGroup("branching data"), LabelWidth(70), PropertySpace(6), RequiredListLength(2,9999)]
    public BranchingStruct[] branches = new BranchingStruct[] { };
    public override string Text()
    {
      return text;
    }

    public NodeAdvancedBranching()
    {
      this.type = NodeType.AdvancedBranching;
    }
    public override string FetchNextNode(int index)
    {
      return branches.Where((t, i) => IsAllConditionsMet(i)).Select(t => t.node).FirstOrDefault();
    }

    private bool IsAllConditionsMet(int index)
    {
      return branches[index].conditions.All(t => t.IsFulfilled());
    }
  }

  [Serializable]
  public class BranchingStruct
  {
    [BoxGroup("Main data"), LabelText("node"), LabelWidth(100), PropertySpace(spaceBefore:5)]
    public string node;
        
    [PropertySpace(spaceBefore:10)]
    [SerializeReference, InlineProperty, LabelText("Conditions"),RequiredListLength(1,99999)]
    [ListDrawerSettings(ShowIndexLabels = false, DraggableItems = true)]
    public ConditionBase[] conditions = new ConditionBase[] { };
  }
}
