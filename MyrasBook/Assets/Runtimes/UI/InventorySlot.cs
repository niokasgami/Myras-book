using Hollow.Management;
using Hollow.Objects;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Hollow.UI
{
  public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
  {
    [PropertyRange(0, 7)]
    public int slot = 0;

    [SerializeField]
    private TextMeshProUGUI label;
    [FormerlySerializedAs("sprite")]
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI description;

    private Inventory inventory;

    private void Start()
    {
      Database.OnDataLoaded += OnStartup;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      description.text =  inventory.Description(slot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      description.text = "";
    }

    void Refresh()
    {
      image.color = inventory.IsSlotAvailable(slot) ? new Color(1, 1, 1, 0) : new Color(1, 1, 1, 1);
      image.sprite = inventory.Sprite(slot);
      label.text = inventory.DisplayName(slot);
    }

    void OnStartup()
    {
      inventory = Player.Instance.Inventory();
      inventory.OnInventoryChanged += Refresh;
      Database.OnDataLoaded -= OnStartup;
      Refresh();
    }
  }
}
