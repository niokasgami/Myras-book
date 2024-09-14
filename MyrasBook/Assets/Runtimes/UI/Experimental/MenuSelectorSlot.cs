using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hollow.UI.Experimental
{
  public class MenuSelectorSlot : MonoBehaviour, IPointerClickHandler
  {

    [SerializeField]
    private Color focusColor;
    [SerializeField]
    private Color unfocusColor;
    [SerializeField]
    private int index;

    private Image image;

    private void Awake()
    {
      image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      image.color = focusColor;
    }
  }
}
