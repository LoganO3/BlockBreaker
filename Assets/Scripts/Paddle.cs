using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    internal object transform;
    [SerializeField] float ScreenWidthUnits = 16f;
    [SerializeField] float xMin = 1f;
    [SerializeField] float xMax = 15f;

    GameStatus thegamestatus;
    Ball theball;

    // Start is called before the first frame update
    void Start()
    {
        thegamestatus = FindObjectOfType<GameStatus>();
        theball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), xMin, xMax);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (thegamestatus.IsAutoPlayEnabled())
        {
            return theball.transform.position.x;
        }
        else
        {
            return (Input.mousePosition.x / Screen.width * ScreenWidthUnits);
        }
    }
}
