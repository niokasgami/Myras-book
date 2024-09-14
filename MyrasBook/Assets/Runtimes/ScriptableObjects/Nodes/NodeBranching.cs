using System.Linq;
using Hollow.conditions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hollow.ScriptableObjects.Nodes
{
  /// <summary>
  /// The node that enable branching based on conditions
  /// if an conditions isn't met then it will go to the next branch instead
  /// it behaves likes an linear node in this context.
  /// </summary>
  [CreateAssetMenu(menuName = "hollow/create new branching node", fileName = "newBranchingNode")]
  public class NodeBranching : NodeBase
  {
    
    [Title("Branching node data")]
    [BoxGroup("branching data", showLabel: false)]
    [TextArea]
    public string text;
    [BoxGroup("branching data"), LabelWidth(70), PropertySpace(6)]
    public string mainNode;
    [BoxGroup("branching data"), LabelWidth(90), PropertySpace(6)]
    public string secondNode;

    [BoxGroup("choice data"), LabelWidth(70), PropertySpace(6), LabelText("Actions"), RequiredListLength(1,100)]
    [SerializeReference]
    public ConditionBase[] conditions = new ConditionBase[] { };

    public NodeBranching()
    {
      this.type = NodeType.Branching;
    }
    public override string Text()
    {
      return text;
    }

    public override string FetchNextNode(int index)
    {
      return IsAllConditionsMet() ? mainNode : secondNode;
    }

    private bool IsAllConditionsMet()
    {
      return conditions.All(t => t.IsFulfilled());
    }
  }
}
