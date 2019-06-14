using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHud : MonoBehaviour
{
    public Camera cam;
    public TMP_Text hpIndicator;
    public StatusManager stats;
    private int[] Hp;

    // Update is called once per frame
    void Update()
    {
        Hp = stats.getHp();
        hpIndicator.text = Hp[0].ToString() + "/" + Hp[1].ToString();
        this.transform.position = RectTransformUtility.WorldToScreenPoint(cam, stats.gameObject.transform.position + Vector3.down);
    }
}
