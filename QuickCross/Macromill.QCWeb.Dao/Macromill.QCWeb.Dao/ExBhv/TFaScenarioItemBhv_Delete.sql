/*
[df:title]
FA シナリオアイテムテーブルの削除

[df:description]
FA シナリオアイテムテーブルのレコードを削除する SQL 文。仕様書 2.4.31。

TFAScenarioItemBhv_Delete.sql
*/
-- !TFAScenarioItemPmb!
-- !!decimal QcWebID!!

DELETE FROM
  T_FA_Scenario_Item fasi
WHERE
  EXISTS (
    SELECT
      *
    FROM
      T_Scenario_Totalization st
    WHERE
      st.QCWebID = /*pmb.QcWebID*/1
      AND st.Scenario_Totalization_ID = fasi.FA_Scenario_Item_ID
  );
