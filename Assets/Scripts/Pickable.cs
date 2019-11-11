using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public Item item;
    float speed = 10f;
    // Start is called before the first frame update

    private void Start()
    {
        this.GetComponent<Collider>().isTrigger = true;
        this.tag = "Item";
        this.gameObject.AddComponent<Rigidbody>().useGravity = false;
        this.gameObject.layer = 8;
    }

    public void SetItem(Item item)
    {
        this.item = item;
        GetComponent<Renderer>().material.mainTexture = textureFromSprite(item.icon);
    }

    public void PickUp()
    {
        item.PickUp();
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 translation = Vector3.back * speed * Time.deltaTime;
        transform.Translate(translation);
    }

    // function for converting sprite to texture
    static Texture2D textureFromSprite(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if collided with player
        if (other.gameObject.layer == 9)
        {
            PickUp();
        }
    }
}
