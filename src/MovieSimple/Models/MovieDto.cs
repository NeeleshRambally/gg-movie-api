using System.Collections.Generic;

namespace MovieSimple;

public class MovieDto
{
    public int id { get; set; }
    public string title { get; set; }
    public string year { get; set; }
    public List<string> genre { get; set; }
    public string director { get; set; }
    public List<string> actors { get; set; }
    public string rating { get; set; }
}