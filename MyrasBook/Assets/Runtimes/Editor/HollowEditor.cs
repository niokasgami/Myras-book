using System.Linq;
using Hollow.ScriptableObjects;
using Sirenix.OdinInspector.Demos.RPGEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Hollow.Editor
{
    public class HollowEditor : OdinMenuEditorWindow
    {
        
        [MenuItem("Tools/Hollow/Open hollow Editor")]
        private static void OpenWindow()
        {
            var window = GetWindow<HollowEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
            window.titleContent.image = EditorIcons.Pen.Active;
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree
            {
                DefaultMenuStyle =
                {
                    IconSize = 28.00f,
                },
                Config =
                {
                    DrawSearchToolbar = true,
                },
            };

            NodeOverview.Instance.UpdateNodeOverview();
            ItemOverview.Instance.UpdateNodeOverview();
            tree.Add("Story nodes", new NodeTable(NodeOverview.Instance.allNodeRessources),EditorIcons.Clouds);
            tree.Add("Items", new ItemTable(ItemOverview.Instance.allItems),EditorIcons.Folder);
            tree.Add("GameVariables", AssetDatabase.LoadAssetAtPath<VariableOverview>("Assets/Scriptables/GameVariables.asset"), EditorIcons.List);
            tree.AddAllAssetsAtPath("Story nodes", "Assets/scriptables/story", typeof(NodeBase), true, true);
            tree.AddAllAssetsAtPath("Items", "Assets/scriptables/items", typeof(ItemBase), true, true);
            tree.EnumerateTree().AddIcons<NodeBase>(AssignIcons);
            tree.EnumerateTree().AddIcons<ItemBase>(AssignItemIcons);
            return tree;
        }
        
        Texture AssignIcons(NodeBase node)
        {
            return node.type switch
            {
                NodeType.Linear => EditorIcons.Pen.Active,
                NodeType.Action => EditorIcons.Clock.Active,
                NodeType.Choice => EditorIcons.List.Active,
                _ => EditorIcons.Clouds.Active
            };
        }

        Texture AssignItemIcons(ItemBase itemBase)
        {
            return EditorIcons.Clouds.Active;
        }
        
        
        protected override void OnBeginDrawEditors()
        {
            var selected = this.MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;
  
            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                var cached = this.MenuTree.Selection.SelectedValue;
                if (selected != null)
                {
                    GUILayout.Label(selected.Name);
                }
                if (cached is NodeBase or NodeTable)
                {
                    if (SirenixEditorGUI.ToolbarButton(new GUIContent("create new Node",image: EditorIcons.Plus.Active)))
                    {
                        ScriptableObjectCreator.ShowDialog<NodeBase>("Assets/Scriptables/Story", obj =>
                        {
                            obj.key = obj.name;
                            base.TrySelectMenuItemWithObject(obj);
                        });
                    }

                    if (SirenixEditorGUI.ToolbarButton(new GUIContent("delete node",image:EditorIcons.Minus.Active)))
                    {
                        if (cached is not NodeTable)
                        {
                            var asset = cached as NodeBase;
                            var path = AssetDatabase.GetAssetPath(asset);
                            AssetDatabase.DeleteAsset(path);
                            AssetDatabase.SaveAssets();
                        }
                    }
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
    }
}

