using System;
using System.Collections.Generic;
using System.Linq;
using Hollow;
using Hollow.Editor.GraphEditor;
using Hollow.Editor.HollowStoryEditor.Nodes;
using Hollow.ScriptableObjects.Nodes;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
  public class DialogueGraph : EditorWindow
  {


    private DialogueGraphView graphView;
    private PropertyTree inspector;
    private ScrollView scrollView;
    private IMGUIContainer inspectorContainer;
    private bool isDirty = false;

    private Dictionary<Guid, NodeBase> collections = new Dictionary<Guid, NodeBase>();
    
    [MenuItem("Hollow/Graph Editor")]
    public static void OpenDialogueGraphWindow()
    {
      var window = GetWindow<DialogueGraph>();
      window.titleContent = new GUIContent("Dialogue Graph");
    }
    

    private void CreateGUI()
    {
      BuildNodeCollections();
      ConstructGraphView();
      GenerateToolbar();
      CreateInspector();
      BindInspector();
    }



    private void BuildNodeCollections()
    {
      var assets = AssetDatabase.FindAssets("t:nodeBase")
        .Select(guid => AssetDatabase.LoadAssetAtPath<NodeBase>(AssetDatabase.GUIDToAssetPath(guid)))
        .ToArray();
      foreach (var node in assets)
      {
        collections.Add(node.Guid,node);
      }
    }


    private void BindInspector()
    {
      DialogBase.OnNodeSelected += OnRefresh;
    }


    private void OnRefresh(Guid node)
    {
      OnUpdateNode(node);
    }
    
    private void CreateInspector()
    {
      var SO = GetIntroNode();
      inspector = PropertyTree.Create(SO);
      inspectorContainer = new IMGUIContainer(() =>
      {
        GUI.skin = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector);
        inspector?.Draw();
      });
      
      scrollView = new ScrollView();
      scrollView.Add(inspectorContainer);
      scrollView.style.width = new Length(35, LengthUnit.Percent);
      scrollView.style.alignSelf = Align.FlexEnd;
      scrollView.style.flexGrow = 1;
      scrollView.style.backgroundColor = new Color(0.22f, 0.22f, 0.22f, 1);
      
      rootVisualElement.Add(scrollView);
    }


    private NodeBase GetIntroNode()
    {
      return collections.Values.FirstOrDefault(node => node.type == NodeType.Intro);
    }

    private NodeBase GetNode(Guid guid)
    {
      return collections[guid];
    }
    
    private void OnUpdateNode(Guid node)
    {
      var so = GetNode(node);

      if (inspector != null)
      {
        inspector.Dispose();
      }
      
      inspector = PropertyTree.Create(so);
      inspectorContainer.onGUIHandler = () =>
      {
        GUI.skin = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector);
        inspector?.Draw();
      };
      isDirty = false;
    }
    
    private void OnGUI()
    {
      inspector.Draw();
    }

    private void ConstructGraphView()
    {
      graphView = new DialogueGraphView(collections)
      {
        name = "Story Editor"
      };
      graphView.StretchToParentSize();
      rootVisualElement.Add(graphView);
    }

    private void GenerateToolbar()
    {
      var toolbar = new Toolbar();
      var nodeCreate = new Button(() =>
      {
      //  graphView.CreateNode("Dialogue Node");
      //  OnUpdateNode();
      });
      nodeCreate.text = "Create Node";

      var toggleVisibility = new Button(() =>
      {
        scrollView.visible = !scrollView.visible;
      });
      toggleVisibility.text = "Inspector";
      
      toolbar.Add(nodeCreate);
      toolbar.Add(toggleVisibility);
      rootVisualElement.Add(toolbar);
    }

    private void OnDisable()
    {
      rootVisualElement.Remove(graphView);
      rootVisualElement.Remove(scrollView);
      collections.Clear();
      DialogBase.OnNodeSelected -= OnRefresh; 
    }
  }
}