using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher_One : Weapon_Tower_AI
{
    private GameDevHQ.FileBase.Missile_Launcher.Missile_Launcher _turret;
    private void Start()
    {
        _turret = GetComponent<GameDevHQ.FileBase.Missile_Launcher.Missile_Launcher>();
    }
    public override void StartShooting()
    {
        base.StartShooting();
        _turret.ShootEnemy();
    }

    public override void StopShooting()
    {
        base.StopShooting();
        _turret.StopShooting();
    }
}
