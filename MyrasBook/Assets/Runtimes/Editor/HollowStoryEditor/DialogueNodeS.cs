
using System;
using DefaultNamespace;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Hollow.Editor.GraphEditor
{

  public class DialogueNodeS : Node
  {
    public string GUID;

    public string DialogueText;
    public bool EntryPoint = false;

    public static Action<DialogueNodeS>  OnNodeSelected;
    
    public override void OnSelected()
    {
      OnNodeSelected?.Invoke(this);
    }
  }
}