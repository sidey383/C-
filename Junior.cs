using Microsoft.AspNetCore.Components.Routing;

namespace C_;

public record Junior (int id, string name) : Employee
{

    public Junior(string id, string name) : this(int.Parse(id), name) {}

    public int GetId() {
        return id;
    }

    public string GetName() {
        return name;
    }

}