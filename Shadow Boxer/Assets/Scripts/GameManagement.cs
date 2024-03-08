using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player_State
{
    ALIVE, DEAD, CREDITS, CUTSCENE
}

public class GameManagement : MonoBehaviour
{
    //object vars
    private GameObject this_obj; 
    private CanvasGroup game_over_canvas_group;

    //player vars
    public static int player_health = 3;
    public static Player_State cur_player_state = Player_State.ALIVE;
    private static bool dying = false;
    private float death_time = 1f;

    //game state vars
    private int cur_level = 1;

    // Start is called before the first frame update
    void Start()
    {
        this_obj = gameObject;
        game_over_canvas_group = GameObject.Find("Game Over Group Obj").GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dying)
        {
            game_over_canvas_group.alpha -= death_time / Time.deltaTime;
        }
    }

    public static void Take_Damage()
    {
        player_health -= 1;

        if(player_health <= 0)
        {
            Die();
        }
    }

    private static void Die()
    {
        dying = true;
    }

    public static void Set_Player_State(Player_State state)
    {
        cur_player_state = state;
    }
}
