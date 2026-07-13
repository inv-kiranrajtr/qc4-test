/*
[df:title]
カテゴリ編集及びGT集計設定追加に追加された
データ数を取得する

[df:description]
QCWEB管理IDに紐づくカテゴリ編集及びGT集計設定追加に
生み出したアイテムの数を取得する


以下の条件値は必須です
・WkQCWebId
・WkGTtableFlg

$Id: TScenarioTotalizationBhv_SelectScenarioAppendCount.sql 5718 2012-04-18 09:23:29Z yzhengy@JOPS.LOCAL $
*/
-- #df:entity#
-- +scalar+

-- !TScenarioItemAppendCountPmb!
-- !!decimal WkQCWebId!!
-- !!bool WkGTtableFlg!!

SELECT  COUNT(*) AS CNT
FROM T_SCENARIO_TOTALIZATION A,
/*IF pmb.WkGTtableFlg*/
T_GT_MATRIX_INFO B
--ELSE T_CATEGORY_OUTPUT_EDIT B
/*END*/
WHERE 
A.SCENARIO_TOTALIZATION_ID = B.SCENARIO_TOTALIZATION_ID
AND A.QCWEBID = /*pmb.WkQCWebId*/1 
