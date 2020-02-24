using ClientCard.Models;
using ClientCard.Models.Cards;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientCard.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private APIRequest apiRequest = new APIRequest();
        private ObservableCollection<FilmCard> filmCards=new ObservableCollection<FilmCard>();
        public ObservableCollection<FilmCard> FilmCards { get { return filmCards; } set { filmCards = value; OnPropertyChanged(); } }


        private string imagePath;
        public string ImagePath { get { return imagePath; } set { imagePath = value; OnPropertyChanged(); } }


        private string selectedCard;
        public string SelectedCard { get { return selectedCard; } set { selectedCard = value; OnPropertyChanged(); } }

        private RelayCommand getFilmCards;
        public RelayCommand GetFilmCards
        {
            get {
                return getFilmCards ??
                  (getFilmCards = new RelayCommand(obj =>
                  {
                      RefreshCards();
                  },(obj)=>true));
                }
        }

        private RelayCommand addFilmCard;
        public RelayCommand AddFilmCard
        {
            get
            {
                return addFilmCard ??
                  (addFilmCard = new RelayCommand(obj =>
                  {
                      apiRequest.AddFilmCard(new FilmCard { filmName = obj.ToString(), posterPath = ImagePath });
                      RefreshCards();
                  }, (obj) => true));
            }
        }

        private RelayCommand removeFilmCard;
        public RelayCommand RemoveFilmCard
        {
            get
            {
                return removeFilmCard ??
                  (removeFilmCard = new RelayCommand(obj =>
                  {
                      apiRequest.RemoveFilmCard(new FilmCard { filmName = SelectedCard });
                      RefreshCards();
                  }, (obj) => !(SelectedCard is null)));
            }
        }

        private RelayCommand changeFilmCard;
        public RelayCommand ChangeFilmCard
        {
            get
            {
                return changeFilmCard ??
                  (changeFilmCard = new RelayCommand(obj =>
                  {
                      apiRequest.ChangeFilmCard(new FilmCard { filmName = obj.ToString(), posterPath=ImagePath }, SelectedCard);
                      RefreshCards();
                  }, (obj) => !(SelectedCard is null)));
            }
        }


        public MainViewModel()
        {
            RefreshCards();
            OnPropertyChanged("FilmCards");
        }

        private async void RefreshCards()
        {
            await Task.Delay(300);
            FilmCards = await apiRequest.GetFilmCards();
            OnPropertyChanged();
        }

        public void ChooseImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog.Filter = "jpeg (*.jpg)|*.jpg";

            if (openFileDialog.ShowDialog().Value)
                 imagePath = openFileDialog.FileName;
        }

        private RelayCommand chooseImage;
        public RelayCommand ChooseImag
        {
            get
            {
                return chooseImage ??
                  (chooseImage = new RelayCommand(obj =>
                  {
                      ChooseImage();
                  }));
            }
        }


        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
