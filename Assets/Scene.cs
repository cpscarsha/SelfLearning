using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public static int DoughGuysNum=1;
    public static int Round=0;
    public static int DoughGuysCount=0;
    public static int ToxicGuysCount=0;
    public static bool isBacking=false;
    public GameObject FoodCopy;
    public GameObject ToxicFoodCopy;
    public int FoodCount=10;
    public int ToxicFoodCount=10;
    public static int randDough=0;
    public static bool ChartTrg1=false;
    GameObject[] Dough;
    

    int[] ranArr;

    public int getToxicGuysCount(){
        Dough=GameObject.FindGameObjectsWithTag("Dough");
        int result=0;
        for(int i=0;i<Dough.Length;i++)if(Dough[i].GetComponent<DoughGuys>().Toxic)result++;
        return result;
    }

    bool wait=false;
    void waitFinish(){
        Dough=GameObject.FindGameObjectsWithTag("Dough");
        for(int i=0;i<Dough.Length;i++)Dough[i].GetComponent<DoughGuys>().ToxicDie=false;
        ranArr=randomArray(Dough.Length);
        
        isBacking=false;
        int[] randNum=randomArray(Dough.Length);
        for(int i=0;i<Dough.Length;i++){
            Dough[randNum[i]].name="DoughGuys"+i;
            Dough[randNum[i]].GetComponent<DoughGuys>().isDoing=true;
        }
        float FoodDis,theata;
        DestroyFood();
        for(int i=0;i<FoodCount;i++){
            FoodDis=Random.Range(0,20);
            theata=Random.Range(0,2*Mathf.PI);
            addFood(new Vector3(FoodDis*Mathf.Cos(theata),0.5f,FoodDis*Mathf.Sin(theata)));
        }
        for(int i=0;i<ToxicFoodCount;i++){
            FoodDis=Random.Range(0,20);
            theata=Random.Range(0,2*Mathf.PI);
            addToxicFood(new Vector3(FoodDis*Mathf.Cos(theata),0.5f,FoodDis*Mathf.Sin(theata)));
        }
        ChangeFoodName();
        Round++;
        //GameObject[] fo=GameObject.FindGameObjectsWithTag("food");
        //Debug.Log(Dough.Length-(FoodCount+ToxicFoodCount));
        for(int i=0;i<Dough.Length;i++)Dough[ranArr[i]].GetComponent<DoughGuys>().Starve=false;
        for(int i=0;i<Dough.Length-(FoodCount+ToxicFoodCount);i++)Dough[ranArr[i]].GetComponent<DoughGuys>().Starve=true;
        ChartTrg1=true;

        wait=false;
    }
    void DestroyFood(){
        GameObject[] fo=GameObject.FindGameObjectsWithTag("food");
        for(int i=0;i<fo.Length;i++)Destroy(fo[i]);
    }
    void ChangeFoodName(){
        GameObject[] fo=GameObject.FindGameObjectsWithTag("food");
        for(int i=0;i<fo.Length;i++)fo[i].name="Food"+Random.Range(0,1000);
    }
    void addFood(Vector3 pos){
        Instantiate (FoodCopy,pos,Quaternion.Euler(0, 90, 0));
    }
    void addToxicFood(Vector3 pos){
        Instantiate (ToxicFoodCopy,pos,Quaternion.Euler(0, 90, 0));
    }
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
    // Start is called before the first frame update
    void Start()
    {
        randDough=0;
        Dough=GameObject.FindGameObjectsWithTag("Dough");
        DoughGuysCount=Dough.Length;
        waitFinish();
    }


    // Update is called once per frame
    void Update()
    {
        Dough=GameObject.FindGameObjectsWithTag("Dough");
        ToxicGuysCount=getToxicGuysCount();
        DoughGuysCount=Dough.Length;
        if(randDough==Dough.Length)randDough=0;
        else randDough++;
        for(int i=0;i<Dough.Length;i++)Dough[i].GetComponent<DoughGuys>().originPosition=new Vector3(29*Mathf.Cos(2*Mathf.PI/DoughGuysCount*i),0,29*Mathf.Sin(2*Mathf.PI/DoughGuysCount*i));
        if(!wait){
            bool roundDown=true;
            for(int i=0;i<Dough.Length;i++)if(Dough[i].GetComponent<DoughGuys>().isDoing==true)roundDown=false;
            if(roundDown&&!isBacking){
                isBacking=true;
                for(int i=0;i<Dough.Length;i++){
                    Dough[i].GetComponent<DoughGuys>().back=true;
                    Dough[i].GetComponent<DoughGuys>().isDoing=true;
                }                                
            }
            else if(roundDown&&isBacking){
                wait=true;
                Invoke("waitFinish",0.2f);
            }
        }
    }
}
