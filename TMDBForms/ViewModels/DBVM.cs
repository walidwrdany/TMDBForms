using System.Collections.Generic;
using TMDbLib.Objects.Collections;
using TMDbLib.Objects.Movies;

namespace TMDBForms.ViewModels
{
    public class DBVM
    {
        public List<Movie> Movies { get; set; }
        public List<Collection> Collections { get; set; }
        public List<Location> Locations { get; set; }
        //changes
        //public List<Location> Locations { get; set; }
    }
}
