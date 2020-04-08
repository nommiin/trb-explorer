using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace trb_explorer {
    class TSymbol {
        public Int16 Index;
        public string Name;
        public long Base;

        public TSymbol(BinaryReader _reader, ref Int32 _offset) {
            this.Base = _reader.BaseStream.Position + 12;
            this.Index = _reader.ReadInt16();
            _reader.BaseStream.Seek(_offset, SeekOrigin.Begin);
            this.Name = _reader.ReadStringNT();
            _reader.BaseStream.Seek(this.Base, SeekOrigin.Begin);
            _offset += this.Name.Length + 1;
        }

        public override string ToString() {
            return $"Name: {this.Name}, Index: {this.Index}";
        }
    }
}
