using System;
using Hollow.Objects;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Hollow.UI
{
  public class MainSceneController : MonoBehaviour
  {


    [SerializeField]
    private Image background;
    
    [SerializeField]
    private Image bookSprite;
    
    [SerializeField]
    private Button continueButton;
    
    [SerializeField]
    private TextMeshProUGUI mainText;
    
    private NodeInterpreter interpreter;

    private void Start()
    {
      interpreter = GameObject.Find("GameManager").GetComponent<NodeInterpreter>(); 
      continueButton.onClick.AddListener(OnContinueButtonPressed);
    //  Refresh();
    }



    /// <summary>
    /// Handles the event when the continue button is pressed.
    /// </summary>
    private void OnContinueButtonPressed()
    {
      interpreter.Next();
      // TODO : maybe refactor this and keep it inside the interpreter
      if (interpreter.Type() == NodeType.Action)
      {
        interpreter.ExecuteActions();
      }
      Refresh();
    }


    private void Refresh()
    {
      RefreshMainText();
    }

    private void RefreshMainText()
    {
      mainText.text = interpreter.Text();
    }
  }
}
