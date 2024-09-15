using Microsoft.AspNetCore.Components.Routing;

namespace C_;

public class Junior
{

    private readonly int id;

    private readonly string name;

    public Junior(int id, string name) {
        this.id = id;
        this.name = name;
    }

    public Junior(string id, string name) {
        this.id = int.Parse(id);
        this.name = name;
    }

    public int GetId() {
        return id;
    }

    public string GetName() {
        return name;
    }

    public override string ToString() {
        return "{id:" + id + ", name: " + name + "}";
    }

}