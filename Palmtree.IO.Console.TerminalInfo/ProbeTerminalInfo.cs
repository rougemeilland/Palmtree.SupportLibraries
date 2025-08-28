using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmtree.IO.Console
{
#if DEBUG
    /// <summary>
    /// デバッグ用の調査コードのクラスです
    /// </summary>
    public static class ProbeTerminalInfo
    {
        /// <summary>
        /// テストを実行します。
        /// </summary>
        public static void DoTest()
        {
            System.Diagnostics.Debug.WriteLine(typeof(Color8).FullName);
            System.Diagnostics.Debug.WriteLine(typeof(Color16).FullName);
            System.Diagnostics.Debug.WriteLine(typeof(Color88).FullName);
            System.Diagnostics.Debug.WriteLine(typeof(Color256).FullName);
        }
    }
#endif
}
