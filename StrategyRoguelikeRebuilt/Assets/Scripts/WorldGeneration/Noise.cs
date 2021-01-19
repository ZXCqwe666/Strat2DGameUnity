using UnityEngine;

namespace MapGenerationSystem
{
	public static class Noise
	{
		public static float[,] Generate2DNoiseMap(int sizeX, int sizeY, int seed, NoiseData data)
		{
			float[,] noiseMap = new float[sizeX, sizeY];

			for (int y = 0; y < sizeY; y++)
			{
				for (int x = 0; x < sizeX; x++)
				{
					float impact = 1;
					float spread = 1;
					float noiseValue = 0;

					for (int i = 0; i < data.octaves; i++)
					{
						float sampleX = (x + seed) / data.scale * spread;
						float sampleY = (y + seed) / data.scale * spread;

						float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
						noiseValue += perlinValue * impact;

						impact *= data.impact;
						spread *= data.spread;
					}
					noiseMap[x, y] = noiseValue;
				}
			}
			return noiseMap;
		}
		public static float[] Generate1DNoiseMap(int sizeX, int seed, NoiseData data)
		{
			float[] noiseMap = new float[sizeX];

			for (int x = 0; x < sizeX; x++)
			{
				float impact = 1;
				float spread = 1;
				float noiseValue = 0;

				for (int i = 0; i < data.octaves; i++)
				{
					float sampleX = (x + seed) / data.scale * spread;

					float perlinValue = Mathf.PerlinNoise(sampleX, 0f);
					noiseValue += perlinValue * impact;

					impact *= data.impact;
					spread *= data.spread;
				}
				noiseMap[x] = noiseValue;
			}
			return noiseMap;
		}
	}
	public struct NoiseData
	{
		public int octaves;
		public float scale, impact, spread;

		public NoiseData(int _octaves, float _scale, float _impact, float _spread)
		{
			octaves = _octaves;
			scale = _scale;
			impact = _impact;
			spread = _spread;
		}
	}
}