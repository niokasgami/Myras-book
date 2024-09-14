
using Hollow.Management;
using Hollow.Objects;
using Hollow.ScriptableObjects;
using Hollow.ScriptableObjects.Nodes;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Demos.RPGEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;


namespace Hollow.Editor
{
  /// <summary>
  ///  A temporary solutions until I figure out how to do the graph editor
  /// I am doing this to make the initial demo
  /// </summary>
  public class StoryEditor: OdinEditorWindow
  {

    
    [MenuItem("Tools/Hollow/Open Story Editor")]
    private static void OpenWindow()
    {

      var window = GetWindow<StoryEditor>();
      window.position = GUIHelper.GetEditorWindowRect().AlignCenter(200, 500);
      window.titleContent.image = EditorIcons.Pen.Active;
    }
    


    [Button(ButtonSizes.Large)]
    [PropertySpace(10)]
    [FoldoutGroup("Node Creation")]
    void CreateLinearNode()
    {
      ScriptableObjectCreator.ShowDialog<NodeLinear>("Assets/Scriptables/Story", obj =>
      {
        obj.key = obj.name;
      });
    }
    
    [FoldoutGroup("Node Creation")]
    [Button(ButtonSizes.Large)]
    [PropertySpace(10)]
    void CreateChoiceNode()
    {
      ScriptableObjectCreator.ShowDialog<NodeChoice>("Assets/Scriptables/Story", obj =>
      {
        obj.key = obj.name;
      });
    }

    [FoldoutGroup("Node Creation")]
    [Button(ButtonSizes.Large)]
    [PropertySpace(10)]
    void CreateActionNode()
    {
      ScriptableObjectCreator.ShowDialog<NodeAction>("Assets/Scriptables/Story", obj =>
      {
        obj.key = obj.name;
      });
    }
    
    [FoldoutGroup("Node Creation")]
    [Button(ButtonSizes.Large)]
    [PropertySpace(spaceBefore:10, spaceAfter:10)]
    void CreateBranchingNode()
    {
      ScriptableObjectCreator.ShowDialog<NodeBranching>("Assets/Scriptables/Story", obj =>
      {
        obj.key = obj.name;
      });
    }
    
    [FoldoutGroup("Node Creation")]
    [Button(ButtonSizes.Large)]
    [PropertySpace(spaceBefore:10, spaceAfter:10)]
    void CreateIntroNode()
    {
      ScriptableObjectCreator.ShowDialog<NodeIntro>("Assets/Scriptables/Story", obj =>
      {
        obj.key = obj.name;
      });
    }

    [FoldoutGroup("Item Creations")]
    [Button(ButtonSizes.Large)]
    [PropertySpace(spaceBefore:10, spaceAfter:10)]
    void CreateItem()
    {
      ScriptableObjectCreator.ShowDialog<ItemBase>("Assets/Scriptables/Items", obj =>
      {
        obj.key = obj.name;
      }); 
    }

    [FoldoutGroup("Debug")]
    [Button(ButtonSizes.Large)]
    [PropertySpace(spaceBefore:10, spaceAfter:10)]
    [DisableInEditorMode]
    void AddItem(string key)
    {
      var slot = Player.Instance.FindEmptySlot();
      Player.Instance.AddItem(key,slot);
    }
    
    [FoldoutGroup("Debug")]
    [Button(ButtonSizes.Large)]
    [PropertySpace(spaceBefore:10, spaceAfter:10)]
    [DisableInEditorMode]
    void RemoveItem(string key)
    {
      Player.Instance.RemoveItem(key);
    }
  }
}
