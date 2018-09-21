using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Model
{
    public class DeckDetail
    {
        public int DeckID { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? ModifyTime { get; set; }
        public DateTime? LastReviewed { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public decimal PercentComplete { get; set; }
    }
}
