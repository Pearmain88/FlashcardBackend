using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Data
{
    public class FlashcardEditDetails
    {
        [Key]
        public int CardPropertieID { get; set; }
        public int CardID { get; set; }
        public Guid UserID { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? ModifyTime { get; set; }
        public DateTime LastReviewed { get; set; }
        public int NumberTimesReviewed { get; set; }
        public virtual Flashcard Flashcard { get; set; }
    }
}
