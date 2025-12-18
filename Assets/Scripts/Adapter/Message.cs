using Newtonsoft.Json.Linq;

namespace Adapter {

[System.Serializable]
public struct Message {
    public Message(string type, JArray content) {
        Type = type;
        Content = content;
    }
    public string Type { get; }
    public JArray Content { get; }
}
}