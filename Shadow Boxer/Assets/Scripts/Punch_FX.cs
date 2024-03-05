using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Punch_FX : MonoBehaviour
{
    private RectTransform rect_trans;
    private CanvasGroup canvas_group;
    [SerializeField] private float fade_duration = 0.5f;
    [SerializeField] private float move_speed = 0.5f;
    [SerializeField] private float horizontal_move = 140f;
    private Vector3 end;
    [SerializeField] private Sprite[] sprites;

    void Start()
    {
        rect_trans = GetComponent<RectTransform>();
        canvas_group = GetComponent<CanvasGroup>();
        canvas_group.alpha = 1f;
        end = rect_trans.anchoredPosition;
        end.x = rect_trans.anchoredPosition.x + horizontal_move;

        //randomize sprite
        gameObject.GetComponent<Image>().sprite = sprites[Random.Range(0, 3)];

        //Destroy self after fade_duration seconds
        Destroy(gameObject, fade_duration);
    }

    // Update is called once per frame
    void Update()
    {
        //fade from fully opaque to fully transparent
        canvas_group.alpha = Mathf.MoveTowards(canvas_group.alpha, 0f, Time.deltaTime / fade_duration);

        //move by horizontal_move
        rect_trans.anchoredPosition = Vector3.MoveTowards(rect_trans.anchoredPosition, end, move_speed * Time.deltaTime);
    }
}
