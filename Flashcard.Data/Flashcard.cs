using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Data
{
    public class Flashcard
    {

        [Key]
        public int CardID { get; set; }
        public Guid UserID { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
        public int DeckIndex { get; set; }


        public Flashcard()
        {
        }

        public Flashcard(string term, string def, int deckindex)
        {

        }
    }
}
