using Skola_App.Models;
using System.Windows.Input;


namespace Skola_App.Views;

public partial class Ucitele1 : ContentPage
{
    public ICommand DeleteUciteleCommand { get; }
    public Ucitele1()
	{
		InitializeComponent();

        BindingContext = new Models.AllUcitele();
        DeleteUciteleCommand = new Command<Ucitele>(OnDelete);
    }

    private void OnDelete(Ucitele ucit)
    {
        
        string path = Path.Combine(FileSystem.AppDataDirectory, ucit.Filename);
        File.Delete(path);

        ((Models.AllUcitele)BindingContext).LoadNotes();
    }

    protected override void OnAppearing()
    {
        ((Models.AllUcitele)BindingContext).LoadNotes();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Modal));
    }

    private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var note = (Models.Ucitele)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(Modal)}?{nameof(Modal.ItemId)}={note.Filename}");

            // Unselect the UI
            notesCollection.SelectedItem = null;
        }
    }
}