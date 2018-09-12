using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Model
{
    public class FlashcardCreate
    {
        public string Term { get; set; }
        public string Definition { get; set; }
        public int DeckIndex { get; set; }
    }
}
