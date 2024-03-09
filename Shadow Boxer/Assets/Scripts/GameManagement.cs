using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player_State
{
    ALIVE, DYING, DEAD, CREDITS, CUTSCENE
}

public class GameManagement : MonoBehaviour
{
    //object vars
    private GameObject this_obj; 
    private CanvasGroup game_over_canvas_group;

    //player vars
    public static int player_health = 3;
    public static Player_State cur_player_state = Player_State.ALIVE;
    private float death_time = 1f;

    //game state vars
    private int cur_level = 1;

    //temp vars
    private static float count = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //make mouse cursor invisible
        Cursor.visible = false;

        //make timeScale = 1f

        this_obj = gameObject;
        game_over_canvas_group = GameObject.Find("Game Over Group Obj").GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cur_player_state == Player_State.DYING)
        {
            count += Time.deltaTime * 2f;

            if(count <= death_time)
            {
                game_over_canvas_group.alpha += Time.deltaTime * 2f;
            }
            else 
            {
                game_over_canvas_group.interactable = true;
                game_over_canvas_group.blocksRaycasts = true;

                //stop time
                Time.timeScale = 0f;
                
                //make mouse cursor visible
                Cursor.visible = true;

                //set cur state to DEAD
                cur_player_state = Player_State.DEAD;
            }
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
        cur_player_state = Player_State.DYING;
        Time.timeScale = 0.5f;
        count = 0f;
    }

    public static void Set_Player_State(Player_State state)
    {
        cur_player_state = state;
    }
}
