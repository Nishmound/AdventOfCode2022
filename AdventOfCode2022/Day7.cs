internal class Day7 : AdventDay<int>
{
    public int run_p1(StreamReader reader)
    {
        Folder root = readFileTree(reader);

        return root.getSizeCap100000();
    }

    public int run_p2(StreamReader reader)
    {
        Folder root = readFileTree(reader);
        int diff = 30000000 - (70000000 - root.Size);

        return root.getSizeOfSmallest(diff);
    }

    static Folder readFileTree(StreamReader reader)
    {
        reader.ReadLine();
        Folder root = new("/");
        Stack<Folder> stack = new Stack<Folder>();
        stack.Push(root);

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] command = line.Split(' ', '.');

            if (command[0] == "$")
            {
                if (command[1] == "ls") continue;
                if (command[2] == string.Empty)
                {
                    stack.Pop();
                    continue;
                }
                Folder? subFolder = stack.Peek().SubFolders.Find(folder => folder.Name == command[2]);
                if (subFolder != null) stack.Push(subFolder);
                continue;
            }

            if (command[0] == "dir")
            {
                stack.Peek().SubFolders.Add(new(command[1]));
                continue;
            }

            stack.Peek().Files.Add(command.Length == 3 ?
                new(command[1], command[2], int.Parse(command[0])) :
                new(command[1], string.Empty, int.Parse(command[0]))
                );
        }

        return root;
    }

    class File
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public string FullName
        {
            get { return string.IsNullOrEmpty(Extension) ? Name : Name + "." + Extension; }
        }
        public int Size { get; set; }

        public File(string name, string extension, int size)
        {
            Name = name;
            Extension = extension;
            Size = size;
        }
    }

    class Folder
    {
        public string Name { get; set; }
        public List<Folder> SubFolders { get; set; }
        public List<File> Files { get; set; }
        public int Size
        {
            get
            {
                var size = 0;
                Files.ForEach(file => { size += file.Size; });
                SubFolders.ForEach(folder => { size += folder.Size; });
                return size;
            }
        }

        public Folder(string name)
        {
            Name = name;
            SubFolders = new();
            Files = new();
        }

        public int getSizeCap100000()
        {
            int size = 0;
            if (Size <= 100000) size += Size;
            SubFolders.ForEach(folder => { size += folder.getSizeCap100000(); });
            return size;
        }

        internal int getSizeOfSmallest(int min)
        {
            var folders = SubFolders.Where(folder => folder.getSizeOfSmallest(min) >= min);
            if (!folders.Any()) return Size;
            return folders.Min(folder => folder.getSizeOfSmallest(min));
        }
    }
}