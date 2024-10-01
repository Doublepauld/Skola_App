using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skola_App.Models
{
    internal class all_udrzba
    {
        public ObservableCollection<Udrzbari> all_udrzbari { get; set; } = new ObservableCollection<Udrzbari>();

        public all_udrzba() =>
            LoadUdrzbari();

        public void LoadUdrzbari()
        {
            all_udrzbari.Clear();

            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.notes.txt files.
            IEnumerable<Udrzbari> udrzbari = Directory

                                        // Select the file names from the directory
                                        .EnumerateFiles(appDataPath, "*.notes.txt")

                                        // Each file name is used to create a new Note
                                        .Select(filename => new Udrzbari()
                                        {
                                            Filename = filename,
                                            Jmeno = File.ReadAllText(filename),
                                            Specializace = File.ReadAllText(filename)
                                        })

                                        // With the final collection of notes, order them by date
                                        .OrderBy(note => note.Jmeno);

            // Add each note into the ObservableCollection
            foreach (Udrzbari udrzbar in udrzbari)
                all_udrzbari.Add(udrzbar);
        }
    }
}
