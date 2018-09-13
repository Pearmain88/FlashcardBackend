using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Model
{
<<<<<<< HEAD
    class FlashcardDetails
    {
        public int CardID { get; set; }
        public Guid UserID { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
        public int DeckIndex { get; set; }
=======
    public class FlashcardDetails
    {
        public int CardID { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
        public int DeckIndex { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime? ModifyTime { get; set; }
        public DateTime? LastReviewed { get; set; }
        public int NumberTimesReviewed { get; set; }
>>>>>>> 09ec52e145ab8ed2bbfc670ee493859b9b95614c
    }
}
