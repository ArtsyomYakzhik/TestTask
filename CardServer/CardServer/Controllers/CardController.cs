using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CardServer.Models.Interfaces;
using CardServer.Models.Cards;
using static System.Net.Mime.MediaTypeNames;

namespace CardServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController:ControllerBase 
    {
        ICardInteraction<FilmCard> cardInteraction;

        public CardController(ICardInteraction<FilmCard> cardInteraction)
        {
            this.cardInteraction = cardInteraction;
        }

        [HttpGet]
        public List<FilmCard> GetFilmCards()
        {
            return cardInteraction.GetCards();
        }

        [HttpGet]
        [Route("GetImage")]
        public ActionResult GetImage([FromQuery]FilmCard filmCard)
        {
            return File(cardInteraction.GetImageBytes(filmCard), "image/jpeg");
        }

        [HttpPost]
        public ActionResult AddCard([FromQuery]FilmCard newCard, byte[] jpgBytes)
        {
            cardInteraction.AddCard(newCard);
            return StatusCode(201);
        }

        [HttpPut]
        public ActionResult ChangeCard([FromQuery]FilmCard newCard, [FromQuery]string cardName)
        {
            cardInteraction.ChangeCard(newCard, cardName);
            return StatusCode(203);
        }

        [HttpDelete]
        public ActionResult RemoveCard([FromQuery]FilmCard removableCard)
        {
            cardInteraction.RemoveCard(removableCard.FilmName);
            return StatusCode(204);
        }
    }
}
