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
        public IHttpActionResult Post(FlashcardCreate flashcard)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateFlashcardService();

            if (!service.CreateFlashCard(flashcard))
                return InternalServerError();

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
