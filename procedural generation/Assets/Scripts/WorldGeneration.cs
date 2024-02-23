using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WorldGeneration : MonoBehaviour
{
    public GameObject blockGameObject;
    public GameObject PlaceObject;
    public GameObject Player;
    public int worldsizeX = 10;
    public int worldSizeZ = 10;
    public int gridOffset = 2;
    public int noiseHeight = 3;
    private List<Vector3> blockposition = new List<Vector3>();
    private Vector3 startPosition;
    private Hashtable blockcontainer = new Hashtable();
    // Start is called before the first frame update
    void Start()
    {
        //for(int x = -worldsizeX; x < worldsizeX; x++)
        //{
        //    for(int z = -worldSizeZ; z < worldSizeZ; z++)
        //    {
        //        Vector3 pos = new Vector3(x * 1+startPosition.x, GenerateNoise(x,z,8f)*noiseHeight, z * 1 + startPosition.z);
        //        GameObject block = Instantiate(blockGameObject, pos, Quaternion.identity, this.transform);

        //        blockcontainer.Add(pos, block);
        //        blockposition.Add(block.transform.position);
        //    }
        //}
      //  Spawnobjects();
    }
    private void Update()
    {
        if(Mathf.Abs(xPlayerMove())>=1|| Mathf.Abs(zPlayerMove()) >= 1)
        {
            for (int x = -worldsizeX; x < worldsizeX; x++)
            {
                for (int z = -worldSizeZ; z < worldSizeZ; z++)
                {
                    Vector3 pos = new Vector3(x * 1 + xPlayerLocation(), GenerateNoise(x+xPlayerLocation(), z+zPlayerLocation(), 8f) * noiseHeight, z * 1 + zPlayerLocation());

                    if (!blockcontainer.ContainsKey(pos))
                    {

                        GameObject block = Instantiate(blockGameObject, pos, Quaternion.identity, this.transform);
                        blockcontainer.Add(pos, block);
                        blockposition.Add(block.transform.position);
                    }

                }
            }
        }
    }
    public int xPlayerMove()
    {
        return (int)(Player.transform.position.x - startPosition.x);
    }

    public int zPlayerMove()
    {
        return (int)(Player.transform.position.z - startPosition.z);
    }
    private int xPlayerLocation()
    {
        return (int)Mathf.Floor(Player.transform.position.x);
    }
    private int zPlayerLocation()
    {
        return (int)Mathf.Floor(Player.transform.position.z);
    }


    private void Spawnobjects()
    {
        for (int i = 0; i <= 20; i++)
        {
            Instantiate(PlaceObject, Objectspawnlocation(), Quaternion.identity, this.transform);
        }
    }

    private Vector3 Objectspawnlocation()
    {
        int randomIndex = Random.Range(0, blockposition.Count);
        Vector3 newpos = new Vector3(blockposition[randomIndex].x, blockposition[randomIndex].y + 0.5f, blockposition[randomIndex].z);
        return newpos;
    }

    private float GenerateNoise(int x,int z,float detailScale) 
    {
        float xNoice = (x + this.transform.position.x) / detailScale;
        float zNoice = (z + this.transform.position.z) / detailScale;

        return Mathf.PerlinNoise(xNoice, zNoice);

    }
}
