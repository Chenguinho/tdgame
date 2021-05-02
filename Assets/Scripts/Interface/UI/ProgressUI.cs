using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressUI : MonoBehaviour
{

    public TextMeshProUGUI progressText;
    public TextMeshProUGUI username;

    private void Start()
    {
        username.text = Player.username;
    }

}
