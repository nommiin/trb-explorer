using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace trb_explorer {
    class TSymbol {
        public Int16 Index;
        public string Name;
        public long Base;

        public TSymbol(BinaryReader _reader, long _origin, Int32 _count) {
            this.Index = _reader.ReadInt16();
            this.Base = _reader.BaseStream.Position + 10;
            _reader.BaseStream.Seek(_origin + (_count * 12), SeekOrigin.Begin);
            this.Name = _reader.ReadStringNT();
            _reader.BaseStream.Seek(this.Base, SeekOrigin.Begin);
            Console.WriteLine(this);
            /*this.Base = _reader.BaseStream.Position + 8;
            _reader.BaseStream.Seek(_origin + (_count * 12) + (this.Index * 12), SeekOrigin.Begin);
            this.Name = _reader.ReadStringNT();
            _reader.BaseStream.Seek(this.Base, SeekOrigin.Begin);
            Console.WriteLine(this);*/
        }

        public override string ToString() {
            return $"Name: {this.Name}, Index: {this.Index}";
        }
    }
}
