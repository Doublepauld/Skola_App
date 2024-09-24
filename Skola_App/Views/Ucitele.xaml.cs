namespace Skola_App.Views;

public partial class Ucitele : ContentPage
{
	public Ucitele()
	{
		InitializeComponent();

        BindingContext = new Models.AllUcitele();
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