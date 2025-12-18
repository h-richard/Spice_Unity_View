using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Adapter {
public class Linker : MonoBehaviour {

    [SerializeField]
    int port;

    Controller _controller;
    TcpListener _server;
    Thread _serverThread;
    bool _running = true;

    void Start() {
        _controller = GetComponent<Controller>();
        _serverThread = new Thread(StartServer);
        _serverThread.Start();
    }

    void StartServer() {
        _server = new TcpListener(IPAddress.Any, port);
        _server.Start();
        Thread.Sleep(50);
        Debug.Log("Serveur ouvert.");

        while (_running) {
            TcpClient client = null;

            try {
                client = _server.AcceptTcpClient();
                Debug.Log("Client connecté");
                using var reader = new StreamReader(client.GetStream());
                
                while (reader.ReadLine() is { } message) // check si null sinon met dans message
                    _controller.HandleMessage(message);
            }
            catch (IOException ioe) {
                Debug.LogWarning("[Erreur] " + ioe.Message);
            }
            catch (SocketException se) {
                Debug.LogWarning("[Erreur] " + se.Message);
            }
            finally {
                Debug.Log("Client déconnecté.");
                client?.Close();
            }
        }
    }

    void OnApplicationQuit() {
        _running = false;
        _server?.Stop();
        _serverThread?.Join();
    }
}
}