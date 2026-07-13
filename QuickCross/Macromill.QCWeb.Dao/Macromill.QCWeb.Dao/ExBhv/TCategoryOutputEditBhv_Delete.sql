/*
[df:title]
カテゴリ出力編集テーブルの削除

[df:description]
カテゴリ出力編集テーブルのレコードを削除する SQL 文。仕様書 2.4.37。

TCategoryOutputEditBhv_Delete.sql
*/
-- !TCategoryOutputEditPmb!
-- !!decimal QcWebID!!

DELETE FROM
  T_Category_Output_Edit coe
WHERE
  EXISTS (
    SELECT
      *
    FROM
      T_Scenario_Totalization st
    WHERE
      st.QCWebID = /*pmb.QcWebID*/1
      AND st.Scenario_Totalization_ID = coe.Scenario_Totalization_ID
  );
