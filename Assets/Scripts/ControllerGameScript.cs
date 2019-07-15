using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public static class GameParam
{
    public static Vector3 shiftPosition = new Vector3(7, 3, 0);
}

public class ControllerGameScript : MonoBehaviour
{
    public Tilemap map;
    public AnimationSelectScript selectObject;
    public Text turnText;
    public List<PLayerScript> players;
    public MenuScriptClick menuscript;
    public GameObject camera;

    PLayerScript activePlayer = null;
    PawnScript selectPawn = null;

    void OnVisionTurn()
    {
        turnText.text = "Turn:" + "\n" + activePlayer.playerName;
    }

    public void NextTurn()
    {
        int nowIndex = players.IndexOf(activePlayer);

        if (nowIndex == players.Count - 1) activePlayer = players[0];
        else activePlayer = players[nowIndex + 1];
        selectPawn = null;
        selectObject.Select(null, false, false, false, false);
        OnVisionTurn();
        if (activePlayer is AIScript) (activePlayer as AIScript).AITurn();
    }

    public void NewGame()
    {
        foreach (PLayerScript ps in players)
        {
            ps.NewGame();
        }
        activePlayer = players[Random.Range(0,2)];
        NextTurn();
    }

    void PlayerClick(Vector3 position)
    {
        if (selectPawn != null && activePlayer.AssignedPawn(selectPawn) && 
            Mathf.Abs(position.x - selectPawn.transform.position.x + 
                position.y - selectPawn.transform.position.y) == 1 &&
               notPawnInPosition(position))
        {
            MoveDirection direction;
            if (position.x - selectPawn.transform.position.x > 0) direction = MoveDirection.Right;
            else if (position.x - selectPawn.transform.position.x < 0) direction = MoveDirection.Left;
            else if (position.y - selectPawn.transform.position.y > 0) direction = MoveDirection.Top;
            else direction = MoveDirection.Bottom;
            MovePawn(selectPawn, direction);
        }            
        else  Select(position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 position = map.WorldToCell(
                        Camera.main.ScreenToWorldPoint(Input.mousePosition));
            position += GameParam.shiftPosition;
            PlayerClick(position);
        }
    }

    bool CheckEndGame()
    {
        foreach (PLayerScript ps in players)
        {
            if (ps.CheckEnd()) return true;
        }
        return false;
    }

    void EndGame()
    {
        turnText.text = "Winner:\n" + activePlayer.playerName;
        menuscript.Show();

    }

    public void MovePawn(PawnScript pawn, MoveDirection direction)
    {
        if (pawn != null) pawn.Move(direction);
        if (CheckEndGame()) EndGame();
        else NextTurn();
    }

    public bool notPawnInPosition(Vector3 position)
    {
        foreach (PLayerScript ps in players)
        {
            if (ps.GetPawnOnPosition(position) != null) return false;
        }
        return true;
    }

    void Select(Vector3 position)
    {
        selectPawn = null;
        foreach (PLayerScript ps in players)
        {
            selectPawn = ps.GetPawnOnPosition(position);
            if (selectPawn != null)
            {
                Vector2 posPawn = selectPawn.transform.position;
                Vector2 newPos = posPawn + Vector2.left;
                bool left = notPawnInPosition(newPos) && newPos.x >= 0;
                newPos = posPawn + Vector2.right;
                bool right = notPawnInPosition(newPos) && newPos.x < 8;
                newPos = posPawn + Vector2.up;
                bool top = notPawnInPosition(newPos) && newPos.y < 8;
                newPos = posPawn + Vector2.down;
                bool bottom = notPawnInPosition(newPos) && newPos.y >= 0;
                selectObject.Select(selectPawn, left, top, right, bottom);
                return;
            }
        }
        selectObject.Select(null, false,false,false,false);
    }
}
