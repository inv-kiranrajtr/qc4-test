/*
[df:title]
出力物レポートセット情報テーブルの削除

[df:description]
出力物レポートセット情報のレコードを削除する SQL 文。仕様書 2.4.。

TOutputReportsetInfoBhv_Delete.sql
*/
-- !TOutputReportsetInfoPmb!
-- !!decimal QcWebID!!


DELETE FROM
 T_Output_Reportset_Info i
WHERE EXISTS
 (SELECT * FROM T_Output_Request r
   WHERE Qcwebid = /*pmb.QcWebID*/1
   AND i.Output_Reportset_Info_Id = r.Output_Reportset_Info_Id);
