using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Model
{
    public class FlashcardValueListItem
    {
        public int CardID { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? ModifyTime { get; set; }
        public DateTime? LastReviewed { get; set; }
        public int NumberTimesReviewed { get; set; }
        public int LevelOfUnderstanding { get; set; }
    }
}
