using UnityEngine;


namespace Hollow.ScriptableObjects.Nodes.Experimental
{
  public class NodeDialogue : NodeBase
  {
    
    public DialogueStruct[] dialogues = new DialogueStruct[1];
    private int index = 0;
    
    public NodeDialogue()
    {
      this.type = NodeType.Dialogue;
    }
    
    public override string Text()
    {
      return dialogues[index].dialogue;
    }

    public Sprite Sprite()
    {
      return dialogues[index].bust;
    }

    public override string FetchNextNode(int index)
    {
      throw new System.NotImplementedException();
    }

    public void ProcessDialogue()
    {
      if (index > dialogues.Length)
      {
        
      }
    }
  }

  public struct DialogueStruct
  {
    public string dialogue;
    public Sprite bust;
  }
}
