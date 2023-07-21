using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    public static LevelManager sharedInstance;
    // conjunto de los bloques que tenemos disponibles para colocar en el nivel
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();

    // conjunto de los bloques de nivel que conforman actualmente el nivel
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();

    public Transform levelStartPosition;  
    
    void Awake(){

        if(sharedInstance == null){

            sharedInstance = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateInitialBlocks();
    }

    public void AddLevelBlock(){
        
        // generamos un numero alearotio en el rango de 0 a la cantidad de bloques de nivel que tenemos
        int randomIdx = Random.Range(0,allTheLevelBlocks.Count);
        LevelBlock block;
        Vector3 spawnPosition = Vector3.zero;

        if(currentLevelBlocks.Count == 0) {

            block = Instantiate(allTheLevelBlocks[0]);

            spawnPosition = levelStartPosition.position;

        }else{

            block = Instantiate(allTheLevelBlocks[randomIdx]);
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count-1].exitPoint.position;
        }

        block.transform.SetParent(this.transform, false);

        Vector3 correction = new Vector3(
                                        spawnPosition.x - block.startPoint.position.x,
                                        spawnPosition.y - block.startPoint.position.y,
                                        0 /*z*/);

        //movemos a la posicion corregida el bloque y lo añadimos al array
        block.transform.position = correction;                                
        currentLevelBlocks.Add(block);

    }

    public void RemoveLevelBlock(){

        LevelBlock oldBlock = currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldBlock);
        Destroy(oldBlock.gameObject);

    }

    public void RemoveAllLevelBlocks(){
        
        while(currentLevelBlocks.Count > 0){
            RemoveLevelBlock();
        }
    }   

    public void GenerateInitialBlocks(){
        
        for(int i = 0; i < 3; i++){

            AddLevelBlock();
        }
    }
}
