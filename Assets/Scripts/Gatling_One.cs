using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling_One : Weapon_Tower_AI
{
    private GameDevHQ.FileBase.Gatling_Gun.Gatling_Gun _turret;
    private void Start()
    {
        _turret = GetComponent<GameDevHQ.FileBase.Gatling_Gun.Gatling_Gun>();
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
