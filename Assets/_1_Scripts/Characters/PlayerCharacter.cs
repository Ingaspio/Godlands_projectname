using UnityEngine;
using UnityEngine.Tilemaps;
public class PlayerCharacter : MonoCache
{
    public static PlayerCharacter instance;
    public CharacterStatsSO player;

    public void Start() 
    {
        if (instance != null)
        { Destroy(gameObject); }
        else
        { instance = this; }
        //player.OnCharacterStateChanged += Player_OnCharacterStateChanged;
        DontDestroyOnLoad(gameObject);

    }

    //public void OnDestroy()
    //{
    //    player.OnCharacterStateChanged -= Player_OnCharacterStateChanged;
    //}
    //private void Player_OnCharacterStateChanged(CharacterStatsSO.CharacterState state)
    //{
    //    player.playable = true;
    //}
    public void Attack(EnemyScript enemy) 
    {
        
        if (Input.GetMouseButtonDown(1)) 
        {
            Debug.Log("You deal " + player.damage + " damage");
            
        }
    }
    public override void OnTick()
    {
        EnemyScript enemy = FindObjectOfType<EnemyScript>();
        Attack(enemy);
    }
}
