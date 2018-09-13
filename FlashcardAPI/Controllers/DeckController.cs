using Flashcard.Model;
using Flashcard.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlashcardAPI.Controllers
{
    [Authorize]
    public class DeckController : ApiController
    {
        public IHttpActionResult GetAll()
        {
            var service = CreateDeckService();
            var decks = service.GetDecks();
            return Ok(decks);
        }

        public IHttpActionResult Post(DeckCreate deck)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateDeckService();

            if (!service.CreateDeck(deck))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Put(DeckEdit deck)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateDeckService();

            if (!service.EditDeck(deck))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateDeckService();
            if (!service.DeleteDeck(id))
                return InternalServerError();

            return Ok();
        }

        private DeckService CreateDeckService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var deckService = new DeckService(userId);
            return deckService;
        }
    }
}
