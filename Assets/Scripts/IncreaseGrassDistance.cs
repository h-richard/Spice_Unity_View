using UnityEngine;


public class IncreaseGrassDistance : MonoBehaviour {
    public float distance = 1000f;

    void Update() {
        Terrain activeTerrain = Terrain.activeTerrain;
        if (activeTerrain) activeTerrain.detailObjectDistance = distance;
    }
}