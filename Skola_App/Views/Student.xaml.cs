using System.Windows.Input;

namespace Skola_App.Views;

public partial class Student : ContentPage
{
    public ICommand DeleteStudentCommand { get; }
    public Student()
    {
        InitializeComponent();

        BindingContext = new Models.AllStudenti();
        DeleteStudentCommand = new Command<Student>(OnDelete);
    }

    private void OnDelete(Student ucitele)
    {
        string path = Path.Combine(FileSystem.AppDataDirectory, ucitele.Filename);
        File.Delete(path);

        ((Models.AllStudenti)BindingContext).LoadStudenti();
    }

    protected override void OnAppearing()
    {
        ((Models.AllStudenti)BindingContext).LoadStudenti();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Modal_studenti));
    }

    private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var note = (Models.Studenti)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(Modal_studenti)}?{nameof(Modal_studenti.ItemId)}={note.Filename}");

            // Unselect the UI
            notesCollection.SelectedItem = null;
        }
    }
}