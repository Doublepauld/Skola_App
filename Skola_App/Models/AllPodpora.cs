using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skola_App.Models
{
    internal class AllPodpora
    {

        public ObservableCollection<Podpora> AllPodpora1 { get; set; } = new ObservableCollection<Podpora>();

        public AllPodpora() =>
            LoadPodpora();

        public void LoadPodpora()
        {
            AllPodpora1.Clear();

            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.notes.txt files.
            IEnumerable<Podpora> podpora = Directory

                                        // Select the file names from the directory
                                        .EnumerateFiles(appDataPath, "*.notes.txt")

                                        // Each file name is used to create a new Note
                                        .Select(filename => new Podpora()
                                        {
                                            Filename = filename,
                                            Jmeno = File.ReadAllText(filename),
                                            Titul = File.ReadAllText(filename)
                                        })

                                        // With the final collection of notes, order them by date
                                        .OrderBy(note => note.Jmeno);

            // Add each note into the ObservableCollection
            foreach (Podpora podpor in podpora)
                AllPodpora1.Add(podpor);
        }

    }
}
