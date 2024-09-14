using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Hollow.ScriptableObjects
{
  /**
   * The abstract class that allow most data to share the same parents.
   * Its created for conveniences sake
   */
  public abstract class ObjectBase : ScriptableObject
  {
    [Title("Metadata")]
    [BoxGroup("Metadata", showLabel: false)]
    [InlineButton("OnNameChange","rename")]
    [LabelWidth(70)]
    public string key;


    private Guid _guid;
    public Guid Guid => _guid;
    #if UNITY_EDITOR
    private void OnNameChange()
    {
      
      var path = AssetDatabase.GetAssetPath(this);
      AssetDatabase.RenameAsset(path, key);
    }
    
    private void OnValidate()
    {
      var path = AssetDatabase.GetAssetPath(this);
      _guid = new Guid(AssetDatabase.AssetPathToGUID(path));
    }
    #endif
  }
}