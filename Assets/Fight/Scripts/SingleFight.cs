using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SingleFight : MonoBehaviour
{
    GameObject[] entitiesArray;
    bool isCounting = true;
    public int maxSpeedCounter = 100;

    GameObject[] indicatorsArray;
    public GameObject camera;

    int currentChar = 0;
    // Start is called before the first frame update
    void Start()
    {
        entitiesArray = GameObject.FindGameObjectsWithTag("Player");
        entitiesArray = entitiesArray.Concat(GameObject.FindGameObjectsWithTag("Enemy")).ToArray();
        entitiesArray = entitiesArray.OrderByDescending(item => item.GetComponent<EntityStatistics>().speed).ToArray();
        indicatorsArray = new GameObject[entitiesArray.Length];
        // Upewnij się, że camera jest przypisana
        camera = Camera.main.gameObject;
        for (int i = 0; i < entitiesArray.Length; i++)
        {
            GameObject objToSpawn = new GameObject();
            objToSpawn.AddComponent<SpriteRenderer>();
            objToSpawn.GetComponent<SpriteRenderer>().sprite = entitiesArray[i].GetComponent<SpriteRenderer>().sprite;
            objToSpawn.GetComponent<SpriteRenderer>().color = entitiesArray[i].GetComponent<SpriteRenderer>().color;
            objToSpawn.GetComponent<SpriteRenderer>().sortingLayerID = entitiesArray[i].GetComponent<SpriteRenderer>().sortingLayerID;
            objToSpawn.GetComponent<SpriteRenderer>().sortingLayerName = entitiesArray[i].GetComponent<SpriteRenderer>().sortingLayerName;
            objToSpawn.GetComponent<SpriteRenderer>().sortingOrder = entitiesArray[i].GetComponent<SpriteRenderer>().sortingOrder;
            objToSpawn.transform.position = new Vector3(camera.transform.position.x-5, camera.transform.position.y+4);
            
            indicatorsArray[i] = objToSpawn;
            // Debug.Log(item);
            // Debug.Log(entitiesArray.Length);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isCounting){
            
            while (currentChar < entitiesArray.Length)
            {
               //Debug.Log(currentChar);
                indicatorsArray[currentChar].transform.position = new Vector3((entitiesArray[currentChar].GetComponent<EntityStatistics>().speedCounter/10)-5, indicatorsArray[currentChar].transform.position.y);
                entitiesArray[currentChar].GetComponent<EntityStatistics>().speedCounter+= entitiesArray[currentChar].GetComponent<EntityStatistics>().speed;
                if(entitiesArray[currentChar].GetComponent<EntityStatistics>().speedCounter >= maxSpeedCounter){
                    indicatorsArray[currentChar].transform.position = new Vector3(camera.transform.position.x+5, camera.transform.position.y+4);
                    //indicatorsArray[currentChar].transform.position = new Vector3(camera.transform.position.x+5, camera.transform.position.y+4);     
                     //Debug.Log("CHuj " + currentChar);
                    isCounting = false;
                    break;
                }
            
            currentChar+=1;
            }if(isCounting){
                //Debug.Log("chuuuuuuuuuuuuuj");
            currentChar = 0;
            }
            
        }else{
            //Debug.Log("CHuj2 " + currentChar);
            if(entitiesArray[currentChar].GetComponent<PlayerFight>()){
                entitiesArray[currentChar].GetComponent<PlayerFight>().isMenuShown = true;
            }else{
                entitiesArray[currentChar].GetComponent<EnemyFight>().isSelected = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            if(entitiesArray[currentChar].GetComponent<PlayerFight>()){
                entitiesArray[currentChar].GetComponent<PlayerFight>().isMenuShown = false;
            }else{
                entitiesArray[currentChar].GetComponent<EnemyFight>().isSelected = false;
            }
            indicatorsArray[currentChar].transform.position = new Vector3(camera.transform.position.x-5, camera.transform.position.y+4);
            entitiesArray[currentChar].GetComponent<EntityStatistics>().speedCounter = 1;
            isCounting = true;
        }
    }
}
