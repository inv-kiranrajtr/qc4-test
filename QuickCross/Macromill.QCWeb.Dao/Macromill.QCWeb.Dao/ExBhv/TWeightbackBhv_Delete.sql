/*
[df:title]
ウェイトバックヘッダーテーブルの削除

[df:description]
ウェイトバックヘッダーテーブルのレコードを削除する SQL 文。仕様書 2.4.34。

TWeightbackBhv_Delete.sql
*/
-- !TWeightbackPmb!
-- !!decimal QcWebID!!

DELETE FROM
  T_Weightback wb
WHERE
  EXISTS (
    SELECT
      *
    FROM
      T_Scenario_Totalization st
    WHERE
      st.QCWebID = /*pmb.QcWebID*/1
      AND st.Scenario_Totalization_ID = wb.Scenario_Totalization_ID
  );
