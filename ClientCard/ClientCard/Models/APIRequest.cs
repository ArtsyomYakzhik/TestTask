using ClientCard.Models.Cards;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientCard.Models
{
    class APIRequest
    {
        private string uri = "https://localhost:44362/Card";
        private HttpClient httpClient = new HttpClient();
        public async Task<ObservableCollection<FilmCard>> GetFilmCards()
        {
            ObservableCollection<FilmCard> filmCards = new ObservableCollection<FilmCard>();
            HttpResponseMessage response = await httpClient.GetAsync(uri);
            if(response.IsSuccessStatusCode)
            {
                filmCards = JsonSerializer.Deserialize<ObservableCollection<FilmCard>>(response.Content.ReadAsStringAsync().Result);
            }
            return filmCards;
        }

        public async Task<bool> AddFilmCard(FilmCard newFilmCard)
        {
            HttpResponseMessage response = await httpClient.PostAsync(uri+"?filmName=" + newFilmCard.filmName + "&PosterPath=", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveFilmCard(FilmCard removableCard)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(uri + "?filmName=" + removableCard.filmName + "&PosterPath=");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeFilmCard(FilmCard removableCard, string cardName)
        {
            HttpResponseMessage response = await httpClient.PutAsync(uri + "?filmName=" + removableCard.filmName + "&PosterPath=&cardName=" + cardName, null);
            return response.IsSuccessStatusCode;
        }


    }
}
