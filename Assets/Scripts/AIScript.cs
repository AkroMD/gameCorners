using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : PLayerScript
{
    ControllerGameScript cgs;

    private void Awake()
    {
        cgs = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControllerGameScript>();
    }

    public void AITurn()
    {
        Invoke("FindBestTurn", Random.Range(0.5f, 1.5f));
    }

    void FindBestTurn()
    {
        PawnScript selectPawn = null;
        MoveDirection direction = MoveDirection.Left;
        float deltaScore = -100;
        foreach (PawnScript go in pawns)
        {
            float newScore = 0;            
            Vector2 position = go.transform.position;
            float score = GetScorePosition(position);
            Vector2 newPosition = Vector2.left;
            MoveDirection newDirection = MoveDirection.Left;
            for (var i = 0; i < 4; i++)
            {
                Vector2 pos = position + newPosition;
                if (cgs.notPawnInPosition(pos) && pos.x >= 0 && pos.y >= 0 && pos.x <= 7 && pos.y <= 7)
                {
                    newScore = GetScorePosition(pos);
                    if (newScore - score > deltaScore)
                    {
                        deltaScore = newScore - score;
                        selectPawn = go;
                        direction = newDirection;
                    }
                }
                switch (i)
                {
                    case 0:
                        newDirection = MoveDirection.Top;
                        newPosition = Vector2.up;
                        break;
                    case 1:
                        newDirection = MoveDirection.Right;
                        newPosition = Vector2.right;
                        break;
                    case 2:
                        newDirection = MoveDirection.Bottom;
                        newPosition = Vector2.down;
                        break;
                }
            }
        }
        cgs.MovePawn(selectPawn, direction);
    }
}
