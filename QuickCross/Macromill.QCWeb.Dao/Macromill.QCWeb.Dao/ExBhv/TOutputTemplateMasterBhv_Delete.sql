/*
[df:title]
PP テンプレートマスタテーブルの削除

[df:description]
PP テンプレートマスタテーブルのレコードを削除する SQL 文。仕様書 2.4.12。

TOutputTemplateMasterBhv_Delete.sql
*/
-- !TOutputTemplateMasterPmb!
-- !!decimal QcWebID!!

DELETE FROM
  T_Output_Template_Master tm
WHERE
  EXISTS (
    SELECT
      *
    FROM
      T_Output_Request req
    INNER JOIN
      T_Output_Reportset_Info rep
      ON req.Output_Reportset_Info_ID = rep.Output_Reportset_Info_ID
    INNER JOIN
      T_Output_Template t
      ON rep.Output_Template_ID = t.Output_Template_ID
    WHERE
      req.QCWebID = /*pmb.QcWebID*/1
      AND tm.Output_Template_Master_ID = t.Output_Template_Master_ID
  );
