namespace Hollow.ScriptableObjects.Nodes
{
  /// <summary>
  /// The starting node.
  /// A story will always have an intro node.
  /// </summary>
  public class NodeIntro : NodeBase
  {
    
    public string text;
    public string nextNode;

    public NodeIntro()
    {
      this.type = NodeType.Intro;
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