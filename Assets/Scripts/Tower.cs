using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : GameCharacter {
    [SerializeField] private float repairDelay;
    protected bool IsBroken { get; private set; }

    private void Repair()
    {
        IsBroken = false;
    }
    protected override void OnDeath()
    {
        Invoke("Repair", repairDelay);
    }
}
