using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace trb_explorer {
    // Toshi Resource Binary
    class TBinary {
        public string Name;
        public Chunk Header;
        public BinaryReader Reader;
        public List<TFile> Files = new List<TFile>();
        public Dictionary<string, Chunk> Chunks = new Dictionary<string, Chunk>();

        public TBinary(string _path) {
            this.Name = Path.GetFileNameWithoutExtension(_path);
            if (File.Exists(_path) == true) {
                this.Reader = new BinaryReader(File.OpenRead(_path));
                this.Header = new Chunk(this.Reader);
                if (this.Header.Name == "TSFL") {
                    this.Reader.BaseStream.Seek(4, SeekOrigin.Current);
                    while (this.Reader.BaseStream.Position + 8 < this.Header.Base + this.Header.Length) {
                        Chunk _toshiChunk = new Chunk(this.Reader);
                        this.Reader.BaseStream.Seek(_toshiChunk.Length, SeekOrigin.Current);
                        this.Chunks.Add(_toshiChunk.Name, _toshiChunk);
                    }
                } else {
                    throw new Exception("Could not parse file, invalid TSFL header");
                }
            } else {
                throw new FileNotFoundException("Could not find file \"" + _path + "\"");
            }
        }

        public void DumpFiles(string _path) {
            if (Directory.Exists(_path) == false) Directory.CreateDirectory(_path);
            for (int i = 0; i < this.Files.Count; i++) {
                TFile _toshiFile = this.Files[i];
                Console.WriteLine("Symbol: {0}", _toshiFile.Symbol.Name);
                this.Reader.BaseStream.Seek(_toshiFile.Origin + _toshiFile.Offset, SeekOrigin.Begin);
                using (BinaryReader _fileReader = new BinaryReader(new MemoryStream(this.Reader.ReadBytes(_toshiFile.Length)))) {
                    MethodInfo _fileHandler = Assembly.GetExecutingAssembly().GetType("trb_explorer.FileHandler").GetMethod("Handle_" + _toshiFile.Symbol.Name);
                    if (_fileHandler != null) {
                        _fileHandler.Invoke(null, new object[] { this, _toshiFile, _fileReader, _path });
                    } else {
                        FileHandler.Handle_Unknown(this, _toshiFile, _fileReader, _path);
                    }
                }
            }
        }
    }
}
