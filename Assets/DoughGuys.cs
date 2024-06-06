using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class DoughGuys : MonoBehaviour
{   

    
    public bool back=false;
    public float speed=10f;
    public bool isDoing=true;
    public bool Toxic=false;
    public bool Starve;
    public int ReproductiveRate=10;
    public int ToxicFatalityRate=90;

    public bool ToxicDie=false;
    public Vector3 originPosition=new Vector3(0.6f,0.9f,-6.8f);
    public Material DieMaterial;
    public Material ToxicMaterial;
    public Material WhiteMaterial;

    int[] randomArray(int n){
        int[] result=new int[n];
        for(int i=0;i<n;i++){
            result[i]=Random.Range(0,n);
            for(int j=0;j<i;j++){
                if(result[i]==result[j]){
                    i--;
                    break;
                }
            }
        }
        return result;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="food" && other.GetComponent<Food>().tracked==name){
            Destroy(other.gameObject);
            if(!Toxic&&other.GetComponent<Food>().hasToxic){
                    
                if(Random.Range(1,100)<=ToxicFatalityRate){
                    gameObject.GetComponent<Renderer>().material=DieMaterial;
                    Destroy(gameObject,0.05f);
                    ToxicDie=true;
                }
                //if(Random.Range(1,100)<=20){
                else{
                    gameObject.GetComponent<Renderer>().material=ToxicMaterial;
                    Toxic=true;
                }
            }
            
            back=true;
            if(!ToxicDie){
                if(Random.Range(1,100)<=ReproductiveRate)Instantiate(gameObject);
            }
            
            
        }
    }
    void go(Vector3 pos){
        float theata=Mathf.Atan2(pos.z-transform.position.z,pos.x-transform.position.x);
        transform.Translate(Mathf.Cos(theata)*speed*Time.deltaTime,0,Mathf.Sin(theata)*speed*Time.deltaTime);
    }
    float distance(Vector3 pos){
        return Mathf.Sqrt((pos.x-transform.position.x)*(pos.x-transform.position.x)/*+(pos.y-transform.position.y)*(pos.y-transform.position.y)*/+(pos.z-transform.position.z)*(pos.z-transform.position.z));
    }
    int findFood(GameObject[] fo){
        int idx;
        int[] ranArr=randomArray(fo.Length);
        for(idx=0;idx<fo.Length;idx++){
            if(fo[ranArr[idx]].GetComponent<Food>().tracked==name)break;
        }
        if(fo.Length==idx){
            for(idx=0;idx<fo.Length;idx++){
                if(fo[ranArr[idx]].GetComponent<Food>().tracked=="empty"){
                    fo[ranArr[idx]].GetComponent<Food>().tracked=name;
                    break;
                }    
            }
        }
        if(fo.Length==idx)return -1;
        return ranArr[idx];
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isDoing){
            if(back){
                go(originPosition);
                if(distance(originPosition)<0.5){
                    back=false;
                    transform.position=originPosition;
                    isDoing=false;
                }
            }
            
            else if(!Starve&&GameObject.FindWithTag("food")){
                GameObject[] fo=GameObject.FindGameObjectsWithTag("food");
                int foodIdx=findFood(fo);
                if(foodIdx!=-1){
                    go(fo[foodIdx].transform.position);
                }
            }
        }
        if(Starve){
                
                //Invoke("DestroySelf", 0.0f);
                
                Destroy(gameObject,0.05f);
                gameObject.GetComponent<Renderer>().material=DieMaterial;
        }
    }
}