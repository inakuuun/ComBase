using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Logs
{
    /// <summary>
    /// ログ生成クラス
    /// </summary>
    public class Log
    {
        /// <summary>
        /// ログ出力先ディレクトリ
        /// </summary>
        private static string _logDirectory = string.Empty;

        /// <summary>
        /// ログ出力先ディレクトリの作成
        /// </summary>
        public static void CreateLogDirectory()
        {
            // ログ出力先ディレクトリ
            _logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "trace");

            // 対象のディレクトリが存在しない場合は作成
            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }
        }

        /// <summary>
        /// ログ出力処理
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="message"></param>
        public static void Trace(string? fileName, string message)
        {
            try
            {
                // 出力先ファイル名が存在する場合にのみ処理を実施
                if (!string.IsNullOrEmpty(fileName))
                {
                    // 呼び出し元情報を取得
                    StackTrace stacTrace = new StackTrace();
                    StackFrame? stackFrame = stacTrace?.GetFrame(1);
                    // 呼び出し元のメソッド名を取得
                    string? methodName = stackFrame?.GetMethod()?.Name;
                    // 呼び出し元メソッドのパラメータ(仮引数)を取得
                    ParameterInfo[]? parameters = stackFrame?.GetMethod()?.GetParameters();

                    // メソッド名生成処理
                    methodName += "(";
                    if (parameters != null && parameters.Length > 0)
                    {
                        foreach (var parameter in parameters)
                        {
                            // 型名
                            string paramType = parameter.ParameterType.Name.ToLower();
                            // 仮引数名
                            string? paramName = parameter.Name;
                            methodName += $"{paramType} {paramName},";
                        }
                    }
                    methodName = methodName.TrimEnd(',');
                    methodName += ")";

                    // ログエントリの生成（出力するメッセージ）
                    string logEntory = $"[{DateTime.Now}] {methodName} => {message}{Environment.NewLine}";

                    // ログファイル名を生成
                    string logFileName = $"{fileName}.log";
                    // ログ出力ファイルパスを生成
                    string logFilePath = Path.Combine(_logDirectory, logFileName);

                    // ファイルへの書き込み
                    // ※ファイルが存在しない場合は作成し、末尾にログを追加
                    File.AppendAllText(logFilePath, logEntory);
                    // コンソールに出力
                    Console.WriteLine(logEntory);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
