using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsUI : MonoBehaviour
{

    public TextMeshProUGUI points;

    void Update()
    {
        points.text = CurrentGame.Points.ToString();
    }
}
