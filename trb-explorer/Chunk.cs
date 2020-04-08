using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace trb_explorer {
    // Standard IFF Chunk
    class Chunk : IEquatable<string> {
        public string Name;
        public Int32 Length;
        public long Base;

        public Chunk(BinaryReader _reader) {
            this.Name = ASCIIEncoding.ASCII.GetString(_reader.ReadBytes(4));
            this.Length = _reader.ReadInt32();
            this.Base = _reader.BaseStream.Position;
        }

        public bool Equals(string _name) {
            return (_name == this.Name);
        }

        public override string ToString() {
            return $"Name: {this.Name}, Length: {this.Length} bytes";
        }
    }
}
