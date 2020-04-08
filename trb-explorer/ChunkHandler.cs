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
                    Console.WriteLine("Updating offset for file {0}", i);
                }
            }
        }

        public static void Handle_SYMB(TBinary _toshi, Chunk _chunk, BinaryReader _reader) {
            List<TSymbol> _chunkSymbols = new List<TSymbol>();
            /*UInt32 _symbolCount = _reader.ReadUInt32(), _symbol
            for(int i = 0, _i = _reader.ReadInt32(); i < _i; i++) {
                _chunkSymbols.Add(new TSymbol(_reader, _chunkBase ));
                Console.WriteLine("Offset: {0}", _reader.BaseStream.Position);
            }*/
            /*for(int i = 0, _i = (int)_reader.BaseStream.Position, __i = _reader.ReadInt32(); i < __i; i++) {
                _chunkSymbols.Add(new TSymbol(_reader, _i, __i));
                if (i < _toshi.Files.Count) {
                    _toshi.Files[i].Symbol = _chunkSymbols[i];
                    #if DEBUG
                    Console.WriteLine(_toshi.Files[i]);
                    #endif
                }
            }*/
        }
    }
}
