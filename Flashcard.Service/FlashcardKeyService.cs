using Flashcard.Model;
using FlashcardAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Service
{
    public class FlashcardKeyService
    {
        public IEnumerable<FlashcardListItem> GetFlashcardsForReview(Guid userID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .FlashcardKeys
                        .Where(e => e.UserID == userID)
                        .Select(
                            e => new FlashcardListItem
                            {
                                CardID = e.CardID,
                                Term = e.Term,
                                Definition = e.Definition,
                                DeckIndex = e.DeckIndex
                            });
                return query.ToList();
            }
        }
    }
}
