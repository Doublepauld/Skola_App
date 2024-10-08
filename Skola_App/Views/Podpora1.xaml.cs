using Skola_App.Models;
using System.Windows.Input;

namespace Skola_App.Views;

public partial class Podpora1 : ContentPage
{
    public ICommand DeletePodporaCommand { get; }
    public Podpora1()
    {
        InitializeComponent();

        BindingContext = new Models.AllPodpora();
        DeletePodporaCommand = new Command<Podpora>(OnDelete);
    }

    private void OnDelete(Podpora podpora)
    {
        string path = Path.Combine(FileSystem.AppDataDirectory, podpora.Filename);
        File.Delete(path);

        ((Models.AllPodpora)BindingContext).LoadPodpora();
    }
    protected override void OnAppearing()
    {
        ((Models.AllPodpora)BindingContext).LoadPodpora();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Modal_podpora));
    }

    private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var note = (Models.Podpora)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(Modal_podpora)}?{nameof(Modal_podpora.ItemId)}={note.Filename}");

            // Unselect the UI
            notesCollection.SelectedItem = null;
        }
    }
}