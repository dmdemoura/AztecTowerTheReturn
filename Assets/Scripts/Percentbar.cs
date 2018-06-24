using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Percentbar : MonoBehaviour
{
    [SerializeField] private GameCharacter gameCharacter;
    [SerializeField] private RectTransform innerBar;

    private void Update()
    {
        float percentage = gameCharacter.Health / gameCharacter.MaxHealth;
        innerBar.anchorMax = new Vector2(percentage, innerBar.anchorMax.y);
    }
}
