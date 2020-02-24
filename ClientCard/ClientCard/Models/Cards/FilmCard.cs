using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace ClientCard.Models.Cards
{
    class FilmCard
    {
        public string filmName { get; set; }
        public string posterPath { get; set; }

        public override string ToString()
        {
            return filmName;
        }
    }
}
