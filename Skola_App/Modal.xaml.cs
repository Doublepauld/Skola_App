namespace Skola_App;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class Modal : ContentPage
{
    string _fileName = Path.Combine(FileSystem.AppDataDirectory, "ucitele.txt");

    public string ItemId
    {
        set { LoadNote(value); }
    }

    public Modal()
    {

        InitializeComponent();


        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.ucitele.txt";

        LoadNote(Path.Combine(appDataPath, randomFileName));


    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        string text = $"{TextEditor1.Text}\n{TextEditor2.Text}"; // Use newline as a delimiter


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
            // Split the content by the delimiter (in this case, newline)
            string[] lines = File.ReadAllLines(fileName);

            if (lines.Length > 0)
                noteModel.Jmeno = lines[0]; // First line is the name

            if (lines.Length > 1)
                noteModel.Titul = lines[1]; // Second line is the specialization
        }

        BindingContext = noteModel;
    }


}