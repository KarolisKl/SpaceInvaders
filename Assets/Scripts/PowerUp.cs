using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/PowerUp")]
public class PowerUp : Item
{
    // not enough time to finish :(
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use()
    {
        //Do the powerup
        base.Use(); // returns the actual boolean
    }
}
