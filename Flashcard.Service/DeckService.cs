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
    public class DeckService
    {
        private readonly Guid _userID;

        public DeckService(Guid userID)
        {
            _userID = userID;
        }

        public IEnumerable<DeckListItem> GetDecks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Decks
                        .Where(e => e.UserID == _userID)
                        .Select(
                        e => new DeckListItem
                        {
                            DeckID = e.DeckID,
                            Title = e.Title,
                            Description = e.Description,
                            PercentComplete = e.PercentComplete
                        });
                return query.ToList();
            }
        }

        public DeckDetail GetDeckByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Decks
                        .Single(e => e.UserID == _userID && e.DeckID == id);
                        return new DeckDetail
                        {
                            DeckID = entity.DeckID,
                            Title = entity.Title,
                            Description = entity.Description,
                            PercentComplete = entity.PercentComplete,
                            CreateTime = entity.CreateTime,
                            ModifyTime = entity.ModifyTime,
                            LastReviewed = entity.LastReviewed
                        };
            }
        }

        public bool CreateDeck(DeckCreate model)
        {
            var entity =
                new DeckData()
                {
                    Title = model.Title,
                    Description = model.Description,
                    CreateTime = DateTime.Now,
                    PercentComplete = 0,
                    UserID = _userID
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Decks.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool EditDeck(DeckEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Decks
                        .Single(e => e.DeckID == model.DeckID && e.UserID == _userID);
                entity.Title = model.Title;
                entity.Description = model.Description;
                entity.ModifyTime = (DateTime.Now);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool EditDeck(int average, int DeckID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Decks
                        .Single(e => e.DeckID == DeckID && e.UserID == _userID);
                entity.PercentComplete = average;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteDeck(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Decks
                        .Single(e => e.DeckID == id && e.UserID == _userID);
                ctx.Decks.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
