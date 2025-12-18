using UnityEngine;
using System.Collections.Generic;

public class LineDrawer : MonoBehaviour {
    public static LineDrawer Instance;
    
    const float LineWidth = 0.1f;

    readonly List<LineRenderer> _activeLines = new();

    // singleton
    void Awake() {
        if (!Instance) Instance = this;
        else Destroy(gameObject);
    }

    // crée un nouveau tracé 'lineName' entre deux points 'start' et 'end'
    // 'parent' représente l'objet parent de ce tracé dans la hiérarchie
    public LineRenderer CreateLine(string lineName, Vector3 start, Vector3 end, Color color, Transform parent) {
        var go = new GameObject(lineName);
        if (parent) go.transform.SetParent(parent);
        var lr = go.AddComponent<LineRenderer>();

        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = LineWidth;
        lr.endWidth = LineWidth;
        lr.useWorldSpace = true;
        

        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        _activeLines.Add(lr);
        return lr;
    }

    // ajoute un segment à un tracé 'line' qui relie son dernier point à un nouveau à 'point'
    public void AddSegment(LineRenderer line, Vector3 point) {
        line.positionCount++;
        line.SetPosition(line.positionCount - 1, point);
    }

    public void DestroyLine(LineRenderer line) {
        if (_activeLines.Contains(line)) {
            _activeLines.Remove(line);
            Destroy(line.gameObject);
        }
    }

    public void ClearAllLines() {
        foreach (var line in _activeLines)
            Destroy(line);

        _activeLines.Clear();
    }
    
    public void SetLineColor(LineRenderer line, Color color) {
        line.startColor = color;
        line.endColor = color;
    }
}