using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling_Two : Weapon_Tower_AI
{
    private GameDevHQ.FileBase.Dual_Gatling_Gun.Dual_Gatling_Gun _turret;
    private void Start()
    {
        _turret = GetComponent<GameDevHQ.FileBase.Dual_Gatling_Gun.Dual_Gatling_Gun>();
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
