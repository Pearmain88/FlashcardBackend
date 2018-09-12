using Flashcard.Model;
using FlashcardAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Service
{
    public class FlashcardValueService
    {
        private readonly Guid userID;

        public FlashcardValueService(Guid userId)
        {
            userID = userId;
        }

        public IEnumerable<FlashcardValueListItem> GetFlashcardsValues()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .FlashcardValues
                        .Where(e => e.UserID == userID)
                        .Select(
                            e => new FlashcardValueListItem
                            {
                                CardID = e.CardID,
                                CreateTime = e.CreateTime,
                                ModifyTime = e.ModifyTime,
                                LastReviewed = e.LastReviewed,
                                NumberTimesReviewed = e.NumberTimesReviewed
                            });
                return query.ToList();
            }
        }

        public bool UpdateFlashcardValue(FlashcardEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .FlashcardValues
                        .Single(e => e.CardID == model.CardID && e.UserID == userID);
                entity.ModifyTime = DateTime.Now;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteFlashcardValue(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .FlashcardValues
                        .Single(e => e.CardID == id && e.UserID == userID);
                ctx.FlashcardValues.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
