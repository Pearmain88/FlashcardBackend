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
        DeckService deckService;
        private readonly IEnumerable<FlashcardListItem> _flashcardList;

        public FlashcardService(Guid userID)
        {
            _userID = userID;
            flashcardKeyService = new FlashcardKeyService(_userID);
            flashcardValueService = new FlashcardValueService(_userID);
            deckService = new DeckService(_userID);
            _flashcardList = flashcardKeyService.GetFlashcardsForReview();
        }

        public FlashcardListItem[] GetFlashcards(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .FlashcardKeys
                        .Where(e => e.UserID == _userID)
                        .Select(
                        e => new FlashcardListItem
                        {
                            CardID = e.CardID,
                            Term = e.Term,
                            Definition = e.Definition,
                            LevelOfUnderstanding = e.LevelOfUnderstanding,
                            DeckIndex = e.DeckID
                        });
                return query.OrderBy(o => o.LevelOfUnderstanding).ToArray();
            }
        }

        public bool CreateFlashCard(FlashcardCreate model) 
        {
            var entity =
                new FlashcardKey()
                {
                    UserID = _userID,
                    Term = model.Term,
                    Definition = model.Definition,
                    DeckID = model.DeckID,
                    LevelOfUnderstanding = 0
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

                return ctx.SaveChanges() == 2;
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
            bool key = flashcardKeyService.DeleteFlashcardKey(id);

            return (key);
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

        public FlashcardDetails GetFlashcardByID(int id, int did)
        {
            var key = flashcardKeyService.GetFlashcardKeyByID(id, did);
            var value = flashcardValueService.GetFlashcardValueByID(id);

            var entity = new FlashcardDetails
            {
                CardID = key.CardID,
                Term = key.Term,
                Definition = key.Definition,
                DeckIndex = key.DeckID,
                NumberTimesReviewed = value.NumberTimesReviewed,
                CreateTime = value.CreateTime,
                ModifyTime = value.ModifyTime,
                LastReviewed = value.LastReviewed
            };

            return entity;
        }

        public int GetDeckID(int CardID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .FlashcardKeys
                        .Single(e => e.CardID == CardID);

                return entity.DeckID;
            }
        }

        public bool UpdateDeckPercent(int DeckID)
        {
            var flashcards = GetFlashcards(DeckID);
            int total = 0;
            int average = 0;

            foreach (var item in flashcards)
            {
                total += item.LevelOfUnderstanding;
            }

            if (flashcards.Length != 0)
            {
                average = (total * 100) / (flashcards.Length * 2);
            }

            return deckService.EditDeck(average, DeckID);
        }
    }
}
