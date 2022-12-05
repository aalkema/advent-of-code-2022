namespace day4.console;

public struct Sections {
    public int Start;
    public int End;
    public int Length;

    public Sections(string sections) {
        string[] boundaries = sections.Split("-");
        Start = Int32.Parse(boundaries[0]);
        End = Int32.Parse(boundaries[1]);
        Length = End - Start;
    }

    public bool Contains(Sections smallerOther) {
        if ( smallerOther.Start >= this.Start && smallerOther.End <= this.End ) {
            return true;
        }
        return false;
    }

    public bool Overlap(Sections other) {
        if ( other.Start < this.Start ) {
            if ( other.Start + other.Length < this.Start ) {
                return false;
            }
        } else {
            if ( this.Start + this.Length < other.Start ) {
                return false;
            }
        }
        return true;
    }

    public override string ToString() {
        return $"{Start}-{End}";
    }
}