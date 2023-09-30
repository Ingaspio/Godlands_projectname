using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatsSystem;

public class StatsContainer
{
    
    // ATR = attributes
    public CharacterStat ATR_Strength;
    public CharacterStat ATR_Agility;
    public CharacterStat ATR_Constitution;
    public CharacterStat ATR_Intellegence;

    // SKL = skills

    //Combat category
    public CharacterStat SKL_Attack;
    public CharacterStat SKL_Defence;
    public CharacterStat SKL_Initiative;
    public CharacterStat SKL_Range;
    
    //Survival category
    public CharacterStat SKL_Herbalism;
    public CharacterStat SKL_Harvesting;
    public CharacterStat SKL_Tracking;
    public CharacterStat SKL_Conditioning;

    //Crafting category
    public CharacterStat SKL_Smithing;
    public CharacterStat SKL_Alchemy;
    public CharacterStat SKL_Needlework;
    public CharacterStat SKL_Cooking;

    //Magic category
    public CharacterStat SKL_Reserve;
    public CharacterStat SKL_Meditation;
    public CharacterStat SKL_Power;
    public CharacterStat SKL_Control;

     //MAIN = main
    public CharacterStat MAIN_Health;
    public CharacterStat MAIN_VitalityPoint;
    public CharacterStat MAIN_Accuracy;
    public CharacterStat MAIN_Damage;
    public CharacterStat MAIN_Evasion;
    public CharacterStat MAIN_Magic;
    public CharacterStat MAIN_MagicRegen;
    public CharacterStat MAIN_Threat;
    public CharacterStat MAIN_Armor;
    public CharacterStat MAIN_PhysRes;
    public CharacterStat MAIN_FireRes;
    public CharacterStat MAIN_ColdRes;
    public CharacterStat MAIN_PoisonRes;

    
}
