namespace Skola_App;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class Modal : ContentPage
{
    string _fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");

    public string ItemId
    {
        set { LoadNote(value); }
    }

    public Modal()
    {

        InitializeComponent();


        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

        LoadNote(Path.Combine(appDataPath, randomFileName));


    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        string text = TextEditor1.Text + TextEditor2.Text;


        if (BindingContext is Models.Ucitele note)
            File.WriteAllText(note.Filename, text);

        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Ucitele note)
        {
            // Delete the file.
            if (File.Exists(note.Filename))
                File.Delete(note.Filename);
        }

        await Shell.Current.GoToAsync("..");
    }

    private void LoadNote(string fileName)
    {
        Models.Ucitele noteModel = new Models.Ucitele();
        noteModel.Filename = fileName;

        if (File.Exists(fileName))
        {
            noteModel.Jmeno = File.ReadAllText(fileName);
            noteModel.Titul = File.ReadAllText(fileName);
        }

        BindingContext = noteModel;
    }


}