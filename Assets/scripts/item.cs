using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    // Visual about item
    public Sprite sprite;

    // Position within the array
    public int position;

    // Info about the item. There are dupes because there are 2 parents
    public int resistance; // Guard against events (may break this up into pest res/environment res)
    public int resistance2;
    public int rate; // Speed of growth
    public int rate2;
    public int quantity; // Amount of harvest
    public int quantity2;

}
