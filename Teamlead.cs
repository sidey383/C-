using Microsoft.AspNetCore.Components.Routing;

namespace C_;

public record Teamlead (int id, string name) : Employee
{

    public Teamlead(string id, string name) : this(int.Parse(id), name) {}

    public int GetId() {
        return id;
    }

    public string GetName() {
        return name;
    }

}