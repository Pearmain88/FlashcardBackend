using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Data
{
    public class FlashcardKey
    {

        [Key]
        public int CardID { get; set; }
        public int DeckID { get; set; }
            
        public Guid UserID { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
        

        public virtual DeckData Deck { get; set; }
    }
}
