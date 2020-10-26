using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Card")]
public class shopItems : ScriptableObject
{

    public new string name;
    public string description;

    public Sprite spriteAsset;

    public int cost;

}
