using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MapGeneration : MonoBehaviour
{
    public static MapGeneration instance;
    public int mapX, mapY; // map size 1 by 1 units in Unity
    public int currentLevel;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
        currentLevel = 1;
    }
    void Start()
    {
        mapX = 36;
        mapY = 34;

        StartLevel(currentLevel);

    }

    #region Map Creation
    void CreateMap(BlockadeMesh[] meshes)
    {
        foreach (BlockadeMesh mesh in meshes)
        {
            Rectangle[] rectangles = JsonHelper.getJsonArray<Rectangle>(File.ReadAllText(Application.dataPath + "/" + mesh.mesh));
            CreateMesh(mesh.x, mesh.y, rectangles);
        }
    }

    #region Mesh Creation
    void CreateMesh(int x, int y, Rectangle[] rectangleArray)
    {

        // initialize mesh vertices and triangles lists
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        // for each row in mesh add vertices and triangles
        for (int i = 0; i < rectangleArray.Length; i++)
        {

            // add always existant vertices
            List<Vector3> rectangleVertices = new List<Vector3>();
            rectangleVertices.AddRange(new Vector3[] {
                new Vector3(rectangleArray[i].x, 0, i * -1),  //0
                new Vector3(rectangleArray[i].x + rectangleArray[i].length, 0, i * -1),
                new Vector3(rectangleArray[i].x + rectangleArray[i].length, 1, i * -1),
                new Vector3(rectangleArray[i].x, 1, i * -1),
                new Vector3(rectangleArray[i].x, 1, (i * -1) + 1),
                new Vector3(rectangleArray[i].x + rectangleArray[i].length, 1, (i * -1) + 1),
                new Vector3(rectangleArray[i].x + rectangleArray[i].length, 0, (i * -1) + 1),
                new Vector3(rectangleArray[i].x, 0, (i * -1) + 1),
            });

            //initialize triangles list
            List<int> rectangleTriangles = new List<int>();

            // add always existant triangles
            rectangleTriangles.AddRange(new int[] {
                2, 3, 4,
                2, 4, 5,
                1, 2, 5,
                1, 5, 6,
                0, 7, 4,
                0, 4, 3,
                0, 6, 7,
                0, 1, 6
            });

            // if not last row find required bottom vertices
            if (i < rectangleArray.Length - 1)
            {
                // check if face is needed from left side of rectangle
                if (rectangleArray[i + 1].x > rectangleArray[i].x)
                {
                    //getting indexes for created vertices
                    int[] newVerticesIndexes = { rectangleVertices.Count, rectangleVertices.Count + 1 };

                    //Add vertices and triangles 
                    rectangleVertices.AddRange(new Vector3[] {
                    new Vector3(rectangleArray[i + 1].x, 0, i * -1),
                    new Vector3(rectangleArray[i + 1].x, 1, i * -1)
                    });

                    rectangleTriangles.AddRange(new int[]
                    {
                        0, 3, newVerticesIndexes[1],
                        0, newVerticesIndexes[1], newVerticesIndexes[0],
                    });
                }

                // check if face is needed from right side of rectangle
                if (rectangleArray[i + 1].x + rectangleArray[i + 1].length < rectangleArray[i].x + rectangleArray[i].length)
                {
                    // get indexes for created vertices
                    int[] newVerticesIndexes = { rectangleVertices.Count, rectangleVertices.Count + 1 };

                    // Add vertices and triangles 
                    rectangleVertices.AddRange(new Vector3[] {
                    new Vector3(rectangleArray[i + 1].x + rectangleArray[i + 1].length, 0, i * -1), // 10
                    new Vector3(rectangleArray[i + 1].x + rectangleArray[i + 1].length, 1, i * -1) // 11
                    });

                    rectangleTriangles.AddRange(new int[]
                    {
                        newVerticesIndexes[0], newVerticesIndexes[1], 2,
                        newVerticesIndexes[0], 2, 1
                    });
                }
            } else // no additional vertices added, just fully render bot side
            {
                rectangleTriangles.AddRange(new int[]
                    {
                        0, 2, 1,
                        0, 3, 2
                    });
            }

            // if not first row find required top vertices
            if (i > 0)
            {
                // check if face is needed from left side of rectangle
                if (rectangleArray[i - 1].x > rectangleArray[i].x)
                {
                    // get indexes
                    int[] newVerticesIndexes = { rectangleVertices.Count, rectangleVertices.Count + 1 };

                    // Add vertices and triangles 
                    rectangleVertices.AddRange(new Vector3[] {
                    new Vector3(rectangleArray[i - 1].x, 0, (i * -1) + 1),
                    new Vector3(rectangleArray[i - 1].x, 1, (i * -1) + 1)
                    });

                    rectangleTriangles.AddRange(new int[]
                    {
                        7, newVerticesIndexes[1], 4,
                        7, newVerticesIndexes[0], newVerticesIndexes[1]
                    });

                }

                // check if face is needed from right side of rectangle
                if (rectangleArray[i - 1].x + rectangleArray[i - 1].length < rectangleArray[i].x + rectangleArray[i].length)
                {
                    // get indexes
                    int[] newVerticesIndexes = { rectangleVertices.Count, rectangleVertices.Count + 1 };

                    // add vertices and triangles
                    rectangleVertices.AddRange(new Vector3[] {
                    new Vector3 (rectangleArray[i - 1].length + rectangleArray[i - 1].x, 0, (i * -1) + 1), // 10 11
                    new Vector3 (rectangleArray[i - 1].length + rectangleArray[i - 1].x, 1, (i * -1) + 1) });

                    rectangleTriangles.AddRange(new int[] // 5 ir 6
                    {
                        5, newVerticesIndexes[1], newVerticesIndexes[0],
                        5, newVerticesIndexes[0], 6
                    });
                }
            }
            else // no additional vertices added, just render top side
            {
                rectangleTriangles.AddRange(new int[]
                    {
                        5, 4, 7,
                        5, 7, 6
                    });
            }

            // initialize array of indexes
            int[] verticesIndexes = new int[rectangleVertices.Count];

            // add rectangle vertices to whole mesh vertices and get according indexes of them
            for (int index = 0; index < rectangleVertices.Count; index++)
            {
                Vector3 vertice = rectangleVertices[index];
                if (vertices.Contains(vertice))
                {
                    verticesIndexes[index] = vertices.IndexOf(vertice);
                } else
                {
                    verticesIndexes[index] = vertices.Count;
                    vertices.Add(vertice);
                }
            }

            //change indexes in triangles according to the whole mesh.
            for (int index = 0; index < rectangleTriangles.Count; index++)
            {
                rectangleTriangles[index] = verticesIndexes[rectangleTriangles[index]];
            }

            // add rectangle triangles to the whole mesh
            triangles.AddRange(rectangleTriangles);
        }

        // Create the generated mesh and place it at location

        GameObject go = new GameObject("Blockade", typeof(MeshFilter), typeof(MeshRenderer), typeof(Blockade));
        Mesh goMesh = go.GetComponent<MeshFilter>().mesh;
        goMesh.Clear();
        goMesh.vertices = vertices.ToArray();
        goMesh.triangles = triangles.ToArray();
        //goMesh.Optimize();
        goMesh.RecalculateNormals();
        go.AddComponent<MeshCollider>().convex = true;
        go.GetComponent<Renderer>().material = new Material(Shader.Find("Diffuse"));
        go.tag = "Blockade";
      //  MeshCollider meshc = go.AddComponent(typeof(MeshCollider)) as MeshCollider;
      //  meshc.convex = true;
        go.transform.position = new Vector3(x, -0.5f, y);
    }

    #endregion

    //class for each rectangle in Map's mesh
    [System.Serializable]
    class Rectangle
    {
        public int x;
        public int length;

        // you can load json path xD lol easy
    }

    // class for mesh in map
    [System.Serializable]
    class BlockadeMesh
    {
        public int x;
        public int y;
        public string mesh; // path to mesh json.
    }

    // Map Spawn and Mesh data
    class MapData
    {
        public BlockadeMesh[] meshes;
        public SpawnData spawnData;
    }

    // Level Spawn Data
    [System.Serializable]
    class SpawnData
    {
        public SpawnWave[] waves;

        public SpawnData (string jsonString)
        {
            this.waves = JsonHelper.getJsonArray<SpawnWave>(jsonString);
        }
    }

    // Enemy wave class
    [System.Serializable]
    public class SpawnWave
    {
        public int timer; // timer when each enemy will be released
        public int[] enemies; // 0 - simple, 1 - advanced, 2 - crazy, 3 - bossy <-- create these enemies
    }

    // json wrapper for Map Data json
    [System.Serializable]
    public class MapDataJson
    {
        public string meshes;
        public string spawnData;
    }

    public static class JsonHelper
    {
        public static T[] getJsonArray<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.objects;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] objects;
        }
    }

    #endregion

    public void StartLevel(int level)
    {
        level = 1;
        Debug.Log("Starting level " + (level + 1)); 
        if (level > 3)
        {
            Debug.Log("No more levels :(");
            return;
        }
        currentLevel = level + 1;
        ClearMap();
        var path = Application.dataPath + "/Json/MapData/Maps/Level" + currentLevel + ".json";
        var jsonString = File.ReadAllText(path);
        MapDataJson MapDataJson = JsonUtility.FromJson<MapDataJson>(jsonString);
        MapData data = new MapData();
        path = Application.dataPath + "/" + MapDataJson.spawnData;
        data.spawnData = new SpawnData(File.ReadAllText(path));
        SpawnManager.instance.waves = data.spawnData.waves;
        SpawnManager.instance.StartSpawning();
        data.meshes = JsonHelper.getJsonArray<BlockadeMesh>(File.ReadAllText(Application.dataPath + "/" + MapDataJson.meshes));
        currentLevel = level ;

        CreateMap(data.meshes);
    }

    public void ToggleRestart()
    {
        StartCoroutine("Restart", 2f);
    }

    IEnumerator Restart()
    {
        SceneManager.LoadScene(0);
        yield return null;
    }

    public void ClearMap()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Blockade"))
        {
            Destroy(go);
        }
    }

    public bool PointIsInMap(Vector3 point)
    {
        // returns true if point is in enemy area
        return point.x > 0 && point.z > 23 && point.x < mapX && point.z < mapY;
    }
}
