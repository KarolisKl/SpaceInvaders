using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    GameObject image;
    public Item item;
    public int key;


    void Start()
    {
        image = transform.GetChild(0).gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("slot" + key))
        {
            UseItem();
        }
    }

    void UseItem()
    {
        if (item != null)
        {
            item.Use();
            // clear the slot
            item = null;
            image.GetComponent<Image>().sprite = null;
            image.SetActive(false);
        }
    }

    public void AddItem(Item item)
    {
        this.item = item;
        // update the slot
        image.GetComponent<Image>().sprite = item.icon;
        image.SetActive(true);
    }

}
