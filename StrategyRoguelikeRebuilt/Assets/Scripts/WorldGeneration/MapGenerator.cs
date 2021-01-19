using UnityEngine.Tilemaps;
using System.Collections;
using UnityEngine;

namespace MapGenerationSystem
{
    public class MapGenerator : MonoBehaviour
    {
        private Tilemap ground;
        private TileBase[] tileBases;
        private NoiseData noiseData;

        public int seed;
        public bool useRandomSeed;
        public int mapWidth = 300, mapHeight = 50, dirtLayerHeight = 15, stoneHeight = 35, stoneLayerHeight = 10;

        private void Start()
        {
            InitializeSeed();
            InitializeGenerator();
            StartCoroutine(GenerateMap());
        }
        private IEnumerator GenerateMap() /// ADD CHUNK SUPPORT 
        {
            float[] dirtHeightMap = Noise.Generate1DNoiseMap(mapWidth, seed, noiseData);
            float[] stoneHeightMap = Noise.Generate1DNoiseMap(mapWidth, seed, noiseData);
            float[,] noiseMap = Noise.Generate2DNoiseMap(mapWidth, mapHeight + dirtLayerHeight, seed, noiseData);

            for (int x = 0; x < mapWidth; x += 2)
            {
                for (int y = 0; y < mapHeight + dirtLayerHeight * Mathf.Clamp(dirtHeightMap[x], 0, 1); y++)
                {
                    float stoneLevel = stoneHeight + stoneLayerHeight * stoneHeightMap[x];

                    if (y < stoneLevel || noiseMap[x, y] < 0.6f)
                    {
                        ground.SetTile(new Vector3Int(x, y, 0), tileBases[1]);
                        ground.SetTile(new Vector3Int(x + 1, y, 0), tileBases[1]);
                    }
                    else
                    {
                        ground.SetTile(new Vector3Int(x, y, 0), tileBases[0]);
                        ground.SetTile(new Vector3Int(x + 1, y, 0), tileBases[0]);
                    }
                }
                yield return new WaitForSeconds(0.01f);
            }
            ground.GetComponent<TilemapCollider2D>().enabled = true;
        }
        private void InitializeSeed()
        {
            if (useRandomSeed)
            {
                seed = Random.Range(-100000, 100000);
                Random.InitState(seed);
            }
            else Random.InitState(seed);
        }
        private void InitializeGenerator()
        {
            ground = transform.Find("groundTiles").GetComponent<Tilemap>();
            tileBases = new TileBase[2];
            tileBases[0] = Resources.Load<TileBase>("grass_tile");
            tileBases[1] = Resources.Load<TileBase>("stone_tile");
            noiseData = new NoiseData(3, 25f, 0.5f, 2.5f);
        }
    }
}