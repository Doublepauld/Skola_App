using Skola_App.Models;
using System.Windows.Input;

namespace Skola_App.Views;

public partial class Udrzba : ContentPage
{
    public Udrzba()
    {
        InitializeComponent();

        BindingContext = new Models.all_udrzba();
        DeleteCommand = new Command<Udrzbari>(OnDelete);
    }

    public ICommand DeleteCommand { get; }


    private void OnDelete(Udrzbari udrzbari)
    {
        if (udrzbari != null)
        {
            // Remove the item from your collection
            var udrzbariList = (BindingContext as all_udrzba)?.all_udrzbari;
            if (udrzbariList != null)
            {
                udrzbariList.Remove(udrzbari);

                // If you need to delete the associated file as well:
                if (File.Exists(udrzbari.Filename))
                {
                    File.Delete(udrzbari.Filename);
                }
            }
        }
    }
    protected override void OnAppearing()
    {
        ((Models.all_udrzba)BindingContext).LoadUdrzbari();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Modal_udrzbar));
    }

    private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var note = (Models.Udrzbari)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(Modal_udrzbar)}?{nameof(Modal_udrzbar.ItemId)}={note.Filename}");

            // Unselect the UI
            notesCollection.SelectedItem = null;
        }
    }
}