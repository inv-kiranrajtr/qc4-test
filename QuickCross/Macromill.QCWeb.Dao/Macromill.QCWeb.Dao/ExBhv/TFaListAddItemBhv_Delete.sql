/*
[df:title]
付加アイテムテーブルの削除

[df:description]
付加アイテムテーブルのレコードを削除する SQL 文。仕様書 2.4.32。

TFAListAddItemBhv_Delete.sql
*/
-- !TFAListAddItemPmb!
-- !!decimal QcWebID!!

DELETE FROM
  T_FA_List_Add_Item lai
WHERE
  EXISTS (
    SELECT
      *
    FROM
      T_Scenario_Totalization st
    WHERE
      st.QCWebID = /*pmb.QcWebID*/1
      AND st.Scenario_Totalization_ID = lai.Scenario_Totalization_ID
  );
