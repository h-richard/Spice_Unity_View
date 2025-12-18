using System;
using UnityEngine;
using UnityEngine.Rendering;


public class Drone : MonoBehaviour {

    [SerializeField] bool castShadows = true;
    LineRenderer _path;

    void Awake() {
        var meshRenderers = GetComponentsInChildren<MeshRenderer>();
        if (meshRenderers == null) return;
        foreach (var mr in meshRenderers) {
            mr.shadowCastingMode = castShadows
                ? ShadowCastingMode.On
                : ShadowCastingMode.Off;
            mr.receiveShadows = castShadows;
        }
    }

    public void MoveTo(Vector3 position) {
        if (!_path) _path = LineDrawer.Instance.CreateLine("Path", transform.position, position, Color.cyan, transform);
        else LineDrawer.Instance.AddSegment(_path, position);
        
        transform.LookAt(position);
        transform.position = position;
    }

    void OnDestroy() {
        if (_path) LineDrawer.Instance.DestroyLine(_path);
    }
}