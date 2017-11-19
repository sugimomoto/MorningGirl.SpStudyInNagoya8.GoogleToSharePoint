using System;
using System.Data.CData.SharePoint;

namespace MorningGirl.SpStudyInNagoya8.GoogleToSharePoint
{
    public class SharePointManager
    {
        private string SharePointConnectionString;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="connectionString"></param>
        public SharePointManager(string connectionString)
        {
            this.SharePointConnectionString = connectionString;
        }
        
        /// <summary>
        /// SharePointにタスクを作成
        /// </summary>
        /// <param name="targetRecord"></param>
        /// <param name="tradeFlg"></param>
        public void InsertSharePointData(string taskName)
        {
            using (var connection = new SharePointConnection(this.SharePointConnectionString))
            {
                int rowsAffected;
                var cmd = new SharePointCommand("INSERT INTO NagoyaTask (タスク名3,期限,説明) " +
                    $"VALUES ('{taskName}','{DateTime.Now.AddDays(3)}','Google Home から作成')", connection);

                rowsAffected = cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// SharePointのタスクを全部削除
        /// </summary>
        public void DeleteSharePointData()
        {
            using (var connection = new SharePointConnection(this.SharePointConnectionString))
            {
                int rowsAffected;
                var cmd = new SharePointCommand("DELETE FROM NagoyaTask ", connection);
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// SharePointの特定のタスクを削除
        /// タスク名でマッチング
        /// </summary>
        /// <param name="taskName"></param>
        public void DeleteSharePointData(string taskName)
        {
            using (var connection = new SharePointConnection(this.SharePointConnectionString))
            {
                int rowsAffected;
                var cmd = new SharePointCommand($"DELETE FROM NagoyaTask where [タスク名3] = @taskName", connection);
                cmd.Parameters.Add(new SharePointParameter("taskName", taskName));
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }
        
        /// <summary>
        /// SharePointのタスクをカウント
        /// </summary>
        /// <returns></returns>
        public int CountSharePointData()
        {
            var rtn = 0;

            using (var connection = new SharePointConnection(this.SharePointConnectionString))
            {
                var cmd = new SharePointCommand($"SELECT COUNT(*) as COUNT FROM NagoyaTask", connection);
                var rowsAffected = cmd.ExecuteReader();

                while (rowsAffected.Read())
                {
                    rtn = (int)rowsAffected["COUNT"];
                }
            }

            return rtn;
        }


        /// <summary>
        /// SharePointのタスクをカウント
        /// </summary>
        /// <returns></returns>
        public int CountSharePointData(DateTime datetime)
        {
            var rtn = 0;

            using (var connection = new SharePointConnection(this.SharePointConnectionString))
            {
                var cmd = new SharePointCommand($"SELECT COUNT(*) as COUNT FROM [CData].[SharePoint].[NagoyaTask] where [登録日時] < '{datetime.AddDays(1)}' and [登録日時] > '{datetime}'", connection);

                var rowsAffected = cmd.ExecuteReader();

                while (rowsAffected.Read())
                {
                    rtn = (int)rowsAffected["COUNT"];
                }
            }

            return rtn;
        }
    }
}
