using Microsoft.Maui.Controls;
using SocialMediaApp;
using System.Collections.ObjectModel;

namespace SocialMediaApp
{
    public partial class page_home : ContentPage
    {
        private readonly Random randomizer = new Random();
        private readonly ObservableCollection<string> myItems = new ObservableCollection<string>();

        public page_home()
        {
        //start of work on infinite scroll
            InitializeComponent();

            foreach (var s in GetItems(50))
            {
                myItems.Add(s);
            }

            myCollectionView.ItemsSource = myItems;
            myCollectionView.RemainingItemsThreshold = 5;
            myCollectionView.RemainingItemsThresholdReached += MyCollectionView_RemainingItemsThresholdReached;
        }

        private void MyCollectionView_RemainingItemsThresholdReached(object sender, EventArgs e)
        {
            foreach (var s in GetItems(15))
            {
                myItems.Add(s);
            }
        }

        private List<string> GetItems(int numberOfItems)
        {
            var resultList = new List<string>();

            for (var i = 0; i <= numberOfItems; i++)
            {
                resultList.Add(randomizer.Next(10000, 99999).ToString());
            }

            return resultList;
        }
    }
}
