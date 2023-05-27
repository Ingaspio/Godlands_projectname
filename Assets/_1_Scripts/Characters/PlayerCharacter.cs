using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PlayerCharacter : MonoCache  
{
    public static PlayerCharacter instance;
    public CharacterStatsSO player;
    public List<Vector3Int> path;
    Vector3Int locatedCell;
    Vector3 tileAnchor =  new Vector3(0.5f, 0.5f, 0);

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
    //public void Attack(EnemyScript enemy) 
    //{
        
    //    if (Input.GetMouseButtonDown(1)) 
    //    {
    //        Debug.Log("You deal " + player.damage + " damage");
            
    //    }
    //}
    public override void OnTick()
    {
        EnemyScript enemy = FindObjectOfType<EnemyScript>();
        //Attack(enemy);
    }
    public Vector3Int GetVector3IntPlayerPosition() 
    {
        return new Vector3Int(Mathf.FloorToInt(instance.transform.position.x),
            Mathf.FloorToInt(instance.transform.position.y), 0);
    }
    public IEnumerator followThePath(float timeStep = 0.2f) 
    { 
        while (true) 
        {
            if (path.Count > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, (MapManager.mainMap.CellToWorld(path[0])
                    + tileAnchor), 0.05f);
                yield return new WaitForSeconds(timeStep);
                if(transform.position == MapManager.mainMap.CellToWorld(path[0]) + tileAnchor) 
                {
                    locatedCell = path[0];
                    path.RemoveAt(0);
                    if (path.Count == 0) break;
                }
            }
            else break;
        }
        
    }
}
