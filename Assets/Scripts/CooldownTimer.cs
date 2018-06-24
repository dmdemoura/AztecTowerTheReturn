using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownTimer
{
    private float cooldownStartTime;
    public float CooldownDuration { get; set; }
    public bool Available { get; private set; }
    public CooldownTimer(float cooldownDuration)
    {
        CooldownDuration = cooldownDuration;
        Available = true;
    }
    public void Update()
    {
        if (!Available)
        {
            float elapsedTime = Time.realtimeSinceStartup - cooldownStartTime;
            if (elapsedTime >= CooldownDuration)
            {
                Available = true;
                cooldownStartTime = Time.realtimeSinceStartup;
            }
        }
    }
    public void Activate()
    {
        cooldownStartTime = Time.realtimeSinceStartup;
        Available = false;
    }
}
