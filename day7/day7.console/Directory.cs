namespace day7.console;

public class Directory {
    public string Name;
    public Dictionary<string, AdventFile> Files;
    public Dictionary<string, Directory> Directories;
    private Directory parentDirectory;
    public int size;

    public Directory(string name, Directory parentDirectory) {
        this.Name = name;
        Files = new Dictionary<string, AdventFile>();
        Directories = new Dictionary<string, Directory>();
        this.parentDirectory = parentDirectory;
    }

    public void AddFile(AdventFile file) {
        Files.Add(file.Name, file);
        AdjustSize(file.Size);
    }

    internal void AdjustSize(int size) {
        this.size += size;
        if (parentDirectory != null) {
            parentDirectory.AdjustSize(size);
        }
    }

    public void AddDirectory(Directory directory) {
        Directories.Add(directory.Name, directory);
    }

    public Directory GetSubDirectory(string name) {
        return Directories[name];
    }

    public Directory GetParentDirectory() {
        return this.parentDirectory;
    }
}