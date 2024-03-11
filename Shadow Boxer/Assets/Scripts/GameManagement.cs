using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Player_State
{
    ALIVE, DYING, DEAD, CREDITS, CUTSCENE
}

public class GameManagement : MonoBehaviour
{
    //object vars
    private CanvasGroup game_over_canvas_group;

    //player vars
    public static int player_health = 5;
    public static Player_State cur_player_state = Player_State.ALIVE;
    private float death_time = 1f;

    //temp vars
    private static float count = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //set current player state to alive
        cur_player_state = Player_State.ALIVE;

        //make mouse cursor invisible
        Cursor.visible = false;

        //make timeScale = 1f
        Time.timeScale = 1f;

        game_over_canvas_group = GameObject.Find("Game Over Group Obj").GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(cur_player_state);
        // if(Input.GetKeyDown("t"))
        // {
        //     Take_Damage();
        // }

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

        GameObject.Find("Health Obj").GetComponent<HealthFX>().PlayDamageFX();

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

    public void Respawn()
    {
        //load current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
