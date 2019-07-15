using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationSelectScript : MonoBehaviour
{
    public float speedScale = 0.01f;
    public GameObject informationPanel;
    public GameObject leftS, topS, rightS, bottomS;

    public void Select(PawnScript pawn, bool left, bool top, bool right, bool bottom)
    {
        if (pawn != null)
        {
            leftS.SetActive(left);
            topS.SetActive(top);
            rightS.SetActive(right);
            bottomS.SetActive(bottom);
            transform.position = pawn.transform.position;
            informationPanel.SetActive(true);
            informationPanel.transform.Find("InformationText").GetComponent<Text>().text = pawn.GetInformation();
        }
        gameObject.SetActive(pawn != null);
        informationPanel.SetActive(pawn != null);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newScale = new Vector3(
            transform.localScale.x - speedScale,
            transform.localScale.y - speedScale,
            transform.localScale.z);
        transform.localScale = newScale;
        if (transform.localScale.x < 0.85)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
