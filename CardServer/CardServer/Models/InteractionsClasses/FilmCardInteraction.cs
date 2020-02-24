using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardServer.Models.Interfaces;
using CardServer.Models.Cards;
using System.IO;
using System.Text.Json;
using System.Net.Http;

namespace CardServer.Models.InteractionsClasses
{
    public class FilmCardInteraction : ICardInteraction<FilmCard>
    {
        private readonly string filePath;

        private List<FilmCard> baseFilmCards;
        public FilmCardInteraction()
        {
            baseFilmCards = new List<FilmCard>
        {
            new FilmCard{FilmName = "The Hateful Eight", PosterPath = Environment.CurrentDirectory + "\\App Data\\The Hateful Eight.jpg"}
        };
            this.filePath ="App Data\\FilmReviewsFile.json";
            if (!File.Exists(filePath))
                CreateJsonFile(this.filePath);
        }
        private void CreateJsonFile(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                JsonSerializer.SerializeAsync<List<FilmCard>>(fileStream, baseFilmCards);
            }
        }
        public void ChangeCard(FilmCard newCard, string cardName)
        {
            List<FilmCard> filmCardsList = GetCards();
            FilmCard changableCard = filmCardsList.Find(x => x.FilmName == cardName);
            filmCardsList.Remove(changableCard);
            RewriteJSONFile(filmCardsList);
            AddCard(newCard);
        }

        public void AddCard(FilmCard card)
        {
            List<FilmCard> filmCardsList = GetCards();
            filmCardsList.Add(card);
            RewriteJSONFile(filmCardsList);
        }

        
        public void RemoveCard(string cardName)
        {
            List<FilmCard> filmCardsList = GetCards();
            FilmCard removableCard = filmCardsList.Find(x => x.FilmName == cardName);
            filmCardsList.Remove(removableCard);
            RewriteJSONFile(filmCardsList);
        }

        public byte[] GetImageBytes(FilmCard filmCard)
        {
            string path = GetCards().Find((x) => filmCard.FilmName == x.FilmName).PosterPath;
            if (path != null)
                return File.ReadAllBytes(path);
            else
                return null;
        }

        public List<FilmCard> GetCards()
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return JsonSerializer.DeserializeAsync<List<FilmCard>>(fileStream).Result;
            }
        }

        private void RewriteJSONFile(List<FilmCard> filmCards)
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize<List<FilmCard>>(filmCards));
        }

    }
}
