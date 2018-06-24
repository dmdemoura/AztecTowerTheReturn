using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Heartbar : MonoBehaviour
{
    [SerializeField] private Player gameCharacter;
    [SerializeField] private Text innerBar;

    private void Update()
    {
        innerBar.text = "Hearts: " + gameCharacter.hearts;
    }
}
