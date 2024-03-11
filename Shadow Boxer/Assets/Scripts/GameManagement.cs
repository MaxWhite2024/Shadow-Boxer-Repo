using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Player_State
{
    ALIVE, DYING, DEAD, CREDITS, CUTSCENE
}

[System.Serializable]
public class Encounter
{
    public GameObject[] enemies;
    public float encounterTime = 1f;
}

public class GameManagement : MonoBehaviour
{
    //object vars
    private CanvasGroup game_over_canvas_group;

    //player vars
    public static int player_health = 5;
    public static Player_State cur_player_state = Player_State.ALIVE;
    private float death_time = 1f;

    //level sequence vars
    [SerializeField] private Encounter[] levelEncounterSequence;
    [SerializeField] private float timeBetweenSpawns = 0.1f;

    //temp vars
    private static float count = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //reset player health
        player_health = 5;

        //set current player state to alive
        cur_player_state = Player_State.ALIVE;

        //make mouse cursor invisible
        Cursor.visible = false;

        //make timeScale = 1f
        Time.timeScale = 1f;

        //open curtains
        GameObject.Find("Level FX Obj").GetComponent<LevelFX>().OpenCurtains();

        game_over_canvas_group = GameObject.Find("Game Over Group Obj").GetComponent<CanvasGroup>();

        //begin level sequence
        StartCoroutine(LevelSequence());
    }

    IEnumerator LevelSequence()
    {
        //as long as player is alive,
        if(cur_player_state == Player_State.ALIVE)
        {
            //for each encounter in levelEncounterSequence,
            for(int i = 0; i < levelEncounterSequence.Length; i++)
            {
                //for each enemy in encounter,
                for(int j = 0; j < levelEncounterSequence[i].enemies.Length; j++)
                {
                    //activate the enemy
                    levelEncounterSequence[i].enemies[j].SetActive(true);

                    //wait for timeBetweenSpawns
                    yield return new WaitForSeconds(timeBetweenSpawns);
                }

                //wait for encounter to finish
                yield return new WaitForSeconds(levelEncounterSequence[i].encounterTime);
            }
            
            //go to next level
            //GoToNextLevel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(cur_player_state);
        // if(Input.GetKeyDown("t"))
        // {
        //     //Take_Damage();
        //     //GoToNextLevel();
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

    public void GoToNextLevel()
    {
        //close curtains
        GameObject.Find("Level FX Obj").GetComponent<LevelFX>().CloseCurtains();

        //load next level if there is one
        int next_scene_index = SceneManager.GetActiveScene().buildIndex + 1;
        if(next_scene_index <= SceneManager.sceneCount)
        {
            SceneManager.LoadScene(next_scene_index);
        }
    }
}
