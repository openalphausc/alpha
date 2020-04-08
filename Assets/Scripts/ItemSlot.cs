using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehavior
{
    [SerializeField] Image Image;
    private Item _item;
    public Item Item {
        get {
            return _item;
        }
        set {
            _item = value;
            if( _item = null){
                Image.enabled = false;
            } else{
                Image.sprite = _item.Icon;
                Image.enabled = true;
            }
        }
    }


}