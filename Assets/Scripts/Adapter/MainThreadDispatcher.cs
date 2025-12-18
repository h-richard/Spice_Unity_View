using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace Adapter {

// sert à passer l'exécution vers le Thread principal d'Unity
// obligatoire pour les opération sur MonoBehaviour
public class MainThreadDispatcher : MonoBehaviour {
    static readonly ConcurrentQueue<Action> Actions = new();

    public static void Enqueue(Action action) {
        Actions.Enqueue(action);
    }

    void Update() {
        while (Actions.TryDequeue(out var action)) action.Invoke();
    }
}
}