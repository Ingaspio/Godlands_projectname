using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats_", menuName = "SO/CharacterSO")]
public class CharacterStatsSO : ScriptableObject
{
    public string characterName;
    public string description;
    
    public int visionRadius;
    public int maxAP;   
    public int maxHealth;    
    public int damage;
    public int heal;
    public bool death = false;
    public bool playable = false;    
}


//public enum CharacterState { Playable, Idle, Patroling, Aware, Combat }

//CharacterState characterBehaviorState;

//public event Action<CharacterState> OnCharacterStateChanged;

//public void UpdateGameState(CharacterState newState) 
//{ 
//    characterBehaviorState = newState;
//    switch (newState)
//    {
//        case CharacterState.Playable:
//            break;
//        case CharacterState.Idle:
//            break;
//        case CharacterState.Patroling:
//            break;
//        case CharacterState.Aware:
//            break;
//        case CharacterState.Combat:
//            break;
//        default:
//            newState = CharacterState.Idle;
//            break;
//    }
//    OnCharacterStateChanged?.Invoke(newState);
//}