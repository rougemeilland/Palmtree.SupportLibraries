using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Palmtree.IO
{
    partial class DirectoryPath
    {
        [SupportedOSPlatform("windows")]
        private static partial class InterOpForWindows
        {
            public const UInt32 MAX_PREFERRED_LENGTH = 0xFFFFFFFFu;

            public enum NERR
            {
                NERR_Success = 0,
                ERROR_ACCESS_DENIED = 5,
                ERROR_NOT_ENOUGH_MEMORY = 8,
                ERROR_NOT_SUPPORTED = 50,
                ERROR_DUP_NAME = 52,
                ERROR_INVALID_PARAMETER = 87,
                ERROR_INVALID_LEVEL = 124,
                ERROR_MORE_DATA = 234,
                ERROR_INVALID_DOMAINNAME = 1212,
                NERR_ServerNotStarted = 2124,
                NERR_RemoteErr = 2127,
                NERR_WkstaNotStarted = 2138,
                NERR_ServiceNotInstalled = 2184,
                ERROR_NO_BROWSER_SERVERS_FOUND = 6118,
            }

            public enum SharedDataInformationLevel
                : UInt32
            {
                /// <summary>
                /// 共有の名前を取得します。
                /// 関数から制御が返ると、 bufptr パラメータが指すバッファに、複数の 構造体からなる 1 つの配列が格納されます。
                /// </summary>
                Level0 = 0,

                /// <summary>
                /// リソースの名前、タイプ、リソースに関連付けられているコメントなど、共有リソースに関する情報を取得します。
                /// 関数から制御が返ると、bufptr パラメータが指すバッファに、複数の 構造体からなる 1 つの配列が格納されます。
                /// </summary>
                Level1 = 1,

                /// <summary>
                /// リソースの名前、タイプ、アクセス許可、パスワード、接続の数など、共有リソースに関する情報を取得します。
                /// 関数から制御が返ると、bufptr パラメータが指すバッファに、複数の 構造体からなる 1 つの配列が格納されます。
                /// </summary>
                Level2 = 2,

                /// <summary>
                /// リソースの名前、種類とアクセス許可、接続の数、その他の関連情報など、共有リソースに関する情報を返します。
                /// bufptr パラメーターは、SHARE_INFO_502構造体の配列を指します。
                /// 異なるスコープの共有は返されません。
                /// スコープの詳細については、<see href="https://learn.microsoft.com/ja-jp/windows/win32/api/lmserver/nf-lmserver-netservertransportaddex#remarks">NetServerTransportAddEx 関数のドキュメントの「解説」セクション</see> を参照してください
                /// </summary>
                Level502 = 502,

                /// <summary>
                /// リソースの名前、種類とアクセス許可、接続の数、その他の関連情報など、共有リソースに関する情報を返します。 bufptr パラメーターは、SHARE_INFO_503構造体の配列を指します。 すべてのスコープからの共有が返されます。 この構造体の shi503_servername メンバーが "*" の場合、構成されたサーバー名はなく、 NetShareEnum 関数はスコープなしのすべての名前の共有を列挙します。
                /// <para>
                /// Windows Server 2003 および Windows XP: この情報レベルはサポートされていません。
                /// </para>
                /// </summary>
                Level503 = 503,
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public struct SHARE_INFO_0
            {
                public String NetName;
            }

            /// <summary>
            /// サーバー上の各共有リソースに関する情報を取得します。
            /// </summary>
            /// <param name="serverName">
            /// 関数を実行するリモート サーバーの DNS または NetBIOS 名を指定する文字列へのポインター。
            /// このパラメーターが null の場合は、ローカル コンピューターが使用されます。
            /// </param>
            /// <param name="level">
            /// データの情報レベルを指定します。
            /// </param>
            /// <param name="bufPtr">
            /// データを受信するバッファーへのポインター。
            /// このデータの形式は 、level パラメーターの値によって異なります。
            /// このバッファーはシステムによって割り当てられ、 NetApiBufferFree 関数を使用して解放する必要があります。 関数が ERROR_MORE_DATA で失敗した場合でも、バッファーを解放する必要があることに注意してください。
            /// </param>
            /// <param name="prefmaxlen">
            /// 返されるデータの推奨される最大長をバイト単位で指定します。
            /// MAX_PREFERRED_LENGTH を指定すると、データに必要なメモリ量が関数によって割り当てられます。 このパラメーターに別の値を指定すると、関数から返されるバイト数を制限できます。 バッファー サイズが不十分で、すべてのエントリを保持するには、関数は ERROR_MORE_DATAを返します。
            /// </param>
            /// <param name="entriesread">
            /// 実際に列挙された要素の数を受け取る値へのポインター。
            /// </param>
            /// <param name="totalentries">
            /// 列挙された可能性のあるエントリの合計数を受け取る値へのポインター。
            /// アプリケーションでは、この値をヒントとしてのみ考慮する必要があることに注意してください。
            /// </param>
            /// <param name="resume_handle">
            /// 既存の共有検索を続行するために使用される再開ハンドルを含む値へのポインター。
            /// 最初の呼び出しではハンドルを 0 にし、後続の呼び出しでは変更しない必要があります。
            /// </param>
            /// <returns>
            /// 関数が成功した場合、戻り値は NERR_Success。
            /// 関数が失敗した場合、戻り値はシステム エラー コードです。
            /// </returns>
            [LibraryImport("Netapi32.dll", StringMarshalling = StringMarshalling.Utf16)]
            public static partial NERR NetShareEnum(String? serverName, SharedDataInformationLevel level, out IntPtr bufPtr, UInt32 prefmaxlen, out Int32 entriesread, out Int32 totalentries, ref Int32 resume_handle);

            [LibraryImport("Netapi32.dll")]
            public static partial NERR NetApiBufferFree(IntPtr buffer);
        }
    }
}
