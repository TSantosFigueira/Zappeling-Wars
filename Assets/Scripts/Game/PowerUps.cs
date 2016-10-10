﻿using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class PowerUps : MonoBehaviour {

    [Range(0,10)]
    public float effectTime;

    public string[] powerUpTypes;

    private int index;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PowerUp")){
            BeginPowerUp();
        }
    }


    private void BeginPowerUp() {
        //Seleciona um efeito aleatorio
        index = Random.Range(0, powerUpTypes.Length);
        //Inicia o efeito de Power Up.
        if(powerUpTypes[index] == "Barreira"){
            GetComponent<PlayerHealth>().StartShield();
        }
        else if(powerUpTypes[index] == "BulletPower"){
            var shoot = GetComponent<PlayerShoot>();
            shoot.bulletPrefab.GetComponent<Bullet>().DamageBuff(5);
            
        }else if(powerUpTypes[index] == "SpeedPower"){
            GetComponent<PlayerController>().SpeedBuff(10);
        }
        Invoke("FinishPowerUp", effectTime); //Cancela o efeito depois do tempo limite.
        
    }

    public void FinishPowerUp() {
        //Cancela os efeitos.
        if (powerUpTypes[index] == "Shield")
        {
            GetComponent<PlayerHealth>().FinishShield();
        }else if (powerUpTypes[index] == "BulletPower"){
            var shoot = GetComponent<PlayerShoot>();
            shoot.bulletPrefab.GetComponent<Bullet>().ReturnDamage();
        }
        else if (powerUpTypes[index] == "SpeedPower"){
            GetComponent<PlayerController>().SpeedNormalize();
        }
    }

}
