using Hollow.Management;
using Hollow.Objects;
using Hollow.ScriptableObjects.Nodes;
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
    private GameObject continueButtonObject;

    [SerializeField]
    private GameObject[] choiceButtons;

    [SerializeField]
    private TextMeshProUGUI[] choicesText;
    [SerializeField]
    private GameObject choicesContainer;

    [SerializeField]
    private TextMeshProUGUI mainText;
    
    [SerializeField]
    private TextMeshProUGUI locationText;
    
    [SerializeField]
    private GameObject bookmarkedPageSprite;
    private NodeInterpreter interpreter;
    


    // cleanup this function?
    private void Start()
    {
      interpreter = GameObject.Find("GameManager").GetComponent<NodeInterpreter>();
   //   choicesContainer = GameObject.Find("Choice_Container");
   //   continueButtonObject = GameObject.Find("Continue_Button");
      Database.OnDataLoaded += OnStartup;
      GameManager.Instance.OnGuiRefresh += Refresh;
      continueButton.onClick.AddListener(OnContinueButtonPressed);
      //   AttachChoiceButtonCallBack();
    }
    

    /// <summary>
    /// Handles the event when the continue button is pressed.
    /// </summary>
    private void OnContinueButtonPressed()
    {
      interpreter.Next();
      Refresh();
    }

    public void OnChoiceButtonPressed(int index)
    {
      if (interpreter.IsConditionsFulfilled(index))
      {
        interpreter.Next(index);
        Refresh();
      }
      else
      {
        Debug.Log("You seem to be lacking the idea!");
      }
    }

    private void Refresh()
    {
      RefreshMainText();
      RefreshButtons();
      RefreshLocationText();
      RefreshBookmarkedPage();
    }

    private void RefreshMainText()
    {
      mainText.text = interpreter.Text();
    }

    // TODO : refactor this its way to messy and I am not sure how performant it is.
    private void RefreshButtons()
    {
      if (!interpreter.IsChoiceNode())
      {
        choicesContainer.SetActive(false);
        // in this case we are deactivating all of them and we enabling them on the spot
        foreach (var button in choiceButtons)
        {
          button.SetActive(false);
        }
        continueButtonObject.SetActive(true);
        return;
      }
      choicesContainer.SetActive(true);
      continueButtonObject.SetActive(false);
      // here we rolling throught the conditions from the interpreter instead of the choice buttons size
      
      for (var i = 0; i < interpreter.ChoiceSize(); i++)
      {
        // here we set it as active so we can show
        var currentNode = (NodeChoice)interpreter.currentNode;
        choiceButtons[i].SetActive(true);
        choicesText[i].text = interpreter.IsConditionsFulfilled(i) ? currentNode.list[i].text : "??????";
      }
    }

    private void RefreshLocationText()
    {
      locationText.text = interpreter.Location();
    }

    // TODO : move this to its own class if I want to do some buttons 
    private void RefreshBookmarkedPage()
    {
      var bookmarks = Player.Instance.GetAllBookmarks();
      var currentNode = interpreter.currentNode.key;
      if (bookmarks.Contains(currentNode))
      {
        bookmarkedPageSprite.SetActive(true);
      }
      else
      {
        bookmarkedPageSprite.SetActive(false);
      }
    }

    private void OnStartup()
    {
      Refresh();
      Database.OnDataLoaded -= OnStartup;
    }
  }
}
