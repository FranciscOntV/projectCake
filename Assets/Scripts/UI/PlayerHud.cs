using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHud : MonoBehaviour
{
    public Camera cam;
    public TMP_Text hpIndicator;
    public StatusManager stats;

    void Update()
    {
        hpIndicator.text = stats.currentHP.ToString() + "/" + stats.maxHP.ToString();
        this.transform.position = RectTransformUtility.WorldToScreenPoint(cam, stats.gameObject.transform.position + Vector3.down);
    }
}
