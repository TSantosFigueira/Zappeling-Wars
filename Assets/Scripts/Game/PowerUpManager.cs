using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class PowerUpManager : NetworkBehaviour {

    //Posições de referencia para montar o grid de posições
    public Transform minTX, minTY, maxTX, maxTY;
    //Variavel que defini o tamanho entre os pontos no grid
    [SerializeField]
    private int lootSpace;
    //Lista de posições(grid)
    private List<Vector3> gridList = new List<Vector3>();

    public GameObject powerUpRef;

    private GameObject powerUpInstance;

    [Header("Tempo para spawnar outro PowerUp")]
    public float powerupSpawnTime = 9;

    public override void OnStartServer(){
        Debug.Log("Serve iniciado");
        InicializeGrid();
        Debug.Log("Lista Pos: " + gridList.Count);

        InvokeRepeating("Cmd_SpawnRandomPower", 10, powerupSpawnTime);

    }

    private void InicializeGrid() {
        //Limpa o grid e depois coloca as posições
        gridList.Clear();
        int minX = (int)minTX.position.x;
        int minY = (int)minTY.position.y;
        int maxX = (int)maxTX.position.x;
        int maxY = (int)maxTY.position.y;

        for (int x = minX; x < maxX; x += lootSpace) {
            for (int y = minY; y < maxY; y += lootSpace) {
                gridList.Add(new Vector3(x, y, 0));
            }
        }
    }

    public Vector3 RandomPosition() {
        //Retorna uma posição randomica
        int randomIndex = Random.Range(0, gridList.Count);
        if (Physics.CheckSphere(gridList[randomIndex], 2)){
            //Verifica se tem um objeto dentro de uma area de 2f;
            Debug.Log("Chocou em algo");
            return RandomPosition();
        }else {
            Debug.Log("Posição selecionada: " + gridList[randomIndex]);
            return gridList[randomIndex];
        }
    }

    [Command]
    private void Cmd_SpawnRandomPower() {
        //Função chamada para spawnar o power up
        Cmd_SpawnPowerUp(powerUpRef, RandomPosition());
    }

    [Command]
    private void Cmd_CleanPowerUp(){
        Destroy(powerUpInstance);
    }

    [Command]
    private void Cmd_SpawnPowerUp(GameObject powerUp, Vector3 position) {
        if(powerUpInstance){
            Destroy(powerUpInstance);
        }
        //Instancia o powerup no mapa.
        powerUpInstance = (GameObject)Instantiate(powerUp, position, Quaternion.identity);
        NetworkServer.Spawn(powerUpInstance);
        Invoke("Cmd_CleanPowerUp", powerupSpawnTime - 2f);
    }

}
