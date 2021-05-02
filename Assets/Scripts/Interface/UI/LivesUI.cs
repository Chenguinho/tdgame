using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{

    public TextMeshProUGUI lives;

    void Update()
    {
        lives.text = CurrentGame.Lives.ToString();
    }

}
