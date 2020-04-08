using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace trb_explorer {
    static class ChunkHandler {
        public static string[] Order = new string[] { "HDRX", "SYMB" };
        public static void Handle_HDRX(TBinary _toshi, Chunk _chunk, BinaryReader _reader) {
            _reader.BaseStream.Seek(4, SeekOrigin.Current);
            for (int i = 0, _i = _reader.ReadInt32(); i < _i; i++) {
                _toshi.Files.Add(new TFile(_reader, (Int32)_toshi.Chunks["SECT"].Base));
                if (i > 0) {
                    _toshi.Files[i].Offset += _toshi.Files[i - 1].Offset + _toshi.Files[i - 1].Length;
                }
            }
        }

        public static void Handle_SYMB(TBinary _toshi, Chunk _chunk, BinaryReader _reader) {
            List<TSymbol> _chunkSymbols = new List<TSymbol>();
            Int32 _symbolCount = _reader.ReadInt32(), _symbolOffset = ((int)_reader.BaseStream.Position + (_symbolCount * 12));
            for(int i = 0; i < _symbolCount; i++) {
                _chunkSymbols.Add(new TSymbol(_reader, ref _symbolOffset));
                _toshi.Files[i].Symbol = _chunkSymbols[i];
            }
        }
    }
}
