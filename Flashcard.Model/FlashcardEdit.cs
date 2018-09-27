using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Model
{
    public class FlashcardEdit
    {
        public int CardID { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
        public int LevelofUnderstanding { get; set; }
    }
}
