using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libzstdn.Common
{
    public static class Constants
    {
        public static readonly int SIZE_OF_BYTE = 1;
        public static readonly int SIZE_OF_SHORT = 2;
        public static readonly int SIZE_OF_INT = 4;
        public static readonly int SIZE_OF_LONG = 8;

        public static readonly uint MAGIC_NUMBER = 0xFD2FB528;

        public static readonly int MIN_WINDOW_LOG = 10;
        public static readonly int MAX_WINDOW_LOG = 31;

        public static readonly int SIZE_OF_BLOCK_HEADER = 3;

        public static readonly int MIN_SEQUENCES_SIZE = 1;
        public static readonly int MIN_BLOCK_SIZE = 1 // block type tag
                + 1 // min size of raw or rle length header
                + MIN_SEQUENCES_SIZE;
        public static readonly int MAX_BLOCK_SIZE = 128 * 1024;

        public static readonly int REPEATED_OFFSET_COUNT = 3;

        // block types
        public const int RAW_BLOCK = 0;
        public const int RLE_BLOCK = 1;
        public const int COMPRESSED_BLOCK = 2;

        // sequence encoding types
        public const int SEQUENCE_ENCODING_BASIC = 0;
        public const int SEQUENCE_ENCODING_RLE = 1;
        public const int SEQUENCE_ENCODING_COMPRESSED = 2;
        public const int SEQUENCE_ENCODING_REPEAT = 3;

        public static readonly int MAX_LITERALS_LENGTH_SYMBOL = 35;
        public static readonly int MAX_MATCH_LENGTH_SYMBOL = 52;
        public static readonly int MAX_OFFSET_CODE_SYMBOL = 31;
        public static readonly int DEFAULT_MAX_OFFSET_CODE_SYMBOL = 28;

        public static readonly int LITERAL_LENGTH_TABLE_LOG = 9;
        public static readonly int MATCH_LENGTH_TABLE_LOG = 9;
        public static readonly int OFFSET_TABLE_LOG = 8;

        // literal block types
        public const int RAW_LITERALS_BLOCK = 0;
        public const int RLE_LITERALS_BLOCK = 1;
        public const int COMPRESSED_LITERALS_BLOCK = 2;
        public const int TREELESS_LITERALS_BLOCK = 3;

        public static readonly int LONG_NUMBER_OF_SEQUENCES = 0x7F00;

        public static readonly int[] LITERALS_LENGTH_BITS = { 0, 0, 0, 0, 0, 0, 0, 0,
                                                        0, 0, 0, 0, 0, 0, 0, 0,
                                                        1, 1, 1, 1, 2, 2, 3, 3,
                                                        4, 6, 7, 8, 9, 10, 11, 12,
                                                        13, 14, 15, 16 };

        public static readonly int[] MATCH_LENGTH_BITS = { 0, 0, 0, 0, 0, 0, 0, 0,
                                                    0, 0, 0, 0, 0, 0, 0, 0,
                                                    0, 0, 0, 0, 0, 0, 0, 0,
                                                    0, 0, 0, 0, 0, 0, 0, 0,
                                                    1, 1, 1, 1, 2, 2, 3, 3,
                                                    4, 4, 5, 7, 8, 9, 10, 11,
                                                    12, 13, 14, 15, 16 };

    }
}
