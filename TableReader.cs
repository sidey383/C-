using Microsoft.VisualBasic.FileIO;
using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem {

    public class TableReader {
        public TableReader() {}

        public Employee[] ReadEmployee(string file)
        {
            using (TextFieldParser csvReader = new TextFieldParser(file))
            {
                List<Employee> employees = new List<Employee>();
                ApplyCSVFormat(csvReader, file);
                while (!csvReader.EndOfData)
                {
                    string[]? fieldData = csvReader.ReadFields();
                    if (fieldData != null && fieldData.Length == 2)
                    {
                        employees.Add(new Employee(int.Parse(fieldData[0]), fieldData[1]));
                    }
                }
                return employees.ToArray();
            }
        }


        private static void ApplyCSVFormat(TextFieldParser parser, string file)
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(";");
            string[]? colFields = parser.ReadFields();
            if (colFields == null || colFields.Length != 2)
                throw new ArgumentException("Wrong table: '" + file + "'");
            if (!colFields[0].ToLowerInvariant().Equals("id") || !colFields[1].ToLowerInvariant().Equals("name"))
                throw new ArgumentException("Wrong table: '" + file + "'");
        }

    }
}