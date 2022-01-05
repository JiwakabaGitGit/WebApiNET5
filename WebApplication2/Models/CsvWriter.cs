using System.Collections.Generic;
using System.Text;


namespace WebApplication2.Models
{
    public class CsvWriter
    {
        // ヘッダーとボディ（定義情報）を結合してCSVデータ文字列を作成
        public static string CreateCsv(List<Member> memberList)
        {
            var sb = new StringBuilder();

            // ヘッダーの作成
            sb.AppendLine(CreateCsvHeader(headerArray));

            // ボディの作成
            memberList.ForEach(a => sb.AppendLine(CreateCsvBody(a)));

            return sb.ToString();
        }

        // CSVヘッダーの配列
        private static string[] headerArray = { "Id", "Name", "Email", "Birth", "Married", "Memo" };

        // CSVヘッダー文字列を作成
        private static string CreateCsvHeader(string[] headerArray)
        {
            var sb = new StringBuilder();
            foreach (var header in headerArray)
            {
                sb.Append($@"""{header}"",");
            }
            // 最後のカンマを削除して返す
            return sb.Remove(sb.Length - 1, 1).ToString();
        }

        // CSV定義情報文字列の作成
        private static string CreateCsvBody(Member a)
        {
            var sb = new StringBuilder();
            sb.Append(string.Format($@"""{a.Id}"","));
            sb.Append(string.Format($@"""{a.Name}"","));
            sb.Append(string.Format($@"""{a.Email}"","));
            sb.Append(string.Format($@"""{a.Birth}"","));
            sb.Append(string.Format($@"""{a.Married}"","));
            sb.Append(string.Format($@"""{a.Memo}"","));
            return sb.ToString();
        }
    }
}
