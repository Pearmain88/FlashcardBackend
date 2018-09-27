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
    public class FlashcardKeyService
    {
        private readonly Guid userID;

        public FlashcardKeyService(Guid userId)
        {
            userID = userId;
        }

        public IEnumerable<FlashcardListItem> GetFlashcardsForReview()
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
                                DeckIndex = e.DeckID
                            });
                return query.ToList();
            }
        }

        public bool UpdateFlashcardKey(FlashcardEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .FlashcardKeys
                        .Single(e => e.CardID == model.CardID && e.UserID == userID);
                entity.Term = model.Term;
                entity.Definition = model.Definition;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteFlashcardKey(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .FlashcardKeys
                        .Single(e => e.CardID == id && e.UserID == userID);
                ctx.FlashcardKeys.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAllFlashcardKeyFromDeck(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .FlashcardKeys
                        .Where(e => e.DeckID == id && e.UserID == userID);

                foreach (var item in query)
                {
                    ctx.FlashcardKeys.Remove(item);
                }

                return ctx.SaveChanges() == 1;
            }
        }

        public FlashcardKey GetFlashcardKeyByID(int id, int did)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .FlashcardKeys
                        .Single(e => e.UserID == userID && e.CardID == id && e.DeckID == did);

                return entity;
            }
        }
    }
}
