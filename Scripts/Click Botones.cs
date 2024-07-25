using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image img;
    [SerializeField] private Sprite defaultSprite, pressedSprite;

    public void OnPointerDown(PointerEventData eventData)
    {
        img.sprite = pressedSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        img.sprite = defaultSprite;
    }

    public void IwasClicked(){
        Debug.Log("Cliqueo");
    }
}
