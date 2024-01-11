using Hollow.Actions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hollow.ScriptableObjects.Nodes
{
  [CreateAssetMenu(menuName = "hollow/createActionNewNode", fileName = "newActionNode")]
  public class NodeAction : NodeBase
  {
    [Title("Action node data")]
    [BoxGroup("choice data", showLabel: false)]
    [TextArea]
    public string text;
    
    [BoxGroup("choice data"), LabelWidth(70), PropertySpace(6)]
    public string nextNode;
    
    [BoxGroup("choice data"), LabelWidth(70), PropertySpace(6), LabelText("Actions")]
    [SerializeReference]
    public ActionBase[] actions;

    public NodeAction()
    {
      this.type = NodeType.Action;
    }
    
    public override string Text()
    {
      return text;
    }

    public override string FetchNextNode(int index)
    {
      return nextNode;
    }
  }

}
