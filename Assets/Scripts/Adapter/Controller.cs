using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Adapter {
public class Controller : MonoBehaviour {

    [SerializeField]
    Drone dronePrefab;

    readonly Dictionary<string, Drone> _drones = new();
    
    void ApplyInit(Dictionary<string, Vector3> drones) {
        foreach (var d in _drones) Destroy(d.Value.gameObject);
        _drones.Clear();
        foreach (var d in drones) {
            var drone = Instantiate(dronePrefab, d.Value, Quaternion.identity);
            var droneComponent = drone.GetComponent<Drone>();
            droneComponent.name = d.Key;
            _drones.Add(d.Key, droneComponent);
        }
    }

    void ApplyUpdate(Dictionary<string, Vector3> drones) {
        foreach (var d in drones) {
            var drone = _drones[d.Key];
            drone.MoveTo(d.Value);
        }
    }

    public void HandleMessage(string json) {
        var msg = JsonConvert.DeserializeObject<Message>(json);
        var drones = new Dictionary<string, Vector3>();
        foreach (var d in msg.Content)
            drones.Add(d["name"].ToString(), ParseVector3((JArray)d["position"]));

        switch (msg.Type) {
            case "init": {
                MainThreadDispatcher.Enqueue(() => ApplyInit(drones)); 
                break;
            }
            case "update": {
                MainThreadDispatcher.Enqueue(() => ApplyUpdate(drones));
                break;
            }
        }
           
    }

    Vector3 ParseVector3(JArray array) {
        return new Vector3(
            (float)array[0],
            (float)array[1],
            (float)array[2]
        );
    }
}
}