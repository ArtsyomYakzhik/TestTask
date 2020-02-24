using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardServer.Models.Interfaces
{
    public interface ICardInteraction<T>
    {
        public void AddCard(T card);

        public void ChangeCard(T newCard, string cardName);

        public void RemoveCard(string cardName);

        public byte[] GetImageBytes(T Card);

        public List<T> GetCards();

    }
}
