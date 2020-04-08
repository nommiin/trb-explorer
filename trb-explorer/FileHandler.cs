using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace trb_explorer {
    static class FileHandler {
        public static void Handle_Unknown(TBinary _toshi, TFile _file, BinaryReader _reader, string _path) {
            
        }

        public static void Handle_ttex(TBinary _toshi, TFile _file, BinaryReader _reader, string _path) {
            // TODO: Figure out more of the header
            _reader.BaseStream.Seek(48, SeekOrigin.Begin);
            string _ttexName = _reader.ReadStringNT();
            _reader.BaseStream.Seek(27, SeekOrigin.Current);
            File.WriteAllBytes(_path + "\\" + _ttexName, _reader.ReadBytes((int)(_file.Length - _reader.BaseStream.Position)));
        }
    }
}
