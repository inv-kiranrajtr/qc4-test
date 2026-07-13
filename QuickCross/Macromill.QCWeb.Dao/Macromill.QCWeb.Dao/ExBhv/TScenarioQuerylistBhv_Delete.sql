/*
[df:title]
シナリオ絞込み条件テーブルの削除

[df:description]
シナリオ絞込み条件テーブルのレコードを削除する SQL 文。仕様書 2.4.35。

TScenarioQuerylistBhv_Delete.sql
*/
-- !TScenarioQuerylistPmb!
-- !!decimal QcWebID!!

DELETE FROM
  T_Scenario_Querylist sq
WHERE
  EXISTS (
    SELECT
      *
    FROM
      T_Scenario_Totalization st
    WHERE
      st.QCWebID = /*pmb.QcWebID*/1
      AND st.Scenario_Totalization_ID = sq.Scenario_Totalization_ID
  );
