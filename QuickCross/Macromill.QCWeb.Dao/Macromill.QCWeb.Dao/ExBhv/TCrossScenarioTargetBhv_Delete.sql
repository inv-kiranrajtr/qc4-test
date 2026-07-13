/*
[df:title]
クロス集計対象シナリオアイテムテーブルの削除

[df:description]
クロス集計対象シナリオアイテムテーブルのレコードを削除する SQL 文。仕様書 2.4.30。

TCrossScenarioTargetBhv_Delete.sql
*/
-- !TCrossScenarioTargetPmb!
-- !!decimal QcWebID!!

DELETE FROM
  T_Cross_Scenario_Target cst
WHERE
  EXISTS (
    SELECT
      *
    FROM
      T_Scenario_Totalization st
    WHERE
      st.QCWebID = /*pmb.QcWebID*/1
      AND st.Scenario_Totalization_ID = cst.Scenario_Totalization_ID
  );
