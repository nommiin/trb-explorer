using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace trb_explorer {
    class TFile {
        public long Offset;
        public Int32 Length;
        public Int32 Origin;
        public TSymbol Symbol;

        public TFile(BinaryReader _reader, Int32 _offset) {
            this.Origin = _offset;
            _reader.BaseStream.Seek(4, SeekOrigin.Current);
            this.Length = _reader.ReadInt32();
            _reader.BaseStream.Seek(8, SeekOrigin.Current);
        }

        public override string ToString() {
            return $"Length: {this.Length} bytes, Offset: {this.Offset}, Symbol: [{this.Symbol}]";
        }
    }
}
