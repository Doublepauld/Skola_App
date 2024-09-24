using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skola_App.Models
{
    internal class AllUcitele
    {
        public ObservableCollection<Ucitele> AllUcitele1 { get; set; } = new ObservableCollection<Ucitele>();

        public AllUcitele() =>
            LoadNotes();

        public void LoadNotes()
        {
            AllUcitele1.Clear();

            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.notes.txt files.
            IEnumerable<Ucitele> ucitele = Directory

                                        // Select the file names from the directory
                                        .EnumerateFiles(appDataPath, "*.notes.txt")

                                        // Each file name is used to create a new Note
                                        .Select(filename => new Ucitele()
                                        {
                                            Filename = filename,
                                            Jmeno = File.ReadAllText(filename),
                                            Titul = File.ReadAllText(filename)
                                        })

                                        // With the final collection of notes, order them by date
                                        .OrderBy(note => note.Jmeno);

            // Add each note into the ObservableCollection
            foreach (Ucitele ucitel in ucitele)
                AllUcitele1.Add(ucitel);
        }
    }
}
