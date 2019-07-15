using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum MoveDirection {Left, Top, Right, Bottom};

public class PawnScript : MonoBehaviour
{

    static class HistoryPawn
    {
        static List<string> historyList = new List<string>();
        static HistoryPawn()
        {
            historyList.Add("Слон Валера, не понимает, что вокруг происходит");
            historyList.Add("Слон Алексей, знает толк в мышах");
            historyList.Add("Сааамый лучший ОН, и вообще он слОН");
            historyList.Add("За Орду!");
            historyList.Add("Опять работа...(");
            historyList.Add("Будет сделано");
        }

        public static string GetHistory(int i)
        {
            if (i < 0 || i >= historyList.Count)
                return historyList[0];
            return historyList[i];
        }

        public static int GetRandomHistory()
        {
            return Random.Range(0, historyList.Count);
        }
    }

    int history;
    SpriteRenderer sp;

    public void Move(MoveDirection direction)
    {
        Vector2 newPosition = transform.position;
        switch (direction)
        {
            case MoveDirection.Left:
                newPosition += Vector2.left;
                break;
            case MoveDirection.Top:
                newPosition += Vector2.up;
                break;
            case MoveDirection.Right:
                newPosition += Vector2.right;
                break;
            case MoveDirection.Bottom:
                newPosition += Vector2.down;
                break;
        }
        transform.position = newPosition;
    }

    private void Start()
    {
        history = HistoryPawn.GetRandomHistory();
        sp = GetComponent<SpriteRenderer>();
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0);
        transform.localScale = new Vector3(0.6f, 0.6f, 1);
        StartCoroutine("Appearance");
    }

    IEnumerator Appearance()
    {
        while (sp.color.a < 1)
        {
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a + 0.03f);
            if (transform.localScale.x > 0.4)
            transform.localScale = new Vector3(transform.localScale.x - 0.02f, transform.localScale.y - 0.02f);
            yield return new WaitForSeconds(0.02f);
        }
        
    }

    public string GetInformation()
    {
        return "Позиция: x: " +
                (transform.position.x - 0.5f).ToString() + ", y: " +
                (transform.position.y - 0.5f).ToString() + "\n" +
                HistoryPawn.GetHistory(history);
    }

}
