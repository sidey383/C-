
using C_;
using Microsoft.VisualBasic.FileIO;

public class TableReader
{

    private readonly List<Junior> juniors = new List<Junior>();

    private readonly List<Teamlead> teamleads = new List<Teamlead>();

    public TableReader() { }

    public void ReadJuniors(string file)
    {
        using (TextFieldParser csvReader = new TextFieldParser(file))
        {
            ApplyCSVFormat(csvReader, file);
            while (!csvReader.EndOfData)
            {
                string[]? fieldData = csvReader.ReadFields();
                if (fieldData != null && fieldData.Length == 2)
                {
                    juniors.Add(new Junior(fieldData[0], fieldData[1]));
                }
            }
        }
    }

    public void ReadTeamleads(string file)
    {
        using (TextFieldParser csvReader = new TextFieldParser(file))
        {
            ApplyCSVFormat(csvReader, file);
            while (!csvReader.EndOfData)
            {
                string[]? fieldData = csvReader.ReadFields();
                if (fieldData != null && fieldData.Length == 2)
                {
                    teamleads.Add(new Teamlead(fieldData[0], fieldData[1]));
                }
            }
        }
    }


    private static void ApplyCSVFormat(TextFieldParser parser, string file)
    {
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(";");
        string[]? colFields = parser.ReadFields();
        if (colFields == null || colFields.Length != 2)
            throw new ArgumentException("Wrong table: '" + file + "'");
        if (!colFields[0].ToLowerInvariant().Equals("id")
        || !colFields[1].ToLowerInvariant().Equals("name"))
            throw new ArgumentException("Wrong table: '" + file + "'");
    }


    public List<Junior> GetJuniors() {
        return juniors;
    } 

    public List<Teamlead> GetTeamleads() {
        return teamleads;
    }

}