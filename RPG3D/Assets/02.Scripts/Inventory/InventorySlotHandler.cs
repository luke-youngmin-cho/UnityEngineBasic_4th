using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventorySlotHandler : MonoBehaviour, IPointerDownHandler
{
    public static InventorySlotHandler instance;
    public bool isBusy;

    private InventoryViewSlot _slot;
    [SerializeField] private Image _icon;

    // UI Raycast event
    [SerializeField] private GraphicRaycaster _graphicRaycaster;
    private EventSystem _eventSystem;
    private PointerEventData _pointerEventData;

    public void Handle(InventoryViewSlot slot)
    {
        _slot = slot;
        _icon.sprite = ItemDataAssets.Instance.ItemDataDictionary[_slot.ItemCode].Icon;
        gameObject.SetActive(true);
        isBusy = true;
    }

    public void Cancel()
    {
        _slot = null;
        _icon.sprite = null;
        gameObject.SetActive(false);
        isBusy = false;
    }

    private void Awake()
    {
        instance = this;
        _eventSystem = FindObjectOfType<EventSystem>();

        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Cancel();
        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            _pointerEventData = new PointerEventData(_eventSystem);
            _pointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            _graphicRaycaster.Raycast(_pointerEventData, results);

            bool onCanvas = false;
            // Check event occured on ui
            foreach (var result in results)
            {
                if (result.gameObject != this.gameObject &&
                    result.gameObject.TryGetComponent(out CanvasRenderer canvasRenderer))
                {
                    onCanvas = true;
                }
            }

            // User is dropping the item on battle field
            if (onCanvas == false)
            {
                if (InventoryPresenter.instance.dropCMD.TryExecute(new ItemPair(_slot.ItemCode, _slot.Num)))
                {
                    Cancel();
                }
            }
        }
    }
}
