using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch_FX : MonoBehaviour
{
    private RectTransform rect_trans;
    private CanvasGroup canvas_group;
    [SerializeField] private float fade_duration = 0.5f;
    [SerializeField] private float horizontal_move = 140f;
    private float end_x;

    void Awake()
    {
        rect_trans = GetComponent<RectTransform>();
        canvas_group = GetComponent<CanvasGroup>();
        canvas_group.alpha = 1f;
        end_x = rect_trans.anchoredPosition.x + horizontal_move;
    }

    // Update is called once per frame
    void Update()
    {
        //fade from fully opaque to fully transparent
        canvas_group.alpha = Mathf.MoveTowards(canvas_group.alpha, 0f, Time.deltaTime / fade_duration);

        //move by horizontal_move
        rect_trans.anchoredPosition = new Vector2(Mathf.MoveTowards(rect_trans.anchoredPosition.x, end_x, Time.deltaTime / fade_duration), 0f);
    }
}
