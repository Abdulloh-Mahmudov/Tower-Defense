using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher_Two : Weapon_Tower_AI
{
    private GameDevHQ.FileBase.Missle_Launcher_Dual_Turret.Missle_Launcher _turret;
    private void Start()
    {
        _turret = GetComponent<GameDevHQ.FileBase.Missle_Launcher_Dual_Turret.Missle_Launcher>();
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
