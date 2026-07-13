/*
[df:title]
出力物レポートセット情報テーブルの削除

[df:description]
出力物削除バッチで、出力物レポートセット情報のレコードを
削除する SQL 文。仕様書 2.3.2.6。

TOutputReportsetInfoBhv_Delete.sql
*/
-- !TOutputReportsetInfoOutputDeletePmb!
-- !!decimal outputRequestId!!


DELETE FROM
 T_Output_Reportset_Info i
WHERE EXISTS
 (SELECT * FROM T_Output_Request r
   WHERE r.Output_Request_ID = /*pmb.outputRequestId*/1
   AND i.Output_Reportset_Info_Id = r.Output_Reportset_Info_Id);
