using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

namespace trb_explorer {
    class Program {
        static void Main(string[] args) {
            // Collection of files to read
            List<string> _toshiFiles = new List<string>() {
                //@"D:\Steam\steamapps\common\de Blob\Data\Font\Blob.font.trb",
                @"D:\Steam\steamapps\common\de Blob\Data\LEVELS\PRODUCTION\Singleplayer\Abyss\RegionAssets.trb",
                //@"D:\Steam\steamapps\common\de Blob\Data\credits.trb",
                //@"D:\Steam\steamapps\common\de Blob\Data\Entities\StateData.trb"
            };

            // Loop through files and parse
            for (int i = 0; i < _toshiFiles.Count; i++) {
                TBinary _toshiBinary = new TBinary(_toshiFiles[i]);
                for(int j = 0; j < ChunkHandler.Order.Length; j++) {
                    Chunk _chunkGet = _toshiBinary.Chunks[ChunkHandler.Order[j]];
                    if (_chunkGet != null) {
                        MethodInfo _chunkHandler = Assembly.GetExecutingAssembly().GetType("trb_explorer.ChunkHandler").GetMethod("Handle_" + _chunkGet.Name);
                        if (_chunkHandler != null) {
                            _toshiBinary.Reader.BaseStream.Seek(_chunkGet.Base, SeekOrigin.Begin);
                            _chunkHandler.Invoke(null, new object[] { _toshiBinary, _chunkGet, _toshiBinary.Reader });
                        } else {
                            Console.WriteLine("Could not handle: {0}", _chunkGet.Name);
                        } 
                    }
                }
                _toshiBinary.DumpFiles(@"D:\Steam\steamapps\common\de Blob\Data\Dump");
            }
        }
    }

    static class BinaryReaderExtension {
        public static string ReadStringNT(this BinaryReader _reader) {
            string _ = "";
            byte __ = _reader.ReadByte();
            while (__ != 0x00) {
                _ += (char)__;
                __ = _reader.ReadByte();
            }
            return _;
        }
    }
}
