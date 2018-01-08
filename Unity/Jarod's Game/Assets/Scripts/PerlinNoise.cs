using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour {

    Terrain terr;
    int hmWidth;
    int hmHeight;
    public float perlinHeight = 10;
    public float perlinDivider = 10;

	// Use this for initialization
	void Start () {
        terr = Terrain.activeTerrain;
        hmWidth = terr.terrainData.heightmapWidth;
        hmHeight = terr.terrainData.heightmapHeight;
        terr.heightmapMaximumLOD = 0;

        float[,] heights = terr.terrainData.GetHeights(0, 0, hmWidth, hmHeight);

        // we set each sample of the terrain in the size to the desired height
        for (int i = 0; i < hmWidth; i++)
            for (int j = 0; j < hmHeight; j++)
            {
                heights[i, j] = (Mathf.PerlinNoise(((float)i / (float)terr.terrainData.heightmapWidth) * perlinHeight, ((float)j / (float)terr.terrainData.heightmapHeight) * perlinHeight) / perlinDivider);
            }
        // set the new height
        terr.terrainData.SetHeights(0, 0, heights);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
