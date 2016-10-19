using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class PowerUps : MonoBehaviour{

    [Range(0, 10)]
    public float effectTime;

    public string[] powerUpTypes;

    [SerializeField]
    private bool isActive;

    private int index;

    public int damageBuff;
    public bool weaponBuff;

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("PowerUp") && !isActive){
            BeginPowerUp();
            Destroy(other.gameObject);
        }
    }


    private void BeginPowerUp(){
        //Seleciona um efeito aleatorio
        index = Random.Range(0, powerUpTypes.Length);
        //Inicia o efeito de Power Up.
       // Debug.Log("Inicio o power up: " + powerUpTypes[index]);
        if (powerUpTypes[index] == "Shield"){
            GetComponent<PlayerHealth>().StartShield();
        }else if (powerUpTypes[index] == "BulletPower"){
            weaponBuff = true;
        }else if (powerUpTypes[index] == "SpeedPower"){
            GetComponent<PlayerController>().SpeedBuff(10);
        }
        isActive = true;
        Invoke("FinishPowerUp", effectTime); //Cancela o efeito depois do tempo limite.

    }

    public void FinishPowerUp(){
        //Cancela os efeitos.
        if (powerUpTypes[index] == "Shield"){
            GetComponent<PlayerHealth>().FinishShield();
        }else if (powerUpTypes[index] == "BulletPower"){
            weaponBuff = false;
        }else if (powerUpTypes[index] == "SpeedPower"){
            GetComponent<PlayerController>().SpeedNormalize();
        }
        isActive = false;
    }

}