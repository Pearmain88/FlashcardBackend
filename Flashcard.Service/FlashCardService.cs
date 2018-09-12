using Flashcard.Data;
using Flashcard.Model;
using FlashcardAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Service
{
    public class FlashcardService
    {
        private readonly Guid _userID;

        public FlashcardService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateFlashCard(FlashcardCreate model)
        {
            var entity =
                new FlashcardKey()
                {
                    UserID = _userID,
                    Term = model.Term,
                    Definition = model.Definition,
                    DeckIndex = model.DeckIndex                    
                };

            var flashcardValue =
                new FlashcardValue()
                {
                    UserID = _userID,
                    CardID = entity.CardID,
                    CreateTime = DateTime.Now,
                    NumberTimesReviewed = 0,
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.FlashcardKeys.Add(entity);
                ctx.FlashcardValues.Add(flashcardValue);

                return ctx.SaveChanges() == 1;
            }
        }


    }
}
