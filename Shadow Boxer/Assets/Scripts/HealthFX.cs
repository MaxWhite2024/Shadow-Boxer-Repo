using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthFX : MonoBehaviour
{
    //component vars
    private CanvasGroup damage_HUD_group;
    private Image healthbar_component;

    //FX vars
    [SerializeField] private Sprite[] healthbarSprites = new Sprite[6];
    private bool taking_damage = false;
    [SerializeField] private float fade_in_time = 0.2f;
    [SerializeField] private float fade_out_time = 1f;
    private float count = 0f;

    // Start is called before the first frame update
    void Start()
    {
        damage_HUD_group = transform.GetChild(0).GetComponent<CanvasGroup>();
        healthbar_component = transform.GetChild(1).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManagement.cur_player_state == Player_State.ALIVE)
        {
            if(taking_damage)
            {
                count += Time.deltaTime;

                if(count < fade_in_time)
                {
                    damage_HUD_group.alpha += Time.deltaTime * 2f;
                }
                else if(count >= fade_in_time && count < fade_out_time)
                {
                    damage_HUD_group.alpha -= Time.deltaTime;
                }
                else
                {
                    taking_damage = false;
         
                    count = 0f;

                    damage_HUD_group.alpha = 0f;
                }
            }
        }
    }

    public void PlayDamageFX()
    {
        taking_damage = true;

        count = 0f;

        damage_HUD_group.alpha = 0f;
    }
}
