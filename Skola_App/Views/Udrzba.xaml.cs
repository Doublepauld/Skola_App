namespace Skola_App.Views;

public partial class Udrzba : ContentPage
{
    public Udrzba()
    {
        InitializeComponent();

        BindingContext = new Models.all_udrzba();
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