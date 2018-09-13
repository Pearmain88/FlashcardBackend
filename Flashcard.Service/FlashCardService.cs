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
        FlashcardKeyService flashcardKeyService;
        FlashcardValueService flashcardValueService;
        private readonly IEnumerable<FlashcardListItem> _flashcardList;
        private readonly IEnumerable<FlashcardValueListItem> _flashcardValues;

        public FlashcardService(Guid userID)
        {
            _userID = userID;
            flashcardKeyService = new FlashcardKeyService(_userID);
            flashcardValueService = new FlashcardValueService(_userID);
            _flashcardList = flashcardKeyService.GetFlashcardsForReview();
            _flashcardValues = flashcardValueService.GetFlashcardsValues();
        }

        public FlashcardListItem[] GetFlashcards()
        {
            var flashcards = _flashcardList.ToArray();

            return flashcards;
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

        public bool EditFlashCard(FlashcardEdit model)
        {
            bool key = flashcardKeyService.UpdateFlashcardKey(model);
            bool value = flashcardValueService.UpdateFlashcardValue(model);

            return (key && value);
        }

        public bool DeleteFlashcard(int id)
        {
            bool value = flashcardValueService.DeleteFlashcardValue(id);
            bool key = flashcardKeyService.DeleteFlashcardKey(id);

            return (key && value);
        }

        public bool CheckID(int id)
        {
            foreach (var item in _flashcardList)
            {
                if (item.CardID == id)
                {
                    return true;
                }
            }
            return false;
        }

        public FlashcardDetails GetFlashcardByID(int id)
        {
            var key = flashcardKeyService.GetFlashcardKeyByID(id);
            var value = flashcardValueService.GetFlashcardValueByID(id);

            var entity = new FlashcardDetails
            {
                CardID = key.CardID,
                Term = key.Term,
                Definition = key.Definition,
                DeckIndex = key.DeckIndex,
                NumberTimesReviewed = value.NumberTimesReviewed,
                CreateTime = value.CreateTime,
                ModifyTime = value.ModifyTime,
                LastReviewed = value.LastReviewed
            };

            return entity;
        }
    }
}
