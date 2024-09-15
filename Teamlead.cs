using Microsoft.AspNetCore.Components.Routing;

namespace C_;

public class Teamlead 
{

    private readonly int id;

    private readonly string name;

    public Teamlead(int id, string name) {
        this.id = id;
        this.name = name;
    }

    public Teamlead(string id, string name) {
        this.id = int.Parse(id);
        this.name = name;
    }

    public int getId() {
        return id;
    }

    public string getName() {
        return name;
    }

}