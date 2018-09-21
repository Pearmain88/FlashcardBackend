using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Data
{
    public class DeckData
    {
        [Key]
        public int DeckID { get; set; }
        public Guid UserID { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? ModifyTime { get; set; }
        public DateTime? LastReviewed { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public decimal PercentComplete { get; set; }
    }
}
