using Hollow.ScriptableObjects.Nodes;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Hollow.Editor.HollowStoryEditor.Nodes
{
  public class DialogIntro : DialogBase
  {
    public DialogIntro(NodeBase nodeData) : base(nodeData)
    {
      this.type = NodeType.Intro;

      style.color = new StyleColor(Color.cyan);
    }

    protected override void GenerateInputPort()
    {
      // in this case theres none so we make it empty just in case
      inputContainer.Clear();
    }

  }
}