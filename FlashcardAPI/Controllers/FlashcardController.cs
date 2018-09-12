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
    public class FlashcardController : ApiController
    {
        public IHttpActionResult GetAll()
        {
            var service = CreateFlashcardService();
            var flashcards = service.GetFlashcards();
            return Ok(flashcards);
        }

        public IHttpActionResult Get(int id)
        {
            return Ok();
        }

        public IHttpActionResult Post(FlashcardCreate flashcard)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateFlashcardService();

            if (!service.CreateFlashCard(flashcard))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Put(FlashcardEdit flashcard)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
​
            var service = CreateFlashcardService();
​
            if (!service.UpdateNote(flashcard))
                return InternalServerError();
​
            return Ok();
        }
​
            public IHttpActionResult Delete(int id)
        {
            return Ok();
        }

        private FlashcardService CreateFlashcardService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var flashcardService = new FlashcardService(userId);
            return flashcardService;
        }
    }
}
