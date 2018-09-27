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
        public IHttpActionResult GetAll(int did)
        {
            var service = CreateFlashcardService();
            var flashcards = service.GetFlashcards(did);
            return Ok(flashcards);
        }

        public IHttpActionResult Get(int id, int did)
        {
            var service = CreateFlashcardService();
            if (!service.CheckID(id))
                return InternalServerError();
            
            var model = service.GetFlashcardByID(id, did);
            return Ok(model);
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
            
            var service = CreateFlashcardService();
            
            if (!service.EditFlashCard(flashcard))
                return InternalServerError();
            
            return Ok();
        }
        
        public IHttpActionResult Delete(int id)
        {
            var service = CreateFlashcardService();
            if (!service.DeleteFlashcard(id))
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
