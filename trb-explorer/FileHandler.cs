using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace trb_explorer {
    static class FileHandler {
        public static void Handle_Unknown(TBinary _toshi, TFile _file, BinaryReader _reader, string _path) {
            File.WriteAllBytes(_path + "\\" + _toshi.Name + "_asset" + _file.Symbol.Index + "." + _file.Symbol.Name, _reader.ReadBytes(_file.Length));
        }

        public static void Handle_ttex(TBinary _toshi, TFile _file, BinaryReader _reader, string _path) {
            // TODO: Figure out more of the header
            _reader.BaseStream.Seek(48, SeekOrigin.Begin);
            string _ttexName = _reader.ReadStringNT();
            while (true) {
                long _readerOrigin = _reader.BaseStream.Position;
                if (_reader.ReadUInt32() == 542327876) {
                    _reader.BaseStream.Seek(_readerOrigin, SeekOrigin.Begin);
                    break;
                } else _reader.BaseStream.Seek(_readerOrigin + 1, SeekOrigin.Begin);
            }
            File.WriteAllBytes(_path + "\\" + _ttexName, _reader.ReadBytes((int)(_file.Length - _reader.BaseStream.Position)));
        }
    }
}
