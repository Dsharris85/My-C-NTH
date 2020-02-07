using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
    // Used to hold data for each song. 
    // A song is a queue of notes with their respective "lifetimes" (how long to be played)
    public class SongData
    {
        public int counter;

        // MyTuple used to hold note and lifetime
        public Queue<MyTuple> song;

        // Each row of notes = 1 bar
        private String[] maryNotes = { "C", "X", "Z", "X", "C", "C", "C", "",
                                       "X", "X", "X", "", "C", "B", "B", "",
                                       "C", "X", "Z", "X", "C", "C", "C", "",
                                       "X", "X","C", "X", "Z",
                                     };

        private String[] twinkleNotes = { "Z", "Z", "B", "B", "N", "N", "B", "",
                                          "V", "V", "C", "C", "X", "X", "Z", "",
                                          "B", "B", "V", "V", "C", "C", "X", "",
                                          "B", "B", "V", "V", "C", "C", "X", "",
                                          "Z", "Z", "B", "B", "N", "N", "B", "",
                                          "V", "V", "C", "C", "X", "X", "Z"
                                        };
        
        private String[] cMaj1 = { "ZCB", "XVN", "CBM", "ZVN", "ZVN", "ZCB"};
        private String[] cMaj2 = { "ZCB", "ZCN", "XVN", "XVB", "ZCB" };
        private String[] aMin = { "ZCN", "ZVN", "ZCB", "XBM",
                                  "ZCN", "ZVN", "ZCB", "ZCB"
                                };             
                
        // What song is being used?
        Songs currentSong;

        public Songs CurrentSong
        {
            get => currentSong;
            set
            {
                if (value.Equals(currentSong))
                    return;
                currentSong = value;
            }
        }

        // Construct! fill song with current song data
        public SongData(int life, Songs cSong)
        {
            CurrentSong = cSong;
            song = new Queue<MyTuple>();
            counter = 0;
            MyTuple step;

            switch(CurrentSong)
            {
                case Songs.Mary:                 
                    foreach (String note in maryNotes)
                    {
                        step = new MyTuple(note, life);
                        song.Enqueue(step);
                    }
                    break;

                case Songs.Twinkle:
                    foreach (String note in twinkleNotes)
                    {
                        step = new MyTuple(note, life);
                        song.Enqueue(step);
                    }
                    break;
                case Songs.CMaj1:
                    foreach (String note in cMaj1)
                    {
                        step = new MyTuple(note, life * 4);
                        song.Enqueue(step);
                    }
                    break;

                case Songs.CMaj2:
                    foreach (String note in cMaj2)
                    {
                        step = new MyTuple(note, life * 4);
                        song.Enqueue(step);
                    }
                    break;
                case Songs.AMin:
                    foreach (String note in aMin)
                    {
                        step = new MyTuple(note, life * 4);
                        song.Enqueue(step);
                    }
                    break;
            }
        }

        // Just needed a quick tuple type, but also mutable so...
        public class MyTuple
        {
            public MyTuple(String note, int life)
            {
                Note = note;
                Life = life; 
            }
            // Item1
            public string Note {get;set;}
            // Item2
            public int Life {get;set;}
        }
    }
}
