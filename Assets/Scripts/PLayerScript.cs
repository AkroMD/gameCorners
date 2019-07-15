using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerScript : MonoBehaviour
{
    public readonly List<PawnScript> pawns = new List<PawnScript>();
    public string playerName;
    public GameObject pawnKind;
    public bool cornerTop;

    int[,] scoreBoard =
{
        {256,160,100,30,25,20,12,7},
        {160,128,80,40,30,15,10,6},
        {100,80,64,50,25,18,12,5},
        {30,40,50,32,20,14,8,4},
        {25,30,25,20,16,12,5,3},
        {20,15,18,14,12,8,4,2},
        {12,10,12,8,5,4,4,1},
        {7,6,5,4,3,2,1,0},
    };

    public bool CheckTruePosition(Vector3 position)
    {
        return (cornerTop ?
            (position.x >= 5 && position.y < 3) :
            (position.x < 3 && position.y >= 5));
    }

    public bool CheckTruePositionPawn(PawnScript pawn)
    {
        return CheckTruePosition(pawn.transform.position);
    }

    public PawnScript GetFreePawn()
    {
        foreach(PawnScript go in pawns)
        {
            if (!CheckTruePositionPawn(go)) return go;
        }
        return null;
    }

    public float GetScorePawn(PawnScript go)
    {
        return GetScorePosition(go.transform.position);
    }

    public int GetScorePosition(Vector2 position)
    {
        int x = (int)position.x, y = 7 - (int)position.y;
        if (cornerTop)
        {
            x = 7 - x;
            y = 7 - y;
        }
        return scoreBoard[x, y];
    }

    public float GetScore()
    {
        float score = 0;
        foreach(PawnScript go in pawns)
        {
            score += GetScorePawn(go);
        }
        return score;
    }

    public bool CheckEnd()
    {
        foreach(PawnScript go in pawns)
        {
            if (!CheckTruePositionPawn(go)) return false;
        }
        return true;
    }

    public PawnScript GetPawnOnPosition(Vector3 position)
    {
        foreach(PawnScript pa in pawns)
        {
            if (pa.transform.position.Equals(position))
            {
                return pa;
            }
        }
        return null;
    }

    public bool AssignedPawn(PawnScript mypawn)
    {
        foreach(PawnScript go in pawns)
        {
            if (go.Equals(mypawn))
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator CreatePawns()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject newPawn = Instantiate(pawnKind);
            Vector3 position;
            if (cornerTop)
            {
                position = new Vector3(i % 3, 5 + i / 3, 0);
            }
            else position = new Vector3(5f + i % 3, i / 3, 0);
            newPawn.transform.position = position;
            pawns.Add(newPawn.GetComponent<PawnScript>());
            yield return new WaitForSeconds(0.1f);
        }        
    }

    public void NewGame()
    {
        foreach(PawnScript go in pawns)
        {
            Destroy(go.gameObject);
        }
        pawns.Clear();
        StartCoroutine("CreatePawns");
    }
}
