/*
[df:title]
GT シナリオアイテムテーブルの削除

[df:description]
GT シナリオアイテムテーブルのレコードを削除する SQL 文。仕様書 2.4.27。

TGTScenarioItem_Delete.sql
*/
-- !TGTScenarioItemPmb!
-- !!decimal QcWebID!!

DELETE FROM
  T_GT_Scenario_Item gtsi
WHERE
  EXISTS (
    SELECT
      *
    FROM
      T_Scenario_Totalization st
    WHERE
      st.QCWebID = /*pmb.QcWebID*/1
      AND st.Scenario_Totalization_ID = gtsi.Scenario_Totalization_ID
  )
