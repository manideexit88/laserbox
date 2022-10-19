using System.Collections.Generic;

namespace BoxMirror.Service.Models
{
    public class Room
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public string? Direction { get; set; }
        public string? Orientation { get; set; }
        public Mirror Mirror { get; set; }
    }

    public class BoxInput
    {
        public int Columns { get; set; }
        public int Rows { get; set; }
        public List<Mirror> Mirrors { get; set; }
        public Room Entry { get; set; }
    }

    public class Mirror
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public string Leaning { get; set; }
        public string Reflection { get; set; }
    }
}
