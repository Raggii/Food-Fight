﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enemies : ScriptableObject
{

    public new string name;
    public string description;

    public Sprite spriteAsset;
    //sprites for movement maybe

    public int maxHealth;
    public int movementSpeed;
    public int dropAmount;
    //public int ectera

}
