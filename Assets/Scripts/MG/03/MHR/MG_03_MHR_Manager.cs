using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_03_MHR_Manager : MonoBehaviour
{
    public static MG_03_MHR_Manager instance;
    public int mazeSize = 10;

    public int posX = 0;

    public int posY = 0;

    public List<List<int>> maze = new List<List<int>>();

    public List<GameObject> doors;

    public List<Image> minimap;

    public List<Color> colors;

    public MG_03_MHR_Page page;

    public Transform monster;


    void Start()
    {
        instance = this;
        GenerateMaze();
        LoadRoom(0, 0);
    }

    // Maze
    // 0 = Libre
    // 1 = Mur
    // 2 = Sortie

    // North -> East -> South -> West

    // Colors
    // 0 = Room
    // 1 = Exit

    public void LoadRoom(int xd, int yd)
    {
        posX += xd;
        posY += yd;

        monster.gameObject.SetActive(false);

        if (maze[posY][posX] == 2)
        {
            MG_Starter.instance.LoadNext();
        }

        doors[0].SetActive(posY != 0 && maze[posY - 1][posX] != 1);
        doors[1].SetActive(posX != mazeSize - 1 && maze[posY][posX + 1] != 1);
        doors[2].SetActive(posY != mazeSize - 1 && maze[posY + 1][posX] != 1);
        doors[3].SetActive(posX != 0 && maze[posY][posX - 1] != 1);

        page.gameObject.SetActive(false);

        MiniMap();
    }


    void MiniMap()
    {
        if (posY != 0 && posX != 0 && maze[posY - 1][posX - 1] != 1)
        {
            minimap[0].color = maze[posY - 1][posX - 1] == 0 ? colors[0] : colors[1];
        }
        else
        {
            minimap[0].color = Color.black;
        }

        if (posY != 0 && maze[posY - 1][posX] != 1)
        {
            minimap[1].color = maze[posY - 1][posX] == 0 ? colors[0] : colors[1];
        }
        else
        {
            minimap[1].color = Color.black;
        }

        if (posY != 0 && posX != mazeSize - 1 && maze[posY - 1][posX + 1] != 1)
        {
            minimap[2].color = maze[posY - 1][posX + 1] == 0 ? colors[0] : colors[1];
        }
        else
        {
            minimap[2].color = Color.black;
        }

        if (posX != 0 && maze[posY][posX - 1] != 1)
        {
            minimap[3].color = maze[posY][posX - 1] == 0 ? colors[0] : colors[1];
        }
        else
        {
            minimap[3].color = Color.black;
        }

        if (posX != mazeSize - 1 && maze[posY][posX + 1] != 1)
        {
            minimap[4].color = maze[posY][posX + 1] == 0 ? colors[0] : colors[1];
        }
        else
        {
            minimap[4].color = Color.black;
        }

        if (posY != mazeSize - 1 && posX != 0 && maze[posY + 1][posX - 1] != 1)
        {
            minimap[5].color = maze[posY + 1][posX - 1] == 0 ? colors[0] : colors[1];
        }
        else
        {
            minimap[5].color = Color.black;
        }

        if (posY != mazeSize - 1 && maze[posY + 1][posX] != 1)
        {
            minimap[6].color = maze[posY + 1][posX] == 0 ? colors[0] : colors[1];
        }
        else
        {
            minimap[6].color = Color.black;
        }

        if (posY != mazeSize - 1 && posX != mazeSize - 1 && maze[posY + 1][posX + 1] != 1)
        {
            minimap[7].color = maze[posY + 1][posX + 1] == 0 ? colors[0] : colors[1];
        }
        else
        {
            minimap[7].color = Color.black;
        }

    }


    void GenerateMaze()
    {
        for (int i = 0; i < mazeSize; i++)
        {
            maze.Add(new List<int>());
            for (int j = 0; j < mazeSize; j++)
            {
                maze[i].Add(1);
            }
        }
        List<int[]> ways;
        int maxTunnel = 5 * mazeSize;
        int maxSizeHallway = 5;
        int randomSizeHallway = Random.Range(1, maxSizeHallway + 1);
        int currentSizeHallway = 0;
        int x = 0;
        int y = 0;

        while (maxTunnel > 0)
        {
            ways = new List<int[]>();
            ways.Add(new int[] { 1, 0 });
            ways.Add(new int[] { -1, 0 });
            ways.Add(new int[] { 0, 1 });
            ways.Add(new int[] { 0, -1 });
            if (x <= 0)
            {
                ways.Remove(new int[] { -1, 0 });
            }
            if (x >= mazeSize - 1)
            {
                ways.Remove(new int[] { 1, 0 });
            }
            if (y <= 0)
            {
                ways.Remove(new int[] { 0, -1 });
            }
            if (y >= mazeSize - 1)
            {
                ways.Remove(new int[] { 0, 1 });
            }

            int[] move = ways[Random.Range(0, ways.Count)];
            while (currentSizeHallway < randomSizeHallway)
            {
                if (x + move[0] > mazeSize - 1 || y + move[1] > mazeSize - 1 || x + move[0] < 0 || y + move[1] < 0)
                {
                    currentSizeHallway = randomSizeHallway;
                    break;
                }

                maze[y][x] = 0;
                x += move[0];
                y += move[1];
                currentSizeHallway++;

            }

            currentSizeHallway = 0;
            randomSizeHallway = Random.Range(1, maxSizeHallway + 1);
            maxTunnel--;

        }


        maze[y][x] = 2;


    }

}
