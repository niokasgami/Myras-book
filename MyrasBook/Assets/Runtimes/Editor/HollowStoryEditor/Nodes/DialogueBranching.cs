using Hollow.ScriptableObjects.Nodes;
using UnityEditor.Experimental.GraphView;

namespace Hollow.Editor.HollowStoryEditor.Nodes
{
  public class DialogueBranching : DialogBase
  {
    
    public DialogueBranching(NodeBase nodeData) : base(nodeData)
    {
    }

    protected override void GenerateOutputPort()
    {
      var mainPort = GeneratePort(Direction.Output);
      mainPort.portName = "A";
      output.Add(mainPort);

      var secondPort = GeneratePort(Direction.Output);
      secondPort.portName = "B";
      output.Add(secondPort);
      
      outputContainer.Add(output[0]);
      outputContainer.Add(output[1]);
    }
  }
}