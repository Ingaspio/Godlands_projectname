using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseCharacter_", menuName = "SO/BaseCharacterData")]
public class BaseCharacterSO : ScriptableObject
{
    public static int Strength = 5, Agility = 5, Vitality = 5, Perception = 5,  Intellegence = 5, Appearence = 5, Bravery = 5;
    [HideInInspector]
    public int Health;
    public bool playable = false;
    public bool enemy = false;
    
}  