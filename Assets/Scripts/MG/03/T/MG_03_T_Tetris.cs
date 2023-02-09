using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_03_T_Tetris : MonoBehaviour
{
    public Vector3 rotationPoint;
    float previousTime;
    public float fallTime = 0.8f;

    public static int height = 8;
    public static int width = 15;

    private static Transform[,] grid = new Transform[width+1,height+1];

    void Update()
    {
        while(!MG_Starter.instance.canStart){
            return;
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            transform.position+= new Vector3(-1,0,0);
            if(!ValidMove()){
                transform.position-= new Vector3(-1,0,0);
            }
        }else if(Input.GetKeyDown(KeyCode.RightArrow)){
            transform.position+= new Vector3(1,0,0);
            if(!ValidMove()){
                transform.position-= new Vector3(1,0,0);
            }
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)){
            transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),90);
            if(!ValidMove()){
                transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),-90);
            }
        }

        if(Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime/10 : fallTime)){
            transform.position += new Vector3(0,-1,0);
            if(!ValidMove()){
                transform.position -= new Vector3(0,-1,0);
                AddToGrid();
                CheckForLines();

                this.enabled = false;
                FindObjectOfType<MG_03_T_Spawn>().NewTetromino();
            }
            previousTime = Time.time;
        }
        
    }

    void AddToGrid(){
        foreach(Transform child in transform){
            int roundedX = Mathf.RoundToInt(child.transform.position.x);
            int roundedY = Mathf.RoundToInt(child.transform.position.y);

            if(roundedY >= height){
                UnityEngine.SceneManagement.SceneManager.LoadScene("T");
                return;
            }
            grid[roundedX,roundedY] = child;
        }     
    }

    void CheckForLines(){
        for(int i = height-1; i >= 0;i--){
            if(HasLine(i)){
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    bool HasLine(int i){
        for(int j = 0; j<width+1;j++){
            if(grid[j,i] == null){
                return false;
            }
        }
        return true;
    }

    void DeleteLine(int i){
        for(int j = 0; j<width+1;j++){
            Destroy(grid[j,i].gameObject);
            grid[j,i] = null;
        }
    }

    void RowDown(int i){
        for(int y = i;y < height+1; y++){
            for(int j = 0; j <width+1; j++){
                if(grid[j,y] != null){
                    grid[j,y -1] = grid[j,y];
                    grid[j,y] = null;
                    grid[j,y-1].transform.position -= new Vector3(0,1,0);
                }
            }
        }
    }

    bool ValidMove(){
        foreach(Transform child in transform){
            int roundedX = Mathf.RoundToInt(child.transform.position.x);
            int roundedY = Mathf.RoundToInt(child.transform.position.y);
            if(roundedX < 0 || roundedX > width || roundedY < 0 || roundedY > height){
                return false;
            }
            if(grid[roundedX,roundedY] != null){
                return false;
            }
        }
        return true;
    }
}
