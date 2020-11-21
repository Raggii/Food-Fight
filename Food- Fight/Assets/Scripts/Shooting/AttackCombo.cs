using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AttackCombo", menuName = "AttackCombo")]
public class AttackCombo : ScriptableObject
{
    [Header("Combo Name")]
    public new string name;


    [Header("Combo Post Delay")]
    public float delay;


    [Header("Attacks")]
    public List<AttackAction> attacks = new List<AttackAction>();


}
