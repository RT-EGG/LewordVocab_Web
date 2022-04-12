using System;

namespace LewordVocab.Shared
{
    public enum LetterState
    {
        Unknown,
        Absent,
        Present,
        Correct
    }

    public static class LetterStateConversion
    {
        public static string StateString(this LetterState inState) => inState switch {
            LetterState.Unknown => "unknown",
            LetterState.Absent => "absent",
            LetterState.Present => "present",
            LetterState.Correct => "correct",
            _ => throw new ArgumentException()
        };
    }
}
